using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Splines;

public class Hability2 : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform canvasRect;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>(); // Garante que tenha um Canvas como referência
        canvasRect = canvas.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // Torna o objeto transparente ao começar o arraste
        canvasGroup.alpha = 0.9f;
        canvasGroup.blocksRaycasts = false; // Permite que eventos passem para objetos abaixo
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Move o objeto conforme o mouse/touch
        Vector2 newPosition = rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor;

        if (IsInsideCanvas(newPosition))
        {
            rectTransform.anchoredPosition = newPosition;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Restaura a visibilidade ao soltar o objeto
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    private bool IsInsideCanvas(Vector2 targetPosition)
    {
        Vector3[] corners = new Vector3[4];
        canvasRect.GetWorldCorners(corners);

        float left = corners[0].x;
        float right = corners[2].x;
        float bottom = corners[0].y;
        float top = corners[2].y;

        Vector3 worldPosition = rectTransform.position;
        worldPosition.x = Mathf.Clamp(worldPosition.x, left, right);
        worldPosition.y = Mathf.Clamp(worldPosition.y, bottom, top);

        return RectTransformUtility.RectangleContainsScreenPoint(canvasRect, worldPosition, canvas.worldCamera);
    }
}
