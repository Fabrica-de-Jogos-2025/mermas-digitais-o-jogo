using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hability6_2 : MonoBehaviour, IPointerClickHandler
{
    public GameObject uiCanvas;
    public GameObject MetalGrid, ColliderHability;
    [SerializeField] private Image rightAnswer;
    public bool UsoDaUltimaHabilidade = false;
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (uiCanvas != null)
        {
            UsoDaUltimaHabilidade = true;
            uiCanvas.gameObject.SetActive(false);
            ColliderHability.gameObject.SetActive(false);;
            Destroy(MetalGrid.gameObject);
        }
    }
}