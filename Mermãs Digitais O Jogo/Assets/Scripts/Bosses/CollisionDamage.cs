using UnityEngine;

public class CollisionDamage : MonoBehaviour
{
    [SerializeField] private PlayerDamageFeedback damageFeedback;
    [SerializeField] private PlayerStatus life;
    [SerializeField] private GameObject powerUpIcon;
    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip damagePlayer;
    [SerializeField] private RobotMovement robot;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        life = FindFirstObjectByType<PlayerStatus>();
        robot = FindFirstObjectByType<RobotMovement>();
        damageFeedback = life.GetComponent<PlayerDamageFeedback>();
    }

    private void OnCollisionEnter2D(Collision2D collision) => TryDamagePlayer(collision);
    private void OnCollisionStay2D(Collision2D collision) => TryDamagePlayer(collision);

    private void TryDamagePlayer(Collision2D collision)
    {
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
            life.Die();
            robot.Die();
        }
        else
        {
            damageFeedback.StartDamageFeedback(transform.position);
        }
    }
}
