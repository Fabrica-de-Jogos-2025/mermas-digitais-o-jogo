using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class Hability2OpenAndClose_2 : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueMessages;
    [SerializeField] private GameObject habilityScreen;
    [SerializeField] private GameObject[] images;
    public bool pausarJogador = false;

    private bool playerInTrigger = false;
    private PlayerMovement player;


    public GameObject HabilityScreen { get => habilityScreen; set => habilityScreen = value; }
    public AudioClip Feedback { get => feedback; set => feedback = value; }

    public Hability2_2 CLOSE_1, CLOSE_2, CLOSE_3, CLOSE_4;
    public bool destroy = false;
    public GameObject habilityUITutorial;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip hability;
    [SerializeField] private AudioClip feedback;

    private bool feedbackPlayed = false;
    private bool habilityJustOpened = false;

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
            habilityUITutorial.SetActive(true);

            if (Input.GetKeyDown(KeyCode.H)) {
                habilityUITutorial.SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            habilityUITutorial.SetActive(false);
            habilityJustOpened = false;
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

            if (robot != null && player != null && !player.IsJumping && !habilityJustOpened)
            {
                //robot.StartDialogue(dialogueMessages, player);
                habilityJustOpened = true;
                habilityScreen.SetActive(true);
                player.IsFrozen = true;
                sfxAcess.Audio(hability);
                //pausarJogador = true;
            }
        }
        else if (CLOSE_1.close_1 && CLOSE_2.close_1 && CLOSE_3.close_1 && CLOSE_4.close_1 && !feedbackPlayed)
        {
            StartCoroutine(HandlePuzzleSolved());
            //pausarJogador = false;
        }
    }
    
    private IEnumerator HandlePuzzleSolved()
    {
        sfxAcess.Audio(feedback);
        yield return new WaitForSeconds(feedback.length);
        feedbackPlayed = true;
        player.IsFrozen = false;
        destroy = true;
        habilityScreen.SetActive(false);
    }
}
