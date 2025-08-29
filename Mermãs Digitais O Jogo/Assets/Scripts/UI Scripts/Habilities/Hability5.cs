using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

public class Hability5 : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    [SerializeField] private TMP_InputField numberInput;
    private RectTransform rectTransform;
    private Canvas canvas;
    private CanvasGroup canvasGroup;
    private RectTransform canvasRect;
    private bool isSnapped = false;
    public bool puzzleSolved = false;

    public static List<Hability5> allDraggableObjects = new List<Hability5>();
    public List<GameObject> correctPositions; // Lista de posições corretas
    private GameObject snappedTarget;
    public bool close, allCorrect;
    public bool i = false;
    [SerializeField] private GameObject rocket; // Referência ao foguete
    [SerializeField] private GameObject rocketCollider; // Collider do foguete
    
    private Vector2 NP, newPosition;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>(); // Garante que tenha um Canvas como referência
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

        // Torna o objeto transparente ao começar o arraste
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
            eventData.delta.x / (canvas.scaleFactor * 1.22772f),
            eventData.delta.y / (canvas.scaleFactor * 2.5853f)
            );

            if(((NP.x > -300.7479) && (NP.y > -49.2)) && ((NP.x < 297.3465) && (NP.y < 50.05504)))
            newPosition = rectTransform.anchoredPosition + new Vector2(
            eventData.delta.x / (canvas.scaleFactor * 1.22772f),
            eventData.delta.y / (canvas.scaleFactor * 2.5853f)
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
                
                if (!close)
                {
                    close = true;
                    CheckAllPositions();
                }
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

    private void CheckAllPositions()
    {
        // Verifica se todos os objetos estão corretamente posicionados
        allCorrect = true;
        foreach (Hability5 obj in allDraggableObjects)
        {
            if (!obj.IsCorrectlyPlaced())
            {
                allCorrect = false;
                break;
            }
        }
    }

    void Start()
    {
        //numberInput.characterValidation = TMP_InputField.CharacterValidation.Integer;
    }

    public void NumberCaption() {
        if (int.TryParse(numberInput.text, out int number))
        {
            if (number > 0)
            {
                i = true;
                puzzleSolved = true;
            }
        } 
    }
}
