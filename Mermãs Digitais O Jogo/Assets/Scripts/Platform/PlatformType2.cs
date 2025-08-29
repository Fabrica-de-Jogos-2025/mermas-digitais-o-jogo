using UnityEngine;

public class PlatformType2 : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float StoppingDistance1, StoppingDistance2;
    public Transform Target, Target2;
    public bool verif = false;
    public bool verif2 = false;
    public bool v = false;
    public bool y = false;

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (v && !y)
        {
            if ((Vector2.Distance(transform.position, Target.position) >= StoppingDistance1) && !verif && v)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            }
            else if (verif == false)
            {
                verif = true;
            }

            if ((Vector2.Distance(transform.position, Target2.position) >= StoppingDistance2) && verif2)
            {
                transform.position = Vector2.MoveTowards(transform.position, Target2.position, Speed * Time.deltaTime);
            }
            else if (verif2)
            {
                verif = verif2 = false;
                y = true;
            }
        }
    }

}

