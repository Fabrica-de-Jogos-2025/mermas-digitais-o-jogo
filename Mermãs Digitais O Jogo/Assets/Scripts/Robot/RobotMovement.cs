using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class RobotMovement : MonoBehaviour
{
    // [SerializeField] private GameObject robotPrefab;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float Speed;
    [SerializeField] private float StoppingDistance;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip fly;

    [SerializeField] private PlayerStatus playerStatus;
    private Transform Target;
    //private RobotPowerUp robotPowered;
    //public bool HasPowerUp { get => hasPowerUp; set => hasPowerUp = value; }
    public bool HasPowerUp;
    private static Vector3 respawnpoint;
    private bool isUsingPowerUp = false;
    private bool isFreeze = false;
    private bool isDead = false;
    public static Vector3 Respawnpoint 
    { 
      get { return respawnpoint; } 
      set { respawnpoint = value; } 
    }

    public bool IsDead { get => isDead; set => isDead = value; }
    public PlayerMovement Player { get => player; set => player = value; }

    public float direction;
    public Vector3 moviment;
    private Rigidbody2D rig;

    public InputController controls;
    private InputAction powerUp;

    private void Awake()
    {
        controls = new InputController();
    }

    private void OnEnable()
    {
        powerUp = controls.Player.Attack;
        powerUp.Enable();
    }

    private void OnDisable()
    {
        powerUp.Disable();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // DontDestroyOnLoad(this.gameObject);
        // Instantiate(this.gameObject);

        playerStatus = GetComponent<PlayerStatus>();
        //robotPowered = FindAnyObjectByType<RobotPowerUp>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        rig = GetComponent<Rigidbody2D>();

        Player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isDead) {
            if (!isUsingPowerUp && Vector2.Distance(transform.position, Target.position) >= StoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target.position + moviment, Speed * Time.deltaTime);
            }

            float rotation = Input.GetAxis("Horizontal");

            if (rotation > 0 && !player.IsFrozen)
            {
                transform.eulerAngles = new Vector2(0f, 0f);
                moviment = new Vector3(-1f, 0.5f, 0f);
            } else if (rotation < 0 && !player.IsFrozen)
            {
                transform.eulerAngles = new Vector2(0f, 180f);
                moviment = new Vector3(1f, 0.5f, 0f);
            } else if (player.IsFrozen)
            {
                IsPaused(true);
            }

            if (Vector2.Distance(transform.position, Target.position) <= 2)
            {
                PowerUp();
            }
        }
    }

    public void Die()
    {
        gameObject.SetActive(false);
        isDead = true;
    }

    void PowerUp()
    {
        bool keyboardJump = Input.GetKeyDown(KeyCode.Space);
        bool powerUpPressed = powerUp.ReadValue<float>() > 0f;
        bool powerUpControl = keyboardJump || powerUpPressed;
        if (HasPowerUp && !isUsingPowerUp)
        {
            if (powerUpControl)
            {
                StartCoroutine(UsePowerUp());
            }
        }
    }

    public void IsPaused(bool isPaused)
    {
        isPaused = isFreeze;

        if (isFreeze)
        {
            rig.linearVelocity = Vector2.zero;
            rig.simulated = false;
        }
        else
        {
            rig.simulated = true;
        }
    }

    IEnumerator UsePowerUp()
    {
        isUsingPowerUp = true;
        //float direction = player.transform.localScale.x;
        direction = player.transform.position.x - transform.position.x;
        float maxDistance;
        //Vector3 originalPosition = transform.position;


        if (((direction > 0) && moviment.x == -1) || moviment.x == -1)
        {
            maxDistance = Camera.main.ViewportToWorldPoint(new Vector3(0.75f, 0.5f, 0)).x;

            float Y = player.transform.position.y;
            
            while (direction > 0 && transform.position.x < maxDistance)
            {
                //transform.position += Vector3.right * Mathf.Sign(direction) * Speed * Time.deltaTime; // Move o robo para a direita

                float platformY = player.transform.position.y + 0.479862f; // Acompanha a altura da plataforma via personagem

                transform.position = new Vector3(
                    transform.position.x + Mathf.Sign(direction) * Speed * Time.deltaTime,
                    platformY, // Robô segue a altura da plataforma
                    transform.position.z
                );


                Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 1f);
                foreach (Collider2D enemy in enemies)
                {
                    if (enemy.CompareTag("Enemy"))
                    {
                        Destroy(enemy.gameObject);
                    }
                }

                yield return null;
            }

        } else if (((direction <= 0) && moviment.x == 1) || moviment.x == 1)
        {
            maxDistance = Camera.main.ViewportToWorldPoint(new Vector3(0.25f, 0.5f, 0)).x;

            while (direction <= 0 && transform.position.x > maxDistance)
            {
                //transform.position += Vector3.left * Mathf.Sign(-direction) * Speed * Time.deltaTime; // Move o robo para a esquerda

                float platformY = player.transform.position.y + 0.479862f; // Acompanha a altura da plataforma via personagem

                transform.position = new Vector3(
                    transform.position.x + Mathf.Sign(direction) * Speed * Time.deltaTime,
                    platformY, // Robô segue a altura da plataforma
                    transform.position.z
                );


                Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 1f);
                foreach (Collider2D enemy in enemies)
                {
                    if (enemy.CompareTag("Enemy"))
                    {
                        Destroy(enemy.gameObject);
                    }
                }

                yield return null;
            }

        }

        isUsingPowerUp = false;
    }

    public void SetDialogueState(bool inDialogue)
    {
        if (inDialogue)
        {
            sfxAcess.StopAudio(); // pausa o som de voo
        }
        else
        {
            sfxAcess.LoopAudio(fly); // retoma o som ao fim do diálogo
        }
    }
}
