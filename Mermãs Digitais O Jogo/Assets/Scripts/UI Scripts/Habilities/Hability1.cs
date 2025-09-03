using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Hability1 : MonoBehaviour
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
    public GameObject habilityUITutorial;
    //public Hability1Use validation, validation2;
    //public GameObject detroyer;
    private bool validation = true;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && validation)
        {
            playerInTrigger = true;
            player = other.GetComponent<PlayerMovement>();
            if (!status[0].PuzzleSolved && !status[1].PuzzleSolved)
            {
                habilityUITutorial.SetActive(true);
            }
            if (Input.GetKeyDown(KeyCode.H) || (status[0].PuzzleSolved && status[1].PuzzleSolved))
            {
                habilityUITutorial.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && validation)
        {
            playerInTrigger = false;
            habilityUITutorial.SetActive(false);
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
                habilityUITutorial.SetActive(false);
            }
        }
    }
}
