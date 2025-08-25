using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour
{
    // Referências para os painéis que você já tem (Loja, Opções e Créditos)
    public GameObject telaLoja;
    public GameObject telaOpcoes;
    public GameObject telaCreditos;
    public GameObject telaMenu;
    public GameObject telaInput;

    // Referências dos botões
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

    private ClickButtonEffect uiTitleScreen;
    void Start()
    {
        // Configura os botões
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

        // Desativa todos os painéis inicialmente
        FecharTodasAsTelas();

        AddEventTriggers(historiaBtn);
        AddEventTriggers(lojaBtn);
        AddEventTriggers(opcoesBtn);
        AddEventTriggers(creditosBtn);
        AddEventTriggers(sairBtn);
    }

    // Desativa os painéis existentes
    void FecharTodasAsTelas()
    {
        if (telaLoja != null) telaLoja.SetActive(false);
        if (telaOpcoes != null) telaOpcoes.SetActive(false);
        if (telaCreditos != null) telaCreditos.SetActive(false);
    }

    // Ativa o painel passado como parâmetro
    void AbrirTela(GameObject tela)
    {
        FecharTodasAsTelas();
        if (tela != null)
            tela.SetActive(true);
    }

    // Ação para o botão História (não implementado)
    void AbrirHistoria()
    {
        // Debug.Log("Botão História clicado. Tela não implementada.");
        telaMenu.SetActive(false);
        telaInput.SetActive(true);
    }

    // Ação para o botão Sair
    void SairDoJogo()
    {
        Debug.Log("Saindo do jogo...");
        Application.Quit();
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
        if (tela != null)
            tela.SetActive(false);
    }
}
