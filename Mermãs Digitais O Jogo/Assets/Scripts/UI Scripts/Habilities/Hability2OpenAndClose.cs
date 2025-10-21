using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Hability2OpenAndClose : MonoBehaviour
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
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip hability;
    [SerializeField] private AudioClip feedback;
    public GameObject HabilityScreen { get => habilityScreen; set => habilityScreen = value; }
    public AudioClip Feedback { get => feedback; set => feedback = value; }

    public Hability2 CLOSE_1, CLOSE_2, CLOSE_3, CLOSE_4;
    public bool destroy = false;
    private bool feedbackPlayed = false;

    public InputController controls;
    private InputAction openHability;

    private void Awake()
    {
        controls = new InputController();
    }

    private void OnEnable()
    {
        openHability = controls.Player.UseHability;
        openHability.Enable();
    }

    private void OnDisable()
    {
        openHability.Disable();
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
        bool keyboardHability = Input.GetKeyDown(KeyCode.H);
        bool habilityPressed = openHability.ReadValue<float>() > 0.1f;
        bool habilityControl = keyboardHability || habilityPressed;
        if (playerInTrigger && habilityControl)
        {
            //RobotDialogue robot = FindObjectOfType<RobotDialogue>();
            RobotDialogue robot = FindFirstObjectByType<RobotDialogue>();

            if (robot != null && player != null && !player.IsJumping)
            {
                if (spriteCharacter != null)
                    robot.imageRobot.sprite = spriteCharacter;

                if (!string.IsNullOrEmpty(nameofCharacter))
                    robot.characterNameText.text = nameofCharacter;

                robot.StartDialogue(dialogueMessages, player);
                habilityScreen.SetActive(true);
                sfxAcess.Audio(hability);
            }
        }
            else if (CLOSE_1.close && CLOSE_2.close && CLOSE_3.close && CLOSE_4.close && !feedbackPlayed)
            {
            // sfxAcess.Audio(feedback);
                feedbackPlayed = true;
                sfxAcess.Audio(feedback);
                habilityScreen.SetActive(false);
                destroy = true;
            }
    }
}
