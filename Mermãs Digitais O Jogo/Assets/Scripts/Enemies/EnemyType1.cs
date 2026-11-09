using TMPro;
using UnityEngine;

public class EnemyType1 : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerStatus life;
    [SerializeField] private PlayerDamageFeedback damageFeedback;
    [SerializeField] private RobotMovement robot;
    //[SerializeField] private RobotPowerUp powerUp;
    [SerializeField] private float speed;
    [SerializeField] private float distance;
    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip damagePlayer;
    [SerializeField] private GameObject powerUpIcon;
    [SerializeField] private GameObject aimIcon;
    [SerializeField] private GameObject explosion;
    [SerializeField] private TextMeshPro textExpression;
    [SerializeField] private float explosionDuration = 1f; // duração da animação de explosão
    [SerializeField] private float expressionDuration = 15f; // tempo que a expressão fica na tela
    private bool isDead = false;
    private Checkpoint checkpoint;

    private bool isRight;
    private bool playerIsDamaged = false;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float collisionRadius;
    private string[] expressions = new string[]{"Muito bem", "Ótimo", "Excelente", "WOW", "É isso aí", "Arrasou"};

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
        damageFeedback = life.GetComponent<PlayerDamageFeedback>();
    }
    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
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

        if (aimIcon != null)
        {
            bool canBeHit = robot != null
                            && robot.gameObject.activeInHierarchy
                            && robot.CanReachEnemy(transform.position);

            if (aimIcon.activeSelf != canBeHit)
                aimIcon.SetActive(canBeHit);
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;

        if (aimIcon != null) aimIcon.SetActive(false);

        // Desliga colisão e visual do inimigo, mas mantém o objeto vivo
        foreach (var col in GetComponentsInChildren<Collider2D>()) col.enabled = false;

        var rb = GetComponent<Rigidbody2D>();
        if (rb != null) rb.simulated = false;

        foreach (var sr in GetComponentsInChildren<SpriteRenderer>(true))
        {
            if (explosion != null && sr.transform.IsChildOf(explosion.transform)) continue;
            sr.enabled = false;
        }

        ShowExplosion();
        ShowExpression();

        // Só destrói depois que explosão e texto terminarem
        Destroy(gameObject, Mathf.Max(explosionDuration, expressionDuration));
    }

    private void ShowExplosion()
    {
        if (explosion == null) { return; }

        explosion.transform.rotation = Quaternion.identity; // anula o giro do inimigo
        explosion.SetActive(true);
    }

    private void ShowExpression()
    {
        if (textExpression == null) { return; }

        textExpression.text = expressions[Random.Range(0, expressions.Length)];
        textExpression.transform.rotation = Quaternion.identity;

        // Usa a mesma Sorting Layer do sprite do inimigo, com ordem acima dele
        var enemySprite = GetComponentInChildren<SpriteRenderer>();
        if (enemySprite != null)
        {
            textExpression.sortingLayerID = enemySprite.sortingLayerID;
            textExpression.sortingOrder = enemySprite.sortingOrder + 100;
        }

        // Garante que o texto fique na frente (a câmera normalmente está em Z negativo)
        Vector3 p = textExpression.transform.position;
        p.z = transform.position.z - 0.1f;
        textExpression.transform.position = p;

        textExpression.gameObject.SetActive(true);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, collisionRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision) => TryDamagePlayer(collision);
    private void OnCollisionStay2D(Collision2D collision) => TryDamagePlayer(collision);

    private void TryDamagePlayer(Collision2D collision)
    {
        if (isDead) return;
        if (!collision.collider.CompareTag("Player")) return;
        if (damageFeedback.IsInvincible) return;
        if (life.PlayerLife <= 0) return; // evita Hearts[-1] enquanto a morte é processada

        powerUpIcon.SetActive(false);
        sfxAcess.Audio(damagePlayer);
        robot.HasPowerUp = false;
        life.PlayerLife--;
        life.Hearts[life.PlayerLife].enabled = false;

        if (life.PlayerLife <= 0)
        {
            if (!checkpoint.IsActivated)
            {
                life.Die();
                robot.Die();
            }
            else
            {
                player.Respawn(checkpoint.IsActivated);
            }
        }
        else
        {
            damageFeedback.StartDamageFeedback(transform.position);
        }
    }
}
