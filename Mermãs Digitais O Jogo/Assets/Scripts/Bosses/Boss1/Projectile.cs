using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    //public Transform  player;
    private Transform player;
    public Transform temp, temp2;
    public bool authorization = false;
    public bool authorization2 = true;
    public bool a = false;
    [SerializeField] private float Speed;
    [SerializeField] private float StoppingDistance;
    public float i = 0;
    public BossAttack H;

    [SerializeField] private PlayerStatus life;
    [SerializeField] private RobotMovement robot;

    public bool p = false;

    void Start()
    {
        life = FindFirstObjectByType<PlayerStatus>();
        robot = FindFirstObjectByType<RobotMovement>();   
    }


    void Update()
    {
        if(a)
        {    
            if(authorization)
            {
                authorization2 = false;
                //temp.position = player.position;
                Transform player = GameObject.Find("Player").transform;
                temp.position = player.position;
                authorization = false;
                H.h = false;
            }        
        }

        if(gameObject.activeSelf)
        {
            if(Vector2.Distance(transform.position, temp.position) >= StoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, temp.position, Speed * Time.deltaTime);
            }
            else
            {
                gameObject.SetActive(false);
                transform.position = temp2.position;
                authorization2 = true;
                a = false;
                i++;
                H.h = true;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            p = true;
            
            //robot.HasPowerUp = false;
            life.PlayerLife--;
            life.Hearts[life.PlayerLife].enabled = false;
            if (life.PlayerLife <= 0)
            {
                life.Die();
                robot.Die();
            }
        }
    }
}
