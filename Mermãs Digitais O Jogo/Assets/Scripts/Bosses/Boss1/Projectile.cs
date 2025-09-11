using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour
{
    //public Transform  player;
    private Transform player;
    public Transform temp0, temp, temp2;
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

    private Animator anim;
    private float speedFactor;
    public GameObject DeathProjectile;

    void Start()
    {
        life = FindFirstObjectByType<PlayerStatus>();
        robot = FindFirstObjectByType<RobotMovement>();
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if(a)
        {    
            if(authorization)
            {
                authorization2 = false;
                Transform player = GameObject.Find("Player(Clone)").transform;
                temp.position = player.position;
                temp0.position = player.position;
                authorization = false;
                H.h = false;
            }        
        }

        if(gameObject.activeSelf)
        {
            if((Vector2.Distance(transform.position, temp.position) >= StoppingDistance) && !p)
            {
                transform.position = Vector2.MoveTowards(transform.position, temp.position, Speed * Time.deltaTime);
            }
            else
            {
                DeathProjectile.SetActive(true);
                transform.position = temp2.position;
                authorization2 = true;
                a = false;
                i++;
                H.h = true;
                p = false;
                gameObject.SetActive(false);
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            p = true;
            temp0.position = transform.position;

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
