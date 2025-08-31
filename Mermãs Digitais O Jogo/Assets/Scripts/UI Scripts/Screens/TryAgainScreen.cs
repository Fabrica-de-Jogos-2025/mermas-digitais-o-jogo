using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TryAgainScreen : MonoBehaviour
{
    public Button yesBtn;
    public Button noBtn;

    [SerializeField] private Loader loader;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
}
