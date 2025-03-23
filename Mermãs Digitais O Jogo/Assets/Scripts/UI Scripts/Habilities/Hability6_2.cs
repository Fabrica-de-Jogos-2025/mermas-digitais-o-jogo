using UnityEngine;

public class Hability6_2 : MonoBehaviour
{
    public bool UsoDaUltimaHabilidade = false;
    
    private bool playerInTrigger = false;
    private PlayerMovement player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            player = other.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
        }
    }

    void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.H))
        {
            UsoDaUltimaHabilidade = true;
            Destroy(this.gameObject);
        }
        
    }
}
