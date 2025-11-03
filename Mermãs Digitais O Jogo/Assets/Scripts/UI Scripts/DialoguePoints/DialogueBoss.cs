using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class DialogueBoss : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueMessages;
    public Sprite spriteCharacter;
    public string nameofCharacter;

    private bool playerInTrigger = false;
    private PlayerMovement player;
    public bool permissionDialogueFigure = true;

    private InputController controls;
    private InputAction dialog;

    private RobotDialogue robot;
    public BossAttack bossAttack;
    public GameObject Dialogo;
    private bool dialogoExecutado = false;

    private void Awake()
    {
        if (robot == null)
        {
            GameObject go = GameObject.FindGameObjectWithTag("Robot");
            if (go != null)
                robot = go.GetComponent<RobotDialogue>();
        }
        
        robot.dialogoExecutadoNoBoss = true;

        if (bossAttack == null)
            bossAttack = FindObjectOfType<BossAttack>();

        // Procura os componentes internos via código
        if (robot.imageRobot == null)
            robot.imageRobot = robot.GetComponentInChildren<Image>();

        if (robot.characterNameText == null)
            robot.characterNameText = robot.GetComponentInChildren<Text>();

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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && permissionDialogueFigure)
        {
            playerInTrigger = false;
        }
    }

    private void Update()
    {
        if ((Dialogo.gameObject.activeSelf == false) && dialogoExecutado)
        {
            bossAttack.iniciarBossFight = true;
            Destroy(this.gameObject);
        }
        else if (playerInTrigger && permissionDialogueFigure)
        {
            if (robot != null && player != null && !player.IsJumping)
            {
                if (spriteCharacter != null && robot.imageRobot != null)
                { 
                    robot.imageRobot.sprite = spriteCharacter;
                    robot.imageRobot.transform.localScale = new Vector3(0.5f, 1f, 1f);
                }

                if (!string.IsNullOrEmpty(nameofCharacter) && robot.characterNameText != null)
                {
                    robot.characterNameText.text = nameofCharacter;
                }
                robot.StartDialogue(dialogueMessages, player);
                dialogoExecutado = true;
            }
        }
    }
}
