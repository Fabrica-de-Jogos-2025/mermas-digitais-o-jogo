using UnityEngine;


public class hab_icon_shipt : MonoBehaviour
{
    public GameObject habilityUI;
    public Hability1Use v1;
    public Hability1Use v2;
    private bool playerInside = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = true;

            if (v1.destroy || v2.destroy)
                habilityUI.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInside = false;
            habilityUI.SetActive(false);
        }
    }

        private void Update()
    {
        if (playerInside && (v1.destroy || v2.destroy))
        {
            habilityUI.SetActive(true);
        }
    }
}

