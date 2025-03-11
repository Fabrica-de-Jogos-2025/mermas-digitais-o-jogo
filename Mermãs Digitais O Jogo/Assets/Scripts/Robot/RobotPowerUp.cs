using UnityEngine;

public class RobotPowerUp : MonoBehaviour
{
    [SerializeField] private bool hasPowerUp = false;
    private PlayerMovement player;
    private RobotMovement robot;

    public bool HasPowerUp { get => hasPowerUp; set => hasPowerUp = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GetComponent<PlayerMovement>();
        robot = FindObjectOfType<RobotMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
            hasPowerUp = true;
        }
    }
}
