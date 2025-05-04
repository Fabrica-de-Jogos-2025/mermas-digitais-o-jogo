using UnityEngine;

public class DialogueHability : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueMessages;
    public bool pausarJogador = false;
    public Sprite spriteCharacter;
    public string nameofCharacter;

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

    private void Update()
    {
        if (playerInTrigger && Input.GetKeyDown(KeyCode.X))
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
            }
        }
    }
}
