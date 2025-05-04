using UnityEngine;

public class CharacterName : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private Vector3 offset;
    [SerializeField] private Canvas canvas;

    private Camera cam;
    private RectTransform rectTransform;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        cam = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }
    private void Update()
    {
        if (cam == null)
        {
            cam = Camera.main;
            if (cam == null) return; // ainda não foi atribuída
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

        /*Vector3 pos = cam.WorldToScreenPoint(lookAt.position + offset);

        if (transform.position != pos)
        {
            transform.position = pos;
        }*/
    }
}
