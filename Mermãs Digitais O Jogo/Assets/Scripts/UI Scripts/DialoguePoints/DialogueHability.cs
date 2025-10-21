using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueHability : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueMessages;
    public bool pausarJogador = false;
    public Sprite spriteCharacter;
    
    public string nameofCharacter;
    public GameObject uiDialogueTutorial;

    private bool playerInTrigger = false;
    private PlayerMovement player;
    public bool permissionDialogueFigure = true;

    private InputController controls;
    private InputAction dialog;

    private void Awake()
    {
        controls = new InputController();
    }

    private void OnEnable()
    {
        dialog = controls.Player.StartDialogue;
        dialog.Enable();
    }

    private void OnDisable()
    {
        dialog.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && permissionDialogueFigure)
        {
            playerInTrigger = true;
            player = other.GetComponent<PlayerMovement>();
            uiDialogueTutorial.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && permissionDialogueFigure)
        {
            playerInTrigger = false;
            uiDialogueTutorial.SetActive(false);
        }
    }

    private void Update()
    {
        bool keyboardDialog = Input.GetKeyDown(KeyCode.X);
        bool dialogPressed = dialog.ReadValue<float>() > 0.1f;
        bool dialogControl = keyboardDialog || dialogPressed;
        if (playerInTrigger && dialogControl && permissionDialogueFigure)
        {
            //RobotDialogue robot = FindObjectOfType<RobotDialogue>();
            RobotDialogue robot = FindFirstObjectByType<RobotDialogue>();

            if (robot != null && player != null && !player.IsJumping)
            {
                if (spriteCharacter != null) { 
                robot.imageRobot.sprite = spriteCharacter;
                robot.imageRobot.transform.localScale = new Vector3(0.5f, 1f, 0f);
            }


                if (!string.IsNullOrEmpty(nameofCharacter))
                    robot.characterNameText.text = nameofCharacter;

                robot.StartDialogue(dialogueMessages, player);
            }
        }
    }
}
