using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hability4_2 : MonoBehaviour
{
    [SerializeField] private GameObject habilityScreen;
    public bool pausarJogador = false;

    private bool playerInTrigger = false;
    private PlayerMovement player;

    public GameObject HabilityScreen { get => habilityScreen; set => habilityScreen = value; }

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

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.H))
        {
            // Ativa a tela de habilidade
            if (habilityScreen != null)
            {
                habilityScreen.SetActive(true);
            }
        }
    }
}
