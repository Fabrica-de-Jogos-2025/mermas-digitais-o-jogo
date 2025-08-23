using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickButtonEffect : MonoBehaviour
{
    public Button blinkButton; // Arraste o botão no Inspector
    public GameObject uiTitle;
    public GameObject MainMenuUI; // Nome da cena para carregar
    public GameObject[] grounds;
    public float blinkSpeed = 2f;       // Velocidade do piscar
    public float minAlpha = 0.3f;       // Opacidade mínima
    public float maxAlpha = 1f;         // Opacidade máxima

    private TextMeshProUGUI buttonText;
    // [SerializeField] private MainMenu voltarButton;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (blinkButton != null)
            buttonText = blinkButton.GetComponentInChildren<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // ---- Piscar o botão ----
        if (buttonText != null)
        {
            timer += Time.deltaTime * blinkSpeed;
            float alpha = Mathf.Lerp(minAlpha, maxAlpha, (Mathf.Sin(timer) + 1) / 2f);

            Color c = buttonText.color;
            c.a = alpha;
            buttonText.color = c;
        }

        // ---- Detectar clique em qualquer lugar da tela ----
        if (Input.GetMouseButtonDown(0)) // Clique esquerdo ou toque na tela
        {
            uiTitle.SetActive(false);
            grounds[0].SetActive(false);
            grounds[1].SetActive(false);
            MainMenuUI.SetActive(true);
        }
    }

    public void VoltarTelaInicial()
    {
        uiTitle.SetActive(true);
        grounds[0].SetActive(true);
        grounds[1].SetActive(true);
    }
}
