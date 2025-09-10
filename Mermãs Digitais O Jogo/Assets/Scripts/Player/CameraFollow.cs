using UnityEngine;
using UnityEngine.SceneManagement;
using Unity.Cinemachine;

public class CameraFollow : MonoBehaviour
{
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
        var player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            var vcam = FindAnyObjectByType<CinemachineCamera>();
            if (vcam != null)
                vcam.Follow = player.transform;
        }
    }
}
