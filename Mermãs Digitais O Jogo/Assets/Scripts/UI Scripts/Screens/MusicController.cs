using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MusicController : MonoBehaviour
{
    private const string PREF_MUSIC = "music";
    private AudioSource[] musicSources;

    private void Awake()
    {
        // Garante que só exista um objeto controlador
        var existing = FindObjectsByType<MusicController>(FindObjectsSortMode.None);
        if (existing.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        ApplyMusicVolume();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        ApplyMusicVolume();
    }

    public void ApplyMusicVolume()
    {
        // Busca todos os objetos com tag "Music" (recomendado)
        musicSources = GameObject.FindGameObjectsWithTag("Music")
            .Select(obj => obj.GetComponent<AudioSource>())
            .Where(a => a != null)
            .ToArray();

        int savedMusic = PlayerPrefs.GetInt(PREF_MUSIC, 50);
        float volume = Mathf.Lerp(0f, 1f, savedMusic / 100f);

        foreach (var audio in musicSources)
            audio.volume = volume;
    }
}
