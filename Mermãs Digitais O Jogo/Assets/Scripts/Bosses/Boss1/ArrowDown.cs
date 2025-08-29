using UnityEngine;

public class ArrowDown : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float StoppingDistance1, StoppingDistance2;
    public Transform Target, Target2;
    public bool verif = false;
    

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
