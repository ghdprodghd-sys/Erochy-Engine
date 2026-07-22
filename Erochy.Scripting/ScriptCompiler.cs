using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace Erochy.Scripting;

/// <summary>
/// Compilador JIT de scripts C# utilizando Roslyn.
/// Permite carregar scripts em tempo de execução sem dependência de DLLs pré-compiladas para a lógica de gameplay.
/// </summary>
public class ScriptCompiler
{
    public Assembly? CompileScript(string sourceCode, string assemblyName)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(sourceCode);

        // Adiciona referências básicas. Em um cenário real, leríamos as dependências do próprio assembly em execução.
        var references = new MetadataReference[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(Console).Assembly.Location),
            MetadataReference.CreateFromFile(typeof(IScript).Assembly.Location)
        };

        // Usa as referências carregadas no AppDomain atual para garantir que o script conheça a Engine
        var runtimeReferences = AppDomain.CurrentDomain.GetAssemblies()
            .Where(a => !a.IsDynamic && !string.IsNullOrWhiteSpace(a.Location))
            .Select(a => MetadataReference.CreateFromFile(a.Location));

        var allReferences = references.Concat(runtimeReferences).Distinct();

        var compilation = CSharpCompilation.Create(
            assemblyName,
            new[] { syntaxTree },
            allReferences,
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

        using var ms = new MemoryStream();
        var result = compilation.Emit(ms);

        if (!result.Success)
        {
            // Erros de compilação
            var failures = result.Diagnostics.Where(diagnostic =>
                diagnostic.IsWarningAsError ||
                diagnostic.Severity == DiagnosticSeverity.Error);

            foreach (var diagnostic in failures)
            {
                Console.Error.WriteLine($"{diagnostic.Id}: {diagnostic.GetMessage()}");
            }

            return null;
        }

        ms.Seek(0, SeekOrigin.Begin);
        return Assembly.Load(ms.ToArray());
    }

    /// <summary>
    /// Instancia um IScript a partir de um Assembly compilado dinamicamente.
    /// </summary>
    public IScript? InstantiateScript(Assembly assembly, string typeName)
    {
        var type = assembly.GetType(typeName);
        if (type != null && typeof(IScript).IsAssignableFrom(type))
        {
            return Activator.CreateInstance(type) as IScript;
        }
        return null;
    }
}
