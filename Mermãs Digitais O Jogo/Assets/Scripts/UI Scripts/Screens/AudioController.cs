using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : MonoBehaviour
{
    public static AudioController Instance { get; private set; }

    private const string PREF_MUSIC = "music";
    private const string PREF_SFX = "soundEffects";

    private List<AudioSource> musicSources = new List<AudioSource>();
    private List<AudioSource> sfxSources = new List<AudioSource>();

    private float musicVolume;
    private float sfxVolume;

    private void Awake()
    {
        // Garante que só exista um AudioManager
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        SceneManager.sceneLoaded += OnSceneLoaded;

        LoadVolumes();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        RefreshAllAudioSources();
    }

    private void LoadVolumes()
    {
        musicVolume = Mathf.Lerp(0f, 1f, PlayerPrefs.GetInt(PREF_MUSIC, 50) / 100f);
        sfxVolume = Mathf.Lerp(0f, 1f, PlayerPrefs.GetInt(PREF_SFX, 100) / 100f);
    }

    /// <summary>
    /// Atualiza o volume de todos os áudios na cena.
    /// </summary>
    public void RefreshAllAudioSources()
    {
        musicSources.Clear();
        sfxSources.Clear();

        // Procura todos os AudioSources na cena
        AudioSource[] allSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

        foreach (var source in allSources)
        {
            // Usa heurística simples: músicas geralmente tocam em loop
            if (source.loop)
                RegisterMusicSource(source);
            else
                RegisterSFXSource(source);
        }

        ApplyVolumes();
    }

    public void RegisterMusicSource(AudioSource source)
    {
        if (!musicSources.Contains(source))
        {
            musicSources.Add(source);
            source.volume = musicVolume;
        }
    }

    public void RegisterSFXSource(AudioSource source)
    {
        if (!sfxSources.Contains(source))
        {
            sfxSources.Add(source);
            source.volume = sfxVolume;
        }
    }

    public void ApplyVolumes()
    {
        foreach (var music in musicSources)
            if (music != null) music.volume = musicVolume;

        foreach (var sfx in sfxSources)
            if (sfx != null) sfx.volume = sfxVolume;
    }

    public void SetMusicVolume(int value)
    {
        PlayerPrefs.SetInt(PREF_MUSIC, value);
        PlayerPrefs.Save();
        musicVolume = Mathf.Lerp(0f, 1f, value / 100f);
        ApplyVolumes();
    }

    public void SetSFXVolume(int value)
    {
        PlayerPrefs.SetInt(PREF_SFX, value);
        PlayerPrefs.Save();
        sfxVolume = Mathf.Lerp(0f, 1f, value / 100f);
        ApplyVolumes();
    }
}
