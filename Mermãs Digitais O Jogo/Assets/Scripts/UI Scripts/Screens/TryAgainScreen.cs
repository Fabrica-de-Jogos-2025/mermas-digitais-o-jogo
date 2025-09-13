using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TryAgainScreen : MonoBehaviour
{
    public Button yesBtn;
    public Button noBtn;

    [Header("Prefabs")]
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject robotPrefab;

    [SerializeField] private Loader loader;
    [SerializeField] private PlayerStatus playerStatus;
    private string cenaAnterior;
    private bool yesClicked = false;

    public bool YesClicked { get => yesClicked; set => yesClicked = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // playerStatus = GameObject.Find("Player").GetComponent<PlayerStatus>();
        cenaAnterior = playerStatus.CenaAtual;
    }

    // Update is called once per frame
    void Update()
    {
        AddEventTriggers(yesBtn);
        AddEventTriggers(noBtn);
    }

    // Método que adiciona os eventos de PointerEnter e PointerExit pro botão
    void AddEventTriggers(Button btn)
    {
        EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = btn.gameObject.AddComponent<EventTrigger>();
        }

        // Evento quando o mouse entra no botão
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnButtonPointerEnter(btn); });
        trigger.triggers.Add(entryEnter);

        // Evento quando o mouse sai do botão
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnButtonPointerExit(btn); });
        trigger.triggers.Add(entryExit);
    }

    void OnButtonPointerEnter(Button btn)
    {
        Transform hover = btn.transform.Find("Hover");
        if (hover != null)
        {
            hover.gameObject.SetActive(true);
        }
    }

    // Desativa o FundoRosa quando o mouse sai
    void OnButtonPointerExit(Button btn)
    {
        Transform hover = btn.transform.Find("Hover");
        if (hover != null)
        {
            hover.gameObject.SetActive(false);
        }
    }

    public void NoButton()
    {
        loader.CarregarFase("Level Selector");
    }

    public void YesButton()
    {
        yesClicked = true;

        /*string cenaAnterior = PlayerPrefs.GetString("LastScene", "Fase 1");
        // bool checkpointAtivo = PlayerPrefs.GetInt("CheckpointAtivo", 0) == 1;

        // recarrega a fase
        SceneManager.LoadScene(cenaAnterior);*/
        // playerPrefab.IsDestroyed();

        string lastScene = PlayerPrefs.GetString("LastScene", SceneManager.GetActiveScene().name);

        // Recarrega a cena
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene(lastScene);

        // garante que Player e Robot já existem (DontDestroyOnLoad)
        /*PlayerMovement player = GameObject.Find("Player").GetComponent<PlayerMovement>();
        PlayerStatus status = player.GetComponent<PlayerStatus>();

        // respawn após a cena carregar
        player.StartCoroutine(RespawnAfterLoad(player, status, checkpointAtivo));*/
    }

    private IEnumerator RespawnAfterLoad(PlayerMovement player, PlayerStatus status, bool checkpointAtivo)
    {
        yield return new WaitForSeconds(0.1f); // espera cena carregar

        player.Respawn(checkpointAtivo);
        status.Canvas.SetActive(true);
        playerStatus.gameObject.SetActive(true);
    }

    private void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        // Instancia Player
        GameObject player = Instantiate(playerPrefab);

        // Se tinha checkpoint ativo, teleporta até lá
        if (PlayerPrefs.GetInt("CheckpointAtivo", 0) == 1)
        {
            Vector3 checkpointPos = new Vector3(
                PlayerPrefs.GetFloat("CheckpointX"),
                PlayerPrefs.GetFloat("CheckpointY"),
                PlayerPrefs.GetFloat("CheckpointZ")
            );
            player.transform.position = checkpointPos;
        }
        else
        {
            // Posição inicial padrão
            player.transform.position = Vector3.zero;
        }

        // Instancia Robô, se existir
        if (robotPrefab != null)
        {
            GameObject robot = Instantiate(robotPrefab);

            // (opcional) seguir o Player
            RobotMovement robotMov = robot.GetComponent<RobotMovement>();
            if (robotMov != null)
            {
                robotMov.Player = player.GetComponent<PlayerMovement>();
            }
        }
    }

}
