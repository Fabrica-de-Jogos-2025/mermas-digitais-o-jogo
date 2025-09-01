using UnityEngine;
using UnityEngine.InputSystem;


public class hab_icon : MonoBehaviour
{
    public GameObject habilityUITutorial;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            habilityUITutorial.SetActive(true);

            if (Input.GetKeyDown(KeyCode.H))
            {
                habilityUITutorial.SetActive(false);
                //Destroy(habilityUITutorial.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            habilityUITutorial.SetActive(false);
        }
    }
}
