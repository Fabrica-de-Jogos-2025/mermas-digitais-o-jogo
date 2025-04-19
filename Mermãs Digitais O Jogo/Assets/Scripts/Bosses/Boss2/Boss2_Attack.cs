using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

public class Boss2_Attack : MonoBehaviour
{
    private Animator anim;
    public bool h = true;
    public float tempoDecorrido = 0f;
    public float tempoTotal = 30f;
    public bool permission = true;
    public GameObject arrowdown;
    public GameObject colidder1, colidder2, colidder3;
    private bool c1 = true;
    private bool c2 = true;
    private bool c3 = true;
    public Pause V, V2, V3;
    public int i = 0;
    public int k = -1;
    public int z = 0;
    public GameObject Temporario1,Temporario2;
    [SerializeField] private float Speed;
    [SerializeField] private float StoppingDistance;
    public Transform temp, Return;
    private Transform player;
    private bool preparing_attack = false;
    private bool w = false;
    [SerializeField] private PlayerStatus life;
    [SerializeField] private RobotMovement robot;
    private BoxCollider2D boxCollider;

    void Start()
    {
        life = FindFirstObjectByType<PlayerStatus>();
        robot = FindFirstObjectByType<RobotMovement>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    { 
        Transform player = GameObject.Find("Player").transform;  
        if (transform.position.x < player.position.x)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    

        if ((V.valid && (i == 0)) || (V2.valid && (i == 1)) || (V3.valid && (i == 2)))
        {
            arrowdown.SetActive(false);
            k++;
            i++;
        }

        if (k == 0)
        {
            anim.SetInteger("transition", 3);
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.005 && anim.GetCurrentAnimatorStateInfo(0).IsName("hitted"))
            {
                permission = true;
                k = 1;
            }
        } 
        else if (k == 2)
        {
            anim.SetInteger("transition", 3);
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.005 && anim.GetCurrentAnimatorStateInfo(0).IsName("hitted"))
            {
                permission = true;
                k = 3;
            }
        }
        else if (k == 4)
        {
            anim.SetInteger("transition", 3);
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.005 && anim.GetCurrentAnimatorStateInfo(0).IsName("hitted"))
            {
                permission = true;
                k = 5;
            }
        }
        else if (k == 5)
        {
            if (i == 3)
            {
                anim.SetInteger("transition", 4);

                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.8333 && anim.GetCurrentAnimatorStateInfo(0).IsName("defeated"))
                {
                    gameObject.SetActive(false);
                    Temporario1.SetActive(true);
                    Temporario2.SetActive(true);
                }
            }
        }

        else if ((z < 2) && permission)
        {
            if(h)
            {
                if(Vector2.Distance(transform.position, player.position) >= StoppingDistance)
                {
                    anim.SetInteger("transition", 0);
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.0005f && anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        Vector3 atualCenter = boxCollider.offset;
                        boxCollider.offset = new Vector3(1.002476f, atualCenter.y, atualCenter.z);
                        Attack();
                    }
                }
                else
                {
                    Vector3 atualCenter = boxCollider.offset;
                    boxCollider.offset = new Vector3(1.002476f, atualCenter.y, atualCenter.z);
                    h = false;
                }
            }
            else
            {
                if(!preparing_attack && !w)
                {
                    anim.SetInteger("transition", 1);
                    w = true;
                }
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.005f && anim.GetCurrentAnimatorStateInfo(0).IsName("preparingToAttack") && !preparing_attack)
                {
                    preparing_attack = true;
                }

                if(preparing_attack && w)
                {
                    anim.SetInteger("transition", 2);
                    w = false;
                }
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.005f && anim.GetCurrentAnimatorStateInfo(0).IsName("attacking") && preparing_attack)
                {
                    Vector3 atualCenter = boxCollider.offset;
                    boxCollider.offset = new Vector3(-1.002476f, atualCenter.y, atualCenter.z);
                    z++;
                    preparing_attack = false;
                    h = true;
                    anim.SetInteger("transition", 0);
                }  
            }
        }
        else if(Vector2.Distance(transform.position, Return.position) >= 0.5f)
        {
            Vector3 atualCenter = boxCollider.offset;
            boxCollider.offset = new Vector3(1.002476f, atualCenter.y, atualCenter.z);
            transform.position = Vector2.MoveTowards(transform.position, Return.position, Speed * Time.deltaTime);
        }
        else
        {
            if(permission)
            {
                z = 0;
                permission = false;
                arrowdown.SetActive(true);
                if(c1)
                {
                    colidder1.SetActive(true);
                    c1 = false;
                }
                else if(c2)
                {
                    colidder2.SetActive(true);
                    c2 = false;
                }
                else if(c3)
                {
                    colidder3.SetActive(true);
                    c3 = false;
                }
            }
        }
    }


    void Attack()
    {
        Transform player = GameObject.Find("Player").transform;
        temp.position = player.position;
        
        if(Vector2.Distance(transform.position, temp.position) >= StoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, temp.position, Speed * Time.deltaTime);
        }
        else
        {
            h = false;            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
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
