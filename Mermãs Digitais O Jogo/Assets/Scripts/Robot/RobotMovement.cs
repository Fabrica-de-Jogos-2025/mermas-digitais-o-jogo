using UnityEngine;

public class RobotMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private float Speed;
    [SerializeField] private float StoppingDistance;

    private Transform Target;
    private static Vector3 respawnpoint;

    public static Vector3 Respawnpoint 
    { 
      get { return respawnpoint; } 
      set { respawnpoint = value; } 
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector2.Distance(transform.position, Target.position) >= StoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, Target.position, Speed * Time.deltaTime);
        }
    }
}
