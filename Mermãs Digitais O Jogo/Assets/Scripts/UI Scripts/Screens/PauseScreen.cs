using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private RobotMovement robot;
    [SerializeField] private Loader loader;
    [SerializeField] private Canvas canvas;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
    }
    public void ResumeButton()
    {
        player.PauseScreen.SetActive(false);
        player.IsPaused = false;
        Time.timeScale = 1;
    }
    public void ResetButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
        player.PauseScreen.SetActive(false);
        player.IsPaused = false;
        Destroy(player.gameObject);
        Destroy(robot.gameObject);
    }

    public void QuitButton()
    {
        // SceneManager.LoadScene("Level Selector");
        Destroy(canvas.gameObject);
        loader.CarregarFase("Level Selector");
    }
}
