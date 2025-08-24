using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Hab_1_Sem_UI : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueMessages;
    [SerializeField] private GameObject habilityScreen;
    [SerializeField] private GameObject[] images;
    public bool pausarJogador = false;
    public Sprite spriteCharacter;
    public string nameofCharacter;

    private bool playerInTrigger = false;
    private PlayerMovement player;
    [SerializeField] private Hability1Use [] status;

    public GameObject HabilityScreen { get => habilityScreen; set => habilityScreen = value; }
    private bool validation = true;

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
        /*if (validation.puzzleSolved && validation2.puzzleSolved)
        {
            Destroy(detroyer.gameObject);
            validation.puzzleSolved = false;
            validation2.puzzleSolved = false;
        }*/


        if (playerInTrigger && Input.GetKeyDown(KeyCode.H) && validation/* && !validation.puzzleSolved && !validation2.puzzleSolved*/)
        {
            //RobotDialogue robot = FindObjectOfType<RobotDialogue>();
            RobotDialogue robot = FindFirstObjectByType<RobotDialogue>();

            if (robot != null && player != null && !player.IsJumping)
            {
                if (spriteCharacter != null)
                    robot.imageRobot.sprite = spriteCharacter;

                if (!string.IsNullOrEmpty(nameofCharacter))
                    robot.name.text = nameofCharacter;

                robot.StartDialogue(dialogueMessages, player);
                habilityScreen.SetActive(true);
                validation = false;
            }
        }
        else if (playerInTrigger && Input.GetKeyDown(KeyCode.H) && !validation)
        { 
            habilityScreen.SetActive(true);
        }
    }
}
