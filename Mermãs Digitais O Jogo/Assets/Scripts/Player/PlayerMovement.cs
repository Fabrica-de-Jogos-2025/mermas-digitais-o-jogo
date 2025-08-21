using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private bool isJumping;
    private bool isFrozen = false;
    private bool isPaused = false;
    private Vector2 direction;
    [SerializeField] private Door currentDoor;
    [SerializeField] private GameObject pauseScreen;
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

    private Rigidbody2D rig;
    private GroundCheck groundChecked;
    private Vector2 lastCheckpointPosition;

    public CoinManager coinManager;
    public Tilemap coinTilemap;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        rig = GetComponent<Rigidbody2D>();
        //robot = FindAnyObjectByType<RobotMovement>();
        groundChecked = GetComponentInChildren<GroundCheck>();
        lastCheckpointPosition = transform.position;
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
        
        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && !isJumping)
        {
            rig.AddForce(new Vector2(0f, JumpForce), ForceMode2D.Impulse);
        }
    }

    void CheckInGrounded()
    {
        isJumping = !groundChecked.IsGrounded();
    }

    private void CheckForCoin()
    {
        if (coinTilemap == null) return;

        Vector3Int cellPosition = coinTilemap.WorldToCell(transform.position);

        if (coinTilemap.HasTile(cellPosition)) // Se houver moeda nessa posição
        {
            coinTilemap.SetTile(cellPosition, null); // Remove a moeda
            coinManager.AddCoin(); // Atualiza o contador
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
        transform.position = currentDoor.GetExitPosition(); 
    }

    public void SetLastCheckpoint(Vector2 newCheckpoint)
    {
        lastCheckpointPosition = newCheckpoint;
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
        } else if (isPaused && Input.GetKeyDown(KeyCode.KeypadEnter))
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
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Door"))
        {
            currentDoor = null;
        }
    }

    public void OnCollisionEnter2D(Collision2D collision){
	    if(collision.transform.tag == "Platform")
	    {
            transform.SetParent(collision.transform);
            DontDestroyOnLoad(this.gameObject);
	    }
    }

    public void OnCollisionExit2D(Collision2D collision){
	    if(collision.transform.tag == "Platform")
	    {
            transform.SetParent(null);
            DontDestroyOnLoad(this.gameObject);
	    }
    }

}
