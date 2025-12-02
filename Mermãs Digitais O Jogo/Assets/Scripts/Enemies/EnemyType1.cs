using UnityEngine;

public class EnemyType1 : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerStatus life;
    [SerializeField] private RobotMovement robot;
    //[SerializeField] private RobotPowerUp powerUp;
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip damagePlayer;
    [SerializeField] private GameObject powerUpIcon;
    private Checkpoint checkpoint;

    private bool isRight;
    private bool playerIsDamaged = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float collisionRadius;

    public LayerMask EnemyLayer { get => enemyLayer; set => enemyLayer = value; }
    public Transform GroundCheck { get => groundCheck; set => groundCheck = value; }
    public float Speed { get => speed; set => speed = value; }

    private void Start()
    {
        //player = FindObjectOfType<PlayerMovement>();
        //life = FindObjectOfType<PlayerStatus>();
        //robot = FindObjectOfType<RobotMovement>();
        player = FindFirstObjectByType<PlayerMovement>();
        life = FindFirstObjectByType<PlayerStatus>();
        robot = FindFirstObjectByType<RobotMovement>();
        checkpoint = FindFirstObjectByType<Checkpoint>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);


        LayerMask groundLayer = LayerMask.GetMask("Ground"); // Apenas a camada do ch�o
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, distance, groundLayer);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Enemy"), LayerMask.NameToLayer("CameraConfiner"), true);

        if (hit.collider == false)
        {
            if (isRight == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isRight = true;
            }
        }

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, collisionRadius, enemyLayer);

        if (hitEnemies.Length > 1)
        {
            if (isRight == true)
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                isRight = false;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 180, 0);
                isRight = true;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !playerIsDamaged)
        {
            powerUpIcon.SetActive(false);
            sfxAcess.Audio(damagePlayer);
            robot.HasPowerUp = false;
            life.PlayerLife--;
            life.Hearts[life.PlayerLife].enabled = false;
            if (life.PlayerLife <= 0)
            {
                if (!checkpoint.IsActivated)
                {
                    // Nenhum checkpoint ativo → reinicia cena
                    life.Die();
                    robot.Die();
                }
                else
                {
                    // Respawn no último checkpoint
                    player.Respawn(checkpoint.IsActivated);
                }
            }
        }
    }
}
