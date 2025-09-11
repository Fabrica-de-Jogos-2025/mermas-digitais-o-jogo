using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnManager : MonoBehaviour
{
    private PlayerMovement player;
    private PlayerStatus status;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player")?.GetComponent<PlayerMovement>();
        status = player?.GetComponent<PlayerStatus>();

        if (player != null && status != null)
        {
            bool checkpointAtivo = PlayerPrefs.GetInt("CheckpointAtivo", 0) == 1;
            player.Respawn(checkpointAtivo);
            status.Canvas.SetActive(true);
        }
    }
}
