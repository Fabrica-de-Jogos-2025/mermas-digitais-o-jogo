using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float Speed;
    [SerializeField] private float StoppingDistance;

    [SerializeField] private PlayerStatus playerStatus;
    private Transform Target;
    private RobotPowerUp robotPowered;
    private static Vector3 respawnpoint;

    public static Vector3 Respawnpoint 
    { 
      get { return respawnpoint; } 
      set { respawnpoint = value; } 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);

        playerStatus = GetComponent<PlayerStatus>();
        robotPowered = FindAnyObjectByType<RobotPowerUp>();
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Target.position) >= StoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }

        float rotation = Input.GetAxis("Horizontal");

        if (rotation > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        } else if (rotation < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }

        PowerUp();
    }

    public void Die()
    {
        Destroy(this.gameObject);
    }

    void PowerUp()
    {
        if (robotPowered.HasPowerUp)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                StartCoroutine(UsePowerUp());
            }
        }
    }

    IEnumerator UsePowerUp()
    {
        float direction = player.transform.localScale.x;
        float maxDistance;
        
        if (direction > 0)
        {
            maxDistance = Camera.main.ViewportToWorldPoint(new Vector3(1, 0.5f, 0)).x;
        } else
        {
            maxDistance = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 0)).x;
        }

        while ((direction > 0 && transform.position.x < maxDistance) || (direction < 0 && transform.position.x > maxDistance))
        {
                transform.position += Vector3.right * direction * Speed * Time.deltaTime; // Move o robô para a direita
                yield return null;
            
        }

        Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, 5f);
        foreach (Collider2D enemy in enemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}
