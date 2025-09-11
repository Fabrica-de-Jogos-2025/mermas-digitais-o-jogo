using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterName : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Canvas canvas;

    public Camera cam;
    private RectTransform rectTransform;

    private void Start()
    {
        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    void Awake()
    {
        // Mantém esse objeto entre cenas
        //DontDestroyOnLoad(gameObject);
        // Escuta quando uma nova cena é carregada
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene cena, LoadSceneMode modo)
    {
        Camera novaCamera = Camera.main;
    }


    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }


    void OnEnable()
{
    SceneManager.sceneLoaded += AtualizarCamera;
}

void OnDisable()
{
    SceneManager.sceneLoaded -= AtualizarCamera;
}

void AtualizarCamera(Scene cena, LoadSceneMode modo)
{
    cam = Camera.main;
}

    private void Update()
    {
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null) return;
        }

        if (lookAt == null || canvas == null) return;

        Vector3 worldPos = lookAt.position + offset;

        Vector2 screenPoint;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            cam.WorldToScreenPoint(worldPos),
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : cam,
            out screenPoint))
        {
            rectTransform.anchoredPosition = screenPoint;
        }
    }
}
