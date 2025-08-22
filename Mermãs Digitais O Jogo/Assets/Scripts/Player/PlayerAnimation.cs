using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement player;
    private RobotAnimation robotAnimation;
    private Animator anim;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = FindFirstObjectByType<PlayerMovement>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Walking();
        Jumping();
    }

    void Walking()
    {
        if (player.Direction.sqrMagnitude > 0 && !player.IsFrozen)
        {
            anim.SetInteger("transition", 1);
        }

        else
        {
            anim.SetInteger("transition", 0);
        }

        if (player.Direction.x > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (player.Direction.x < 0)
        {
            transform.eulerAngles = new Vector2(0, 180);
        }
    }

    void Jumping()
    {
        if (player.IsJumping)
        {
            anim.SetInteger("transition", 2);
        }
    }
}
