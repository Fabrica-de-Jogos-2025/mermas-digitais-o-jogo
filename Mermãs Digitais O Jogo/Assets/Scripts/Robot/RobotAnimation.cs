using UnityEngine;

public class RobotAnimation : MonoBehaviour
{
    private PlayerMovement player;
    private RobotMovement robot;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
        robot = FindFirstObjectByType<RobotMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Walking();
    }

    void Walking()
    {
        if (player.Direction.sqrMagnitude > 0)
        {
            anim.SetInteger("transition", 1);
        }
        else
        {
            anim.SetInteger("transition", 0);
        }

        /*if (robot.Direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (player.Direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }*/
    }
}
