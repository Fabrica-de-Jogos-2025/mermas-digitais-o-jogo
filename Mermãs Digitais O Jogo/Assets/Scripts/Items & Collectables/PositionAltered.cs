using UnityEngine;

public class PositionAltered : MonoBehaviour
{   
    [SerializeField] private float StoppingDistance;
    public float Speed;
    public Transform Target;
    public Hability2OpenAndClose_2 Dest;
    private int i = 0;
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("FinalPosition").GetComponent<Transform>();
    }

    void Update()
    {
        if (Dest.destroy)
        {
            if (i==135)
            {
                Dest.destroy = false;
            }
            else
            {
                i++;
                transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
            }
        }
    }
}
