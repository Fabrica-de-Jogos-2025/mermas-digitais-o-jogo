using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private RobotMovement robot;
    [SerializeField] private Loader loader;
    [SerializeField] private Canvas canvas;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip click;
    [SerializeField] private float clickDelay = 0.25f;
    public Transition t;

    private void Start()
    {
        player = FindAnyObjectByType<PlayerMovement>();
        t = FindAnyObjectByType<Transition>();
    }

    public void ResumeButton()
    {
        StartCoroutine(HandleButtonAction(() =>
        {
            player.IsPaused = false;
            Time.timeScale = 1;
            player.PauseScreen.SetActive(false);
        }));
    }
    public void ResetButton()
    {
        StartCoroutine(HandleButtonAction(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }));
    }

    public void QuitButton()
    {
       StartCoroutine(HandleButtonAction(() =>
        {
            t.validacaoPorStart = true;
            t.verificarMaiorTransition();
            Time.timeScale = player.isPaused ? 1 : 0;
            loader.CarregarFase("Level Selector");
            Destroy(canvas.gameObject);
        }));

        //StartCoroutine(HandleButtonAction(() =>
        //{
        //    loader.CarregarFase("Level Selector");
        //    Destroy(canvas.gameObject);
        //}));
    }

    private IEnumerator HandleButtonAction(System.Action action)
    {
        // Toca o som de click e espera um pouco antes da a��o
        sfxAcess.Audio(click);
        yield return new WaitForSecondsRealtime(clickDelay);

        // Executa a a��o correspondente
        action.Invoke();
    }
}
