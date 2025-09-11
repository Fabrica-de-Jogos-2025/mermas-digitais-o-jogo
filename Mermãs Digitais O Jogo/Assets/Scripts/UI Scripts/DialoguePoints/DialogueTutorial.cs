using UnityEngine;
// using static TutorialTrigger;

public class TutorialTrigger : MonoBehaviour
{
    public enum TutorialType
    {
        Introduction,
        Jump,
        PowerUp
    }

    [TextArea(3, 10)]
    public string[] dialogueMessages;
    public bool triggerOnce = true;
    private bool startDialogue = true;

    [Header("UI do Tutorial")]
    public GameObject uiTutorial;
    
    public Collider2D tutorialColliders;
    private KeyCode[] teclasTutorial;

    private bool tutorialAtivo;
//    public bool tutorialJumpAtivo = false;
    public TutorialType tutorialType;
    public PlayerMovement PM;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            //RobotDialogue robot = FindObjectOfType<RobotDialogue>();
            RobotDialogue robot = FindFirstObjectByType<RobotDialogue>();
            PlayerMovement player = other.GetComponent<PlayerMovement>();
            
            if(robot != null && !player.IsJumping && startDialogue)
            {
                DefinirTeclas();
                robot.OnDialogueEnd += MostrarTutorial;
                robot.StartDialogue(dialogueMessages, player);

                if(triggerOnce)
                {
                    startDialogue = false;
                }

                if (tutorialType == TutorialType.Jump)
                {
                    //tutorialJumpAtivo = true;
                    // robot.OnDialogueEnd += () => { PM.TutoJumpAtiv = true; };
                    PlayerMovement playerMov = other.GetComponent<PlayerMovement>();
                    if (playerMov != null)
                    {
                        // Ativa só depois do diálogo do robô terminar
                        robot.OnDialogueEnd += () => { playerMov.TutoJumpAtiv = true; };
                    }
                }


                if (tutorialType == TutorialType.PowerUp)
                {
                    // aqui destru�mos o power-up do tutorial
                    Destroy(gameObject.GetComponent<Collider2D>()); // desabilita o colisor
                                                                    // opcional: tamb�m pode desativar visualmente
                    GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
    }

    void DefinirTeclas()
    {
        switch (tutorialType)
        {
            case TutorialType.Introduction:
                teclasTutorial = new KeyCode[] { KeyCode.A, KeyCode.D, KeyCode.LeftArrow, KeyCode.RightArrow };
                break;
            case TutorialType.Jump:
                teclasTutorial = new KeyCode[] { KeyCode.W, KeyCode.UpArrow };
                break;
            case TutorialType.PowerUp:
                teclasTutorial = new KeyCode[] { KeyCode.Space };
                break;
            default:
                break;
        }
    }

    void MostrarTutorial()
    {
        uiTutorial.SetActive(true);
        tutorialAtivo = true;
    }

    void Update()
    {
        if (tutorialAtivo && teclasTutorial != null)
        {
            foreach (var tecla in teclasTutorial)
            {
                if (Input.GetKey(tecla))
                {
                    Destroy(uiTutorial.gameObject);
                    tutorialAtivo = false;

                    if (triggerOnce)
                    {
                        Destroy(gameObject);
                    }
                    break;
                }
            }
        }
    }
}