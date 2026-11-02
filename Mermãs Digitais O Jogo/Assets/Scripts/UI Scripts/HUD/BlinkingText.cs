using UnityEngine;

[RequireComponent(typeof(CanvasGroup))]
public class BlinkingText : MonoBehaviour
{
    [Header("Configurações do Pisca")]
    [SerializeField] private float blinkSpeed = 2f;  // piscadas por segundo
    [SerializeField, Range(0f, 1f)] private float minAlpha = 0.2f;
    [SerializeField, Range(0f, 1f)] private float maxAlpha = 1f;

    private CanvasGroup canvasGroup;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void OnEnable()
    {
        // Garante que comece visível sempre que for reativado
        if (canvasGroup != null)
            canvasGroup.alpha = maxAlpha;
    }

    void Update()
    {
        // Oscila entre 0 e 1 usando seno; unscaledTime para funcionar mesmo com Time.timeScale = 0
        float t = (Mathf.Sin(Time.unscaledTime * blinkSpeed * Mathf.PI * 2f) + 1f) * 0.5f;
        canvasGroup.alpha = Mathf.Lerp(minAlpha, maxAlpha, t);
    }
}