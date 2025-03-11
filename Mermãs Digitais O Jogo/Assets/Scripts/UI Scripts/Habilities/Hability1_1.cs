using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Hability1_1 : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueMessages;
    [SerializeField] private GameObject habilityScreen;
    [SerializeField] private GameObject[] images;
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
            RobotDialogue robot = FindObjectOfType<RobotDialogue>();

            if (robot != null && player != null && !player.IsJumping)
            {
                //robot.StartDialogue(dialogueMessages, player);
                habilityScreen.SetActive(true);
            }
        }
    }
}
