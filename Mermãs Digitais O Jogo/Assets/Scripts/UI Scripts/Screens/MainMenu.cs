using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

using TMPro;
using System.Collections;
using Unity.VisualScripting;
using static Unity.Collections.Unicode;


public class MainMenu : MonoBehaviour
{
    [Header("Controlador da UI")]
    [SerializeField] private GameObject uiManagerController;

    // Refer�ncias para os pain�is que voc� j� tem (Loja, Op��es e Cr�ditos)
    public GameObject telaLoja;
    public GameObject telaOpcoes;
    public GameObject telaCreditos;
    public GameObject telaMenu;
    public GameObject telaInput;

    // Refer�ncias dos bot�es
    public Button historiaBtn;
    public Button lojaBtn;
    public Button opcoesBtn;
    public Button creditosBtn;
    public Button sairBtn;

    public Button voltarTituloBtn;
    public Button voltarInputBtn;
    public Button voltarLojaBtn;
    public Button voltarOpcoesBtn;
    public Button voltarCreditosBtn;

    public TMP_InputField inputNome;

    [SerializeField] private AudioClip clip;
    [SerializeField] private ClickButtonEffect clip2;
    [SerializeField] private GameplayAudio sfxClick;


    // private ClickButtonEffect uiTitleScreen;
    void Start()
    {
        // Configura os bot�es
        historiaBtn.onClick.AddListener(AbrirHistoria);
        lojaBtn.onClick.AddListener(() => AbrirTela(telaLoja));
        opcoesBtn.onClick.AddListener(() => AbrirTela(telaOpcoes));
        creditosBtn.onClick.AddListener(() => AbrirTela(telaCreditos));
        sairBtn.onClick.AddListener(SairDoJogo);

        if (voltarLojaBtn != null) voltarLojaBtn.onClick.AddListener(() => FecharTela(telaLoja));
        if (voltarOpcoesBtn != null) voltarOpcoesBtn.onClick.AddListener(() => FecharTela(telaOpcoes));
        if (voltarCreditosBtn != null) voltarCreditosBtn.onClick.AddListener(() => FecharTela(telaCreditos));
        if (voltarTituloBtn != null) voltarTituloBtn.onClick.AddListener(() => FecharTela(telaMenu));
        if (voltarInputBtn != null) voltarInputBtn.onClick.AddListener(() => FecharTela(telaInput));

        // Desativa todos os pain�is inicialmente
        FecharTodasAsTelas();

        AddEventTriggers(historiaBtn);
        AddEventTriggers(lojaBtn);
        AddEventTriggers(opcoesBtn);
        AddEventTriggers(creditosBtn);
        AddEventTriggers(sairBtn);
    }

    // Desativa os pain�is existentes
    void FecharTodasAsTelas()
    {
        sfxClick.Audio(clip);
        if (telaInput != null) telaInput.SetActive(false);
        if (telaLoja != null) telaLoja.SetActive(false);
        if (telaOpcoes != null) telaOpcoes.SetActive(false);
        if (telaCreditos != null) telaCreditos.SetActive(false);
    }

    // Ativa o painel passado como par�metro
    void AbrirTela(GameObject tela)
    {
        sfxClick.Audio(clip);
        FecharTodasAsTelas();
        if (tela != null)
            tela.SetActive(true);

        if (tela == telaLoja && CoinManager.instance != null)
        {
            CoinObjectValue.instance.UpdateCoinUI();
        }
    }

    // A��o para o bot�o Hist�ria (n�o implementado)
    void AbrirHistoria()
    {
        // Debug.Log("Bot�o Hist�ria clicado. Tela n�o implementada.");
        // sfxClick.Audio(clip);
        telaMenu.SetActive(false);
        // uiManagerController.SetActive(false);
        telaInput.SetActive(true);
    }

    // A��o para o bot�o Sair
    void SairDoJogo()
    {
        sfxClick.Audio(clip);
        // Debug.Log("Saindo do jogo...");
        Application.Quit();
    }

    // M�todo que adiciona os eventos de PointerEnter e PointerExit pro bot�o
    void AddEventTriggers(Button btn)
    {
        EventTrigger trigger = btn.gameObject.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = btn.gameObject.AddComponent<EventTrigger>();
        }

        // Evento quando o mouse entra no bot�o
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((data) => { OnButtonPointerEnter(btn); });
        trigger.triggers.Add(entryEnter);

        // Evento quando o mouse sai do bot�o
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((data) => { OnButtonPointerExit(btn); });
        trigger.triggers.Add(entryExit);
    }

    // Ativa o FundoRosa quando o mouse entra
    void OnButtonPointerEnter(Button btn)
    {
        Transform fundoRosa = btn.transform.Find("PinkEffect");
        if (fundoRosa != null)
        {
            fundoRosa.gameObject.SetActive(true);
        }
    }

    // Desativa o FundoRosa quando o mouse sai
    void OnButtonPointerExit(Button btn)
    {
        Transform fundoRosa = btn.transform.Find("PinkEffect");
        if (fundoRosa != null)
        {
            fundoRosa.gameObject.SetActive(false);
        }
    }

    void FecharTela(GameObject tela)
    {
        sfxClick.Audio(clip);
        if (tela != null)
            tela.SetActive(false);
    }

    public void GoToCutscene()
    {
        // sfxClick.Audio(longClick.Clip2);
        GameObject executor = new GameObject("CoroutineExecutor");
        var runner = executor.AddComponent<CoroutineRunner>();
        StartCoroutine(GoToCutsceneAfterDelay(runner));
        runner.StartCoroutine(GoToCutsceneAfterDelay(runner));
        /*StartCoroutine(WaitButtonSound());
        PlayerData.playerName = inputNome.text.Trim();
        SceneManager.LoadScene("Cutscene");*/
    }

    private IEnumerator GoToCutsceneAfterDelay(CoroutineRunner runner)
    {
        // Toca o som do clique
        sfxClick.Audio(clip2.Clip2);

        // Aguarda 2 segundos para o som terminar
        yield return new WaitForSeconds(0.5f);

        // Salva o nome do jogador e carrega a próxima cena
        PlayerData.playerName = inputNome.text.Trim();
        SceneManager.LoadScene("Cutscene");
        Destroy(runner.gameObject);
    }

    public void VoltarParaMenu()
    {
        sfxClick.Audio(clip);
        telaMenu.SetActive(true);
    }

    // public class CoroutineRunner : MonoBehaviour { }

    /*private IEnumerator WaitButtonSound()
    {
        yield return new WaitForSeconds(0.2f);
        // GoToCutscene();
    }*/
}
