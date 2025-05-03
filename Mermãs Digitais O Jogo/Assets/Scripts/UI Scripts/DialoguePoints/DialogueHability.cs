using UnityEngine;

public class DialogueHability : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueMessages;
    public bool pausarJogador = false;
    private RobotDialogue robotSprite;
    public Sprite spriteCharacter;
    public string nameofCharacter;

    private bool playerInTrigger = false;
    private PlayerMovement player;

    private void Start()
    {
        robotSprite = FindFirstObjectByType<RobotDialogue>();

        if (robotSprite != null && spriteCharacter != null && nameofCharacter != null) {
            robotSprite.imageRobot.sprite = spriteCharacter;
            robotSprite.name.text = nameofCharacter;
        }
    }

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
        if (playerInTrigger && Input.GetKeyDown(KeyCode.X))
        {
            //RobotDialogue robot = FindObjectOfType<RobotDialogue>();
            RobotDialogue robot = FindFirstObjectByType<RobotDialogue>();

            if (robot != null && player != null && !player.IsJumping)
            {
                robot.StartDialogue(dialogueMessages, player);
            }
        }
    }
}
