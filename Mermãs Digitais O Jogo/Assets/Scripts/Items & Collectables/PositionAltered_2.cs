using UnityEngine;

public class PositionAltered_2 : MonoBehaviour
{   
    [SerializeField] private float StoppingDistance;
    public float Speed;
    public Transform Target;
    public Colision mov;
    public bool i = true;
    
    void Start()
    {
    }

    void Update()
    {
        if ((mov.Grids == true) && i)
        {
            if (Vector2.Distance(transform.position, Target.position) <= StoppingDistance)
            {
                i = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            }
        }
    }
}
