using UnityEngine;
using UnityEngine.InputSystem;

public class Hability1_1 : MonoBehaviour
{
    [TextArea(3, 10)]
    public string[] dialogueMessages;
    [SerializeField] private GameObject habilityScreen;
    [SerializeField] private GameObject[] images;
    // public bool pausarJogador = false;

    private bool playerInTrigger = false;
    private PlayerMovement player;
    // [SerializeField] private Hability1Use_1 [] status;
    [SerializeField] private bool deactivatedHabilityUI = false;

    public GameObject HabilityScreen { get => habilityScreen; set => habilityScreen = value; }
    public GameObject habilityUITutorial;

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
            if (!deactivatedHabilityUI)
            {
                habilityUITutorial.SetActive(true);
            }
            /*if (Input.GetKeyDown(KeyCode.H))
            {
                habilityUITutorial.SetActive(false);
                deactivatedHabilityUI = true;
            }*/
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            habilityUITutorial.SetActive(false);
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
            //RobotDialogue robot = FindFirstObjectByType<RobotDialogue>();

            /*if (robot != null && player != null)
            {
                //robot.StartDialogue(dialogueMessages, player);
                habilityScreen.SetActive(true);
                //pausarJogador = true;
            }*/

            // Desativa a tela de tutorial se ainda não foi desativada
            if (!deactivatedHabilityUI)
            {
                habilityUITutorial.SetActive(false);
                deactivatedHabilityUI = true;
            }

            // Ativa a tela de habilidade
            if (habilityScreen != null)
            {
                habilityScreen.SetActive(true);
                player.IsFrozen = true;
            }
        }
    }
}
