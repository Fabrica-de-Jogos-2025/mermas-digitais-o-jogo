using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class PlayerMovement : MonoBehaviour
{
    // [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isJumping;
    private bool isFrozen = false;
    public bool isPaused = false;
    private bool doorJustOpened = false;
    private bool pauseButtonHeld = false;
    private Vector2 direction;
    [SerializeField] private Door currentDoor;
    [SerializeField] private Padlock currentPadlock;
    public bool permissionDoor_a = false;
    private GameObject pauseScreen;
    [SerializeField] private PlayerStatus playerStatus;
//    [SerializeField] private TutorialTrigger trigger;
    public bool IsJumping
    {
        get { return isJumping; }
        set { isJumping = value; }
    }
    public float JumpForce
    {
        get { return jumpForce; }
        set { jumpForce = value; }
    }

    public Vector2 Direction
    {
        get { return direction; }
        set { direction = value; }
    }

    public bool IsFrozen { get => isFrozen; set => isFrozen = value; }
    public GameObject PauseScreen { get => pauseScreen; set => pauseScreen = value; }
    public bool IsPaused { get => isPaused; set => isPaused = value; }
    public Vector3 LastCheckpointPosition { get => lastCheckpointPosition; set => lastCheckpointPosition = value; }

    private Rigidbody2D rig;
    private GroundCheck groundChecked;
    private Vector3 lastCheckpointPosition;
    private TryAgainScreen yesButton;
    private Checkpoint checkpoint;
    [SerializeField] public GameplayAudio sfxAcess;
    [SerializeField] private GameplayAudio sfxDoorAcess;
    [SerializeField] private AudioClip walk;
    [SerializeField] private AudioClip walkSand;
    [SerializeField] private AudioClip jump;
    [SerializeField] private AudioClip door;

    private CoinManager coinManager;
    private Tilemap coinTilemap;
    public bool i = false;
    public bool TutoJumpAtiv = false;
    private bool hasPlayedJumpSound = false;

    public InputController controls;
    private InputAction move;
    private InputAction jumping;
    private InputAction pause;
    private InputAction doorOpen;

    private void Awake()
    {
        controls = new InputController();

        /*controls.Player.Move.performed += ctx => direction = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => direction = Vector2.zero;*/

        /*controls.Player.Move.performed += ctx =>
        {
            float value = ctx.ReadValue<float>();
            // Ajuste manualmente o direction.x conforme o botão
            if (ctx.control.name == "left")
                direction = Vector2.left * value;
            else if (ctx.control.name == "right")
                direction = Vector2.right * value;
        };
        controls.Player.Move.canceled += ctx => direction = Vector2.zero;*/
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Instantiate(this.gameObject);
        // DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnSceneLoaded;

        rig = GetComponent<Rigidbody2D>();
        groundChecked = GetComponentInChildren<GroundCheck>();
        lastCheckpointPosition = transform.position;

        checkpoint = FindFirstObjectByType<Checkpoint>();

        var pauseScreenController = GameObject.Find("CanvasPauseScreenController");
        pauseScreen = pauseScreenController.transform.Find("CanvasPauseScreen").gameObject;

        GameObject coinObj = GameObject.FindWithTag("Coin");

        if (coinObj != null)
        {
            coinManager = GameObject.FindWithTag("Coin").GetComponent<CoinManager>();
            coinTilemap = GameObject.FindWithTag("Coin").GetComponent<Tilemap>();
        }
        yesButton = GameObject.Find("Canvas").GetComponentInChildren<TryAgainScreen>();

        if (SceneManager.GetActiveScene().name != "Tutorial")
        {
            TutoJumpAtiv = true;
        }

        FindCoinReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen)
        {
            sfxAcess.StopAudio();
            return;
        }
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        // direction = move.ReadValue<Vector2>();
        // float horizontal = move.ReadValue<Vector2>().x;

        ReadMovementInput();
        OnMove();
        Jumping();
        CheckInGrounded();
        CheckForCoin();
        //PauseGame();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            isPaused = !isPaused;
            Time.timeScale = isPaused ? 0 : 1;
            pauseScreen.SetActive(isPaused);
        }

        bool keyboardDoor = Input.GetKeyDown(KeyCode.RightShift);
        bool doorPressed = doorOpen.ReadValue<float>() > 0.1f;
        bool doorControl = keyboardDoor || doorPressed;
        if (doorControl)
        {
            EnterDoor();
        }
    }

    private void ReadMovementInput()
    {
        // 🔹 Se o jogador NÃO estiver movendo o controle
        // então lê o teclado normalmente
        if (Mathf.Abs(direction.x) < 0.1f)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            if (Mathf.Abs(horizontal) > 0.1f || Mathf.Abs(vertical) > 0.1f)
                direction = new Vector2(horizontal, vertical);
            else
                direction = Vector2.zero;
        }
    }

    void OnMove()
    {
        if (isFrozen) return;

        float keyboardInput = Input.GetAxis("Horizontal");
        float controllerInput = move.ReadValue<Vector2>().x;

        float horizontal = Mathf.Abs(controllerInput) > Mathf.Abs(keyboardInput)
        ? controllerInput
        : keyboardInput;

        Vector3 movement = new Vector3(horizontal, 0f, 0f);
        // transform.position += movement * Time.deltaTime * playerSpeed;

        // float rotation = Input.GetAxis("Horizontal");
        // movement.x = move.ReadValue<float>();

        if (Mathf.Abs(horizontal) > 0.1f && groundChecked.IsGrounded())
        {
            string cena = SceneManager.GetActiveScene().name;

            if (cena == "Fase 2" || cena == "Boss Fase 2")
            {
                sfxAcess.LoopAudio(walkSand);
            } else
                sfxAcess.LoopAudio(walk);
        }
        else
        {
            sfxAcess.StopAudio();
        }

        transform.position += movement * Time.deltaTime * playerSpeed;

        if (horizontal > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }

        if (horizontal < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }

    void Jumping()
    {
        if (isFrozen) return;

        bool keyboardJump = Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);

        // float controllerJump = jumping.ReadValue<float>();
        // jumpForce = jumping.ReadValue<float>();
        bool controllerPressed = jumping.ReadValue<float>() >= 0.3f; // se o valor for maior que 0.5, considera pressionado
        // Debug.Log(controllerJump);
        // 🔹 Escolhe o input ativo (teclado OU controle)
        bool jumpPressed = keyboardJump || controllerPressed;
        if (jumpPressed && !isJumping && TutoJumpAtiv) //trigger.tutorialJumpAtivo
        {
            // rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
            rig.linearVelocity = new Vector2(rig.linearVelocity.x, 0f);
            rig.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
            if (!hasPlayedJumpSound)
            {
                sfxAcess.Audio(jump);
                hasPlayedJumpSound = true;
            }
        }
    }

    void CheckInGrounded()
    {
        // isJumping = !groundChecked.IsGrounded();
        // sfxAcess.StopAudio();
        bool grounded = groundChecked.IsGrounded();

        if (grounded && isJumping)
        {
            // Ao tocar o chão novamente, libera o som do próximo salto
            hasPlayedJumpSound = false;
        }

        isJumping = !grounded;
    }

    private void FindCoinReferences()
    {
        GameObject coinObj = GameObject.FindWithTag("Coin");
        if (coinObj != null)
        {
            coinManager = coinObj.GetComponent<CoinManager>();
            coinTilemap = coinObj.GetComponent<Tilemap>();
        }
        else
        {
            coinManager = null;
            coinTilemap = null;
        }
    }

    void OnEnable()
    {
        move = controls.Player.Move;
        move.Enable();

        jumping = controls.Player.Jump;
        jumping.Enable();

        pause = controls.Player.Pause;
        pause.Enable();

        doorOpen = controls.Player.EnterDoor;
        doorOpen.Enable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        move.Disable();
        jumping.Disable();
        pause.Disable();
        doorOpen.Disable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool checkpointAtivo = PlayerPrefs.GetInt("CheckpointAtivo", 0) == 1;
        //Respawn(checkpointAtivo);
        // Sempre que mudar de cena, procura de novo o CoinManager e Tilemap
        FindCoinReferences();
    }

    private void CheckForCoin()
    {
        if (coinTilemap == null) return;

        Vector3Int cellPosition = coinTilemap.WorldToCell(transform.position);

        if (coinTilemap.HasTile(cellPosition)) // Se houver moeda nessa posição
        {
            coinTilemap.SetTile(cellPosition, null); // Remove a moeda
            coinManager?.AddCoin(); // Atualiza o contador
        }
    }

    public void EnterDoor()
    {

        if (currentDoor != null && !doorJustOpened)
        {
            doorJustOpened = true;
            currentDoor.PlayAnimation();
            sfxAcess.Audio(door);
            StartCoroutine(TeleportAfterAnimation());
        }
    }

    private IEnumerator TeleportAfterAnimation()
    {
        yield return new WaitForSeconds(0.8f);
        if (currentDoor != null && i)
        {
            transform.position = currentDoor.GetExitPosition();
        }
    }

    public void SetLastCheckpoint(Vector2 newCheckpoint)
    {
        lastCheckpointPosition = newCheckpoint;
    }

    public void Respawn(bool checkpointAtivo)
    {
        // rig.linearVelocity = Vector3.zero;

        if (checkpointAtivo && PlayerPrefs.GetInt("CheckpointAtivo", 0) == 1)
        {
            float x = PlayerPrefs.GetFloat("CheckpointX", 0f);
            float y = PlayerPrefs.GetFloat("CheckpointY", 0f);
            float z = PlayerPrefs.GetFloat("CheckpointZ", 0f);
            transform.position = new Vector3(x, y, z);
        }
        else
        {
            SpawnerController respawn = GameObject.Find("RespawnManagerPlayer").GetComponent<SpawnerController>();
            transform.position = respawn.PlayerSpawnPoint.transform.position;
            // transform.position = Vector2.zero; // posição inicial da fase
        }

        playerStatus.PlayerLife = playerStatus.Hearts.Length;
        foreach (var heart in playerStatus.Hearts)
        {
            heart.enabled = true;
        }

        FreezePlayer(false);

        if (playerStatus.Robot != null)
        {
            playerStatus.Robot.gameObject.SetActive(true);
            playerStatus.gameObject.SetActive(true);
            playerStatus.Robot.IsDead = false;
        }
    }

    public void FreezePlayer(bool freeze)
    {
        isFrozen = freeze;

        if (freeze)
        {
            sfxAcess.StopAudio();
            rig.linearVelocity = Vector2.zero;
            rig.simulated = false;
        }
        else
        {
            rig.simulated = true;
        }
    }

    public void PauseGame()
    {
        bool keyboardPause = Input.GetKeyDown(KeyCode.KeypadEnter);

        float controllerValue = pause.ReadValue<float>();
        bool controllerPressed = controllerValue >= 0.5f;
        bool pausePressed = keyboardPause || (controllerPressed && !pauseButtonHeld);
        pauseButtonHeld = controllerPressed;

        /*if (pausePressed && !isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else if (isPaused && pausePressed)
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
        }*/

        if (pausePressed)
        {
            isPaused = !isPaused;

            Time.timeScale = isPaused ? 0 : 1;
            pauseScreen.SetActive(isPaused);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            currentDoor = collision.GetComponent<Door>();
        }

        if (playerStatus.PlayerLife <= 0)
        {
            SceneManager.LoadScene("Morte_Falha");

            if (yesButton != null && yesButton.YesClicked)
            {
                Respawn(checkpoint.IsActivated);
                yesButton.YesClicked = false;
                playerStatus.Robot.IsDead = false;
            }
        }
    
}

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            currentDoor = null;
            doorJustOpened = false;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Platform")
        {
            transform.SetParent(collision.transform);
            DontDestroyOnLoad(this.gameObject);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "Platform")
        {
            transform.SetParent(null);
            DontDestroyOnLoad(this.gameObject);
        }
    }
    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
