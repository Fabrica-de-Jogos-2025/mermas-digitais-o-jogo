using UnityEngine;

public class Holes : MonoBehaviour
{
    [SerializeField] private RobotMovement robot;
    [SerializeField] private PlayerMovement player;
    [SerializeField] private PlayerStatus life;
    private Checkpoint checkpoint;

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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            if (robot != null) robot.HasPowerUp = false;

            if (life != null)
            {
                life.PlayerLife = 0;

                if (life.Hearts != null && life.Hearts.Length > 0 && life.PlayerLife < life.Hearts.Length)
                    life.Hearts[life.PlayerLife].enabled = false;

                if (life.PlayerLife <= 0)
                {
                    if (checkpoint == null || !checkpoint.IsActivated)
                    {
                        life.Die();
                        if (robot != null) robot.Die();
                    }
                    else
                    {
                        player.Respawn(checkpoint.IsActivated);
                    }
                }
            }
            else
            {
                Debug.LogError("?? PlayerStatus (life) não está atribuído no script Holes!");
            }
        }
    }
}
