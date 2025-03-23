using UnityEngine;

public class Colision : MonoBehaviour
{
    public PlatformType2 P;
    public Hability6_2 H;
    private bool playerInTrigger = false;
    private PlayerMovement player;
    public bool valid = false;

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
        if (playerInTrigger)
        {
            if (P.v == false)
            {
                P.v = true;
            }

            if (H.UsoDaUltimaHabilidade)
            {
                P.verif2 = true;
            }

            if (valid == false)
            {
                valid = true;
            }
        }
    }
            
}
