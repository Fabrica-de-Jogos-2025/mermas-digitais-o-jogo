using UnityEngine;

public class DialogueTutorial2 : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueMessages;
    public bool triggerOnce = true;
    public bool pausarJogador = false;
    public Sprite spriteCharacter;
    public string nameofCharacter;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //RobotDialogue robot = FindObjectOfType<RobotDialogue>();
            RobotDialogue robot = FindFirstObjectByType<RobotDialogue>();
            PlayerMovement player = other.GetComponent<PlayerMovement>();

            if (robot != null)
            {
                if (spriteCharacter != null)
                    robot.imageRobot.sprite = spriteCharacter;

                if (!string.IsNullOrEmpty(nameofCharacter))
                    robot.name.text = nameofCharacter;

                robot.StartDialogue(dialogueMessages, player);
                

                if (triggerOnce)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
