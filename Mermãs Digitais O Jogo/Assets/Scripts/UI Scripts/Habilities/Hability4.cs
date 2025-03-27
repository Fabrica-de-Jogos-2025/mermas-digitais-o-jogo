using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Splines;

public class Hability4 : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform canvasRect;
    private bool isSnapped = false;

    public static List<Hability4> allDraggableObjects = new List<Hability4>();
    public List<GameObject> correctPositions; // Lista de posi��es corretas
    private GameObject snappedTarget;
    public bool close;
    private Vector2 NP, newPosition;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>(); // Garante que tenha um Canvas como refer?ncia
        canvasRect = canvas.GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();

        allDraggableObjects.Add(this);
    }

    private void OnDestroy()
    {
        allDraggableObjects.Remove(this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (isSnapped)
        {
            isSnapped = false;
            snappedTarget = null;
        }

        // Torna o objeto transparente ao come�ar o arraste
        canvasGroup.alpha = 0.9f;
        canvasGroup.blocksRaycasts = false; // Permite que eventos passem para objetos abaixo
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isSnapped)
        {
            // Move o objeto conforme o mouse/touch
            //Vector2 newPosition = rectTransform.anchoredPosition + eventData.delta / canvas.scaleFactor;

            NP = rectTransform.anchoredPosition + new Vector2(
            eventData.delta.x / (canvas.scaleFactor * 1.04f),
            eventData.delta.y / (canvas.scaleFactor * 2.19f)
            );

            if(((NP.x > -300.7479) && (NP.y > -49.2)) && ((NP.x < 297.3465) && (NP.y < 50.05504)))
            newPosition = rectTransform.anchoredPosition + new Vector2(
            eventData.delta.x / (canvas.scaleFactor * 1.04f),
            eventData.delta.y / (canvas.scaleFactor * 2.19f)
            );

            if (IsInsideCanvas(newPosition))
            {
                rectTransform.anchoredPosition = newPosition;
            }
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // Restaura a visibilidade ao soltar o objeto
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        List<RaycastResult> results = new List<RaycastResult>();
        PointerEventData pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = eventData.position
        };
        EventSystem.current.RaycastAll(pointerEventData, results);
        foreach (var result in results)
        {
            if (result.gameObject != gameObject && result.gameObject.CompareTag("SnapTarget"))
            {
                rectTransform.position = result.gameObject.transform.position;
                isSnapped = true;
                snappedTarget = result.gameObject;
                //Hability2Use.CheckAllPositions();
                if (!close) { close = true; }
                return;
            }
        }
    }

    public bool IsCorrectlyPlaced()
    {
        return snappedTarget != null && correctPositions.Contains(snappedTarget);
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