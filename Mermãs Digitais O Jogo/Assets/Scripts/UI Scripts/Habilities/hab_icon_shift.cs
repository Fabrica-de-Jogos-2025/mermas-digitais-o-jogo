using UnityEngine;
using UnityEngine.InputSystem;


public class hab_icon_shipt : MonoBehaviour
{
    public GameObject habilityUI;
    public Hability1Use v1;
    public Hability1Use v2;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (v1.destroy || v2.destroy))
        {
            habilityUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && (v1.destroy || v2.destroy))
        {
            habilityUI.SetActive(false);
        }
    }
}
