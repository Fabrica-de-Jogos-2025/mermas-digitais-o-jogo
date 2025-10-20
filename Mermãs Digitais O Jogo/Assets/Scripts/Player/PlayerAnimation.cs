using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimation : MonoBehaviour
{
    private PlayerMovement player;
    private RobotAnimation robotAnimation;
    private Animator anim;
    public InputController controls;
    private InputAction move;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        controls = new InputController();
    }

    void OnEnable()
    {
        move = controls.Player.Move;
        move.Enable();
    }

    void OnDisable()
    {
        move.Disable();
    }


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
        float keyboardInput = Input.GetAxisRaw("Horizontal");
        float controllerInput = move.ReadValue<Vector2>().x;

        float horizontal = Mathf.Abs(controllerInput) > Mathf.Abs(keyboardInput)
        ? controllerInput
        : keyboardInput;

        if (Mathf.Abs(horizontal) > 0 && !player.IsFrozen)
        {
            anim.SetInteger("transition", 1);
        }

        else
        {
            anim.SetInteger("transition", 0);
        }

        if (horizontal > 0)
        {
            transform.eulerAngles = new Vector2(0, 0);
        }

        if (horizontal < 0)
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
