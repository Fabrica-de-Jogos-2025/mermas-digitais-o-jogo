using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;


public class PlayerMovement : MonoBehaviour
{
    // [SerializeField] private GameObject playerPrefab;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isJumping;
    private bool isFrozen = false;
    private bool isPaused = false;
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

    private CoinManager coinManager;
    private Tilemap coinTilemap;
    public bool i = false;
    public bool TutoJumpAtiv = false;

    private void Awake()
    {
        // Evita duplicação do Player
        /*GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        if (players.Length > 1)
        {
            Destroy(gameObject);
            return;
        }*/

        // DontDestroyOnLoad(gameObject);
        // Instantiate(playerPrefab, new Vector3(-6.88f, -2f, 0f), Quaternion.identity);
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

        coinManager = GameObject.FindWithTag("Coin").GetComponent<CoinManager>();
        coinTilemap = GameObject.FindWithTag("Coin").GetComponent<Tilemap>();
        yesButton = GameObject.Find("Canvas").GetComponentInChildren<TryAgainScreen>();

        FindCoinReferences();
    }

    // Update is called once per frame
    void Update()
    {
        if (isFrozen) return;

        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        OnMove();
        Jumping();
        CheckInGrounded();
        CheckForCoin();
        PauseGame();

        if (Input.GetKeyDown(KeyCode.RightShift))
        {
            EnterDoor();
        }
    }

    void OnMove()
    {
        if (isFrozen) return;

        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * playerSpeed;

        float rotation = Input.GetAxis("Horizontal");

        if (rotation > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }

        if (rotation < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }

    void Jumping()
    {
        if (isFrozen) return;

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !isJumping && TutoJumpAtiv) //trigger.tutorialJumpAtivo
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }

    void CheckInGrounded()
    {
        isJumping = !groundChecked.IsGrounded();
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
       SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
       SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        bool checkpointAtivo = PlayerPrefs.GetInt("CheckpointAtivo", 0) == 1;
        Respawn(checkpointAtivo);
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

        if (currentDoor != null)
        {
            currentDoor.PlayAnimation();
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
        if (Input.GetKeyDown(KeyCode.KeypadEnter) && !isPaused)
        {
            isPaused = true;
            Time.timeScale = 0;
            pauseScreen.SetActive(true);
        }
        else if (isPaused && Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            isPaused = false;
            Time.timeScale = 1;
            pauseScreen.SetActive(false);
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
