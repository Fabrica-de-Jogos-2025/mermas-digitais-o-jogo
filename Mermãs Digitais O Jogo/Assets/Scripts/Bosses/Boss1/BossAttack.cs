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

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if ((V.valid && (i == 0)) || (V2.valid && (i == 1)) || (V3.valid && (i == 2)))
        {
            arrowdown.SetActive(false);
            //anim.SetInteger("transition", 1);

            /*if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
            {
                permission = true;
            }
            //permission = true;*/

            k++;

            i++;

            /*if (i == 3)
            {
                anim.SetInteger("transition", 2);

                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("defeated"))
                {
                    gameObject.SetActive(false);
                }
            }*/
        }

        if (k == 0)
        {
            anim.SetInteger("transition", 1);
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
            {
                permission = true;
                k = 1;
            }
        } 
        else if (k == 2)
        {
            anim.SetInteger("transition", 1);
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
            {
                permission = true;
                k = 3;
            }
        }
        else if (k == 4)
        {
            anim.SetInteger("transition", 1);
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("hit"))
            {
                permission = true;
                k = 5;
            }
        }
        else if (k == 5)
        {
            if (i == 3)
            {
                anim.SetInteger("transition", 2);

                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("defeated"))
                {
                    gameObject.SetActive(false);
                    Temporario1.SetActive(true);
                    Temporario2.SetActive(true);
                }
            }
        }

        

        

        //if(A.i <= 7)
        
        else if ((tempoDecorrido <= tempoTotal) && permission)
        {
            if(permission)
            {
                tempoDecorrido += Time.deltaTime;
            }

            if(h)
            {
                anim.SetInteger("transition", 3);
                if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1 && anim.GetCurrentAnimatorStateInfo(0).IsName("spitting"))
                {
                    Attack();
                }
            }
            else
            {
                anim.SetInteger("transition", 0);
            }
        }
        else
        {
            if(permission && !projectile.activeSelf)
            {
                tempoDecorrido = 0;
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

            anim.SetInteger("transition", 0);
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
