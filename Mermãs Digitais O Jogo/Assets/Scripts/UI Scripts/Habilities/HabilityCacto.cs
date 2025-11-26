using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class HabilityCacto : MonoBehaviour
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

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip hability;

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
        if (other.CompareTag("Player") && validation)
        {
            playerInTrigger = true;
            player = other.GetComponent<PlayerMovement>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && validation)
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
        bool keyboardHability = Input.GetKeyDown(KeyCode.H);
        bool habilityPressed = openHability.ReadValue<float>() > 0.1f;
        bool habilityControl = keyboardHability || habilityPressed;

        if (playerInTrigger && habilityControl && validation/* && !validation.puzzleSolved && !validation2.puzzleSolved*/)
        {
            //RobotDialogue robot = FindObjectOfType<RobotDialogue>();
            RobotDialogue robot = FindFirstObjectByType<RobotDialogue>();

            if (robot != null && player != null && !player.IsJumping)
            {
                if (spriteCharacter != null)
                {
                    robot.imageRobot.sprite = spriteCharacter;

                    if (nameofCharacter == "Robo" || nameofCharacter == "Robô")
                    {
                        // Mantém a escala normal
                        robot.imageRobot.transform.localScale = Vector3.one;
                    }
                    else
                    {
                        // Ajuste de escala para outros personagens
                        robot.imageRobot.transform.localScale = new Vector3(0.5f, 1f, 1f);
                    }
                }

                if (!string.IsNullOrEmpty(nameofCharacter))
                    robot.characterNameText.text = nameofCharacter;

                robot.StartDialogue(dialogueMessages, player);
                habilityScreen.SetActive(true);
                sfxAcess.Audio(hability);
                validation = false;
            }
        }
    }
}
