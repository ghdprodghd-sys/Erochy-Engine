namespace Erochy.Audio;

/// <summary>
/// Interface para o subsistema de áudio, abstraindo bibliotecas como FMOD, Wwise ou OpenAL.
/// </summary>
public interface IAudio
{
    /// <summary>
    /// Volume global mestre da engine (0.0f a 1.0f).
    /// </summary>
    float MasterVolume { get; set; }

    /// <summary>
    /// Toca um som de uma única vez (ex: efeito sonoro).
    /// </summary>
    void PlayOneShot(string soundPath, float volume = 1.0f);

    /// <summary>
    /// Inicia a reprodução de uma música em loop (ex: trilha sonora).
    /// </summary>
    void PlayMusic(string musicPath, float volume = 1.0f);
    
    /// <summary>
    /// Para a música atual.
    /// </summary>
    void StopMusic();
}
