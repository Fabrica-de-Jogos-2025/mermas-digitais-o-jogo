using UnityEngine;

public class PositionAltered : MonoBehaviour
{   
    [SerializeField] private float StoppingDistance;
    public float Speed;
    public Transform Target;
    public Hability2OpenAndClose_2 Dest;
    
    void Start()
    {
    }

    void Update()
    {
        if (Dest.destroy)
        {
            if (Vector2.Distance(transform.position, Target.position) <= StoppingDistance)
            {
                Dest.destroy = false;
            }
            else
            {
                transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            }
        }
    }
}
