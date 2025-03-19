using UnityEngine;

public class Plataform2 : MonoBehaviour
{
    
    [SerializeField] private float Speed;
    [SerializeField] public float StoppingDistance1, StoppingDistance2;
    private Transform Target, Target2;
    public bool verif = false;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Limit 2").GetComponent<Transform>();
        Target2 = GameObject.FindGameObjectWithTag("Limit 3").GetComponent<Transform>();
    }

    void Update()
    {
        Movement();
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
