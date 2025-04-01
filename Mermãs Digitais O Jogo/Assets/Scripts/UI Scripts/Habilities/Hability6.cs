using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hability6 : MonoBehaviour, IPointerClickHandler
{
    public GameObject uiCanvas;
    public GameObject ship;
    [SerializeField] private Image rightAnswer;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiCanvas != null)
        {
            uiCanvas.gameObject.SetActive(false);
            Destroy(ship.gameObject);
        }
    }
}
