using UnityEngine;

public class Boss3_Attack : MonoBehaviour
{
    [SerializeField] private PlayerStatus life;
    [SerializeField] private RobotMovement robot;
    private Animator anim;
    [SerializeField] private float Speed;
    [SerializeField] private float StoppingDistance;
    public bool h = true;
    public bool h_1 = true;
    public bool h_2 = false;
    public bool h_3 = false;
    public bool i = true;
    public bool m = true;
    public Transform reference_1, reference_2;
    public int j = 0;
    public int k;
    public int l = 1;
    public GameObject item_1, item_2, item_3;
    public bool z = false;
    public bool z_1 = false;
    public GameObject Hability6;
    public GameObject saida;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip hit;
    [SerializeField] private GameObject powerUpIcon;
    [SerializeField] private AudioClip damagePlayer;

    public bool iniciarBossFight = false;
    private bool q = true;

    void Start()
    {
        life = FindFirstObjectByType<PlayerStatus>();
        robot = FindFirstObjectByType<RobotMovement>();
        anim = GetComponent<Animator>();
    }
    
    void Update()
    {
        if (iniciarBossFight == true)
        {
            if (q)
            {
                anim.SetInteger("transition", 6);
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                    {
                        q = false;
                    }
            }
            else if (l == 5)
            {
                anim.SetInteger("transition", 4);
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.833f && anim.GetCurrentAnimatorStateInfo(0).IsName("defeated"))
                {
                    Destroy(this.gameObject);
                    saida.SetActive(true);
                }
            }
            else if (h)
            {
                if (h_1)
                {
                    anim.SetInteger("transition", 1);
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1333f && anim.GetCurrentAnimatorStateInfo(0).IsName("preparingToAttack"))
                    {
                        h_1 = false;
                        h_2 = true;
                    }
                }
                else if (h_2)
                {
                    anim.SetInteger("transition", 2);
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.0667f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Item"))
                    {
                        h_2 = false;
                        h_3 = true;
                    }
                }
                else if (h_3)
                {
                    Attack();
                }
                else
                {
                    anim.SetInteger("transition", 7);
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.0667f && anim.GetCurrentAnimatorStateInfo(0).IsName("Attack_Item"))
                    {
                        h = false;
                    }
                }
            }
            else if (z)
            {
                if ((l == 3) && (Hability6.activeSelf))
                {
                    Destroy(Hability6);
                }
                if (z_1)
                {
                    anim.SetInteger("transition", 6);
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                    {
                        z_1 = false;
                    }
                }
                else
                {
                    anim.SetInteger("transition", 3);
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.0667f && anim.GetCurrentAnimatorStateInfo(0).IsName("hitted"))
                    {
                        sfxAcess.Audio(hit);
                        if (l == 4)
                        {
                            l = 5;
                        }
                        anim.SetInteger("transition", 8);
                        z = false;
                        h = true;
                        h_1 = true;
                    }
                }

            }
            else
            {
                anim.SetInteger("transition", 0);
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.1333f && anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                {
                    z_1 = true;
                }
            }
        }
    }

    void Attack()
    {
        if(i)
        {
            i = false;
            k = j;
        }
        if(m)
        {
            if(Vector2.Distance(transform.position, reference_1.position) >= StoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, reference_1.position, Speed * Time.deltaTime);
            }
            else
            {
                m = false;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
        if(!m)
        {
            if(Vector2.Distance(transform.position, reference_2.position) >= StoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, reference_2.position, Speed * Time.deltaTime);
            }
            else if (k != 0)
            {
                m = true;
                k--;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                if(k == 0)
                {
                    if (l == 1)
                    {
                        item_1.SetActive(true);
                        l = 2;
                    }
                    else if (l == 2)
                    {
                        item_2.SetActive(true);
                        l = 3;
                    }
                    else if (l == 3)
                    {
                        item_3.SetActive(true);
                        l = 4;
                    }
                }
                m = true;
                h_3 = false;
                i = true;
                j++;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            powerUpIcon.SetActive(false);
            sfxAcess.Audio(damagePlayer);
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
