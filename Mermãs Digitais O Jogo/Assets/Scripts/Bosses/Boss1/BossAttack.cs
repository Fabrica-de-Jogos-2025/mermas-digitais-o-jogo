using UnityEngine;
using System.Threading.Tasks;
using System.Collections;

public class BossAttack : MonoBehaviour
{
    private Animator anim;
    public Projectile A;
    public GameObject projectile;
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

    public GameObject Temporario1,Temporario2;

    private float speedFactor;

    private bool h_0 = true;
    private bool h_1 = false;
    private bool h_2 = false;
    private bool h_3 = true;
    private bool h_4 = false;    
    private bool h_5 = true;
    private bool h_6 = false;    
    private bool h_7 = true;
    private bool h_8 = false;

    [SerializeField] private GameplayAudio sfxAcess;
    [SerializeField] private AudioClip hit;
    [SerializeField] private AudioClip ataqueDoBoss;
    public bool iniciarBossFight = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (iniciarBossFight)
        {
            if ((V.valid && (i == 0)) || (V2.valid && (i == 1)) || (V3.valid && (i == 2)))
            {
                arrowdown.SetActive(false);
                k++;
                i++;
            }

            if (k == 0)
            {
                if (h_3)
                {
                    anim.SetInteger("transition", 4);
                    speedFactor = 0.06667f;
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.06667 * speedFactor && anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))

                    //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        sfxAcess.Audio(hit);
                        h_3 = false;
                        h_4 = true;
                    }
                }
                else

                if (h_4)
                {
                    anim.SetInteger("transition", 1);
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        h_3 = true;
                        h_4 = false;
                        permission = true;
                        k = 1;
                    }
                }
            }
            else if (k == 2)
            {
                if (h_5)
                {
                    anim.SetInteger("transition", 4);
                    speedFactor = 0.06667f;
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.06667 * speedFactor && anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))

                    //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        sfxAcess.Audio(hit);
                        h_5 = false;
                        h_6 = true;
                    }
                }
                else

                if (h_6)
                {
                    anim.SetInteger("transition", 1);
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        h_5 = true;
                        h_6 = false;
                        speedFactor = 0f;
                        permission = true;
                        k = 3;
                    }
                }
            }
            else if (k == 4)
            {
                if (h_7)
                {
                    anim.SetInteger("transition", 4);
                    speedFactor = 0.06667f;
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.06667 * speedFactor && anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
                    //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                    {
                        sfxAcess.Audio(hit);
                        h_7 = false;
                        h_8 = true;
                    }
                }
                else

                if (h_8)
                {
                    anim.SetInteger("transition", 1);
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                        //speedFactor = 0.06667f;
                        //if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.06667 * speedFactor && anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
                        //{
                        h_7 = true;
                    h_8 = false;
                    speedFactor = 0f;
                    permission = true;
                    k = 5;
                    //}
                }
            }
            else if (k == 5)
            {
                if (i == 3)
                {
                    anim.SetInteger("transition", 10);
                    speedFactor = 0.7f;

                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.7 * speedFactor && anim.GetCurrentAnimatorStateInfo(0).IsName("defeated"))
                    {
                        speedFactor = 0f;
                        gameObject.SetActive(false);
                        Temporario1.SetActive(true);
                        Temporario2.SetActive(true);
                    }
                }
            }

            else if ((tempoDecorrido <= tempoTotal) && permission)
            {
                if (permission)
                {
                    tempoDecorrido += Time.deltaTime;
                }

                if (h)
                {
                    if (h_0)
                    {
                        anim.SetInteger("transition", 0);
                        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("idle"))
                        {
                            h_0 = false;
                            h_1 = true;
                            //h_2 = false;
                        }
                    }
                    else

                    if (h_1)
                    {
                        anim.SetInteger("transition", 8);
                        if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("spitting"))
                        {
                            sfxAcess.Audio(ataqueDoBoss);
                            h_0 = true;
                            h_1 = false;
                            Attack();
                            //h_2 = true;
                        }
                    }/*else

                if(h_2){
                    anim.SetInteger("transition", 3);    
                    if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsName("spitting"))
                    {
                        h_0 = true;
                        h_1 = false;
                        h_2 = false;    
                        Attack();
                    }
                }*/

                }
                else
                {
                    anim.SetInteger("transition", 9);
                }
            }
            else
            {
                if (permission && !projectile.activeSelf)
                {
                    tempoDecorrido = 0;
                    permission = false;
                    arrowdown.SetActive(true);
                    if (c1)
                    {
                        colidder1.SetActive(true);
                        c1 = false;
                    }
                    else if (c2)
                    {
                        colidder2.SetActive(true);
                        c2 = false;
                    }
                    else if (c3)
                    {
                        colidder3.SetActive(true);
                        c3 = false;
                    }
                }

                anim.SetInteger("transition", 9);
            }
        }
    }


    void Attack()
    {

        if (A.authorization2 && !projectile.activeSelf)
        {
            A.a = true;
            projectile.SetActive(true);
            A.authorization = true;
        }

    }
}
