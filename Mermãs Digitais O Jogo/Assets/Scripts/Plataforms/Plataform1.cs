using UnityEngine;

public class Plataform1 : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float StoppingDistance1, StoppingDistance2;
    private Transform Target, Target2;
    public bool verif = false;
    public bool PlayerDetect = false;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Limit 1").GetComponent<Transform>();
        Target2 = GameObject.FindGameObjectWithTag("Limit 2").GetComponent<Transform>();
    }

    void Update()
    {
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDetect = true;
        }
        else
        {
            if (PlayerDetect)
            {
            PlayerDetect = false;
            }
        }
    }

    void Movement()
    {
        if ((Vector2.Distance(transform.position, Target.position) >= StoppingDistance1) && verif)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
        else if (verif == true)
        {
            verif = false;
        }

        if ((Vector2.Distance(transform.position, Target2.position) >= StoppingDistance2) && !verif)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target2.position, Speed * Time.deltaTime);
        }
        else if (verif == false)
        {
            verif = true;
        }
    }

}

