using System.Collections;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class LevelGenerator : MonoBehaviour
{
    public static LevelGenerator instance;
    
    [Header("Botões das Fases")]
    public Button botaoTutorial;
    public Button botaoFase1;
    public Button botaoFase2;
    public Button botaoFase3;

    // [SerializeField] private Loader loader;
    [SerializeField] private Button voltarButton;
    // [SerializeField] private AudioClip clip;
    // [SerializeField] private GameplayAudio sfxClick;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            // DontDestroyOnLoad(gameObject); // Mantém só este objeto
        }
        else if (instance != this)
        {
            Destroy(gameObject); // Evita duplicatas
        }
    }
    void Start()
    {
        /*if (botaoTutorial != null)
        {
            botaoTutorial.onClick.AddListener(() => StartCoroutine(GoToCourse("Tutorial")));
            // botaoTurorial.onClick.AddListener(() => CarregarComLoading("Tutorial"));
        }

        if (botaoFase1 != null)
        {
            botaoFase1.onClick.AddListener(() => StartCoroutine(GoToCourse("Fase 1")));
        }

        if (botaoFase2 != null)
        {
            botaoFase2.onClick.AddListener(() => StartCoroutine(GoToCourse("Fase 2")));
        }

        if (botaoFase3 != null)
            botaoFase3.onClick.AddListener(() => StartCoroutine(GoToCourse("Fase 3")));

        if (voltarButton != null)
            voltarButton.onClick.AddListener(() => StartCoroutine(GoToCourse("Main Menu")));*/

        botaoTutorial.onClick.AddListener(() => LevelManager.instance.IrParaFase("Tutorial"));
        botaoFase1.onClick.AddListener(() => LevelManager.instance.IrParaFase("Fase 1"));
        botaoFase2.onClick.AddListener(() => LevelManager.instance.IrParaFase("Fase 2"));
        botaoFase3.onClick.AddListener(() => LevelManager.instance.IrParaFase("Fase 3"));
        voltarButton.onClick.AddListener(() => LevelManager.instance.IrParaFase("Main Menu"));
    }

    /*void CarregarComLoading(string nomeCena)
    {
        // Debug.Log("Passando pela tela de carregamento antes de: " + nomeCena);
        loader.CarregarFase(nomeCena); // Aqui ele chama o sistema de carregamento com animação
    }*/
}

