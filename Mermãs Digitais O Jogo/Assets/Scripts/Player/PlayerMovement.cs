using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float playerSpeed;
    public float jumpForce;
    public Vector2 direction;
    public bool isJumping;

    private Rigidbody2D rig;
    private GroundCheck groundChecked;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        groundChecked = GetComponentInChildren<GroundCheck>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        OnMove();
        Jumping();
        CheckInGrounded();
    }

    void OnMove()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * playerSpeed;

        float rotation = Input.GetAxis("Horizontal");

        if (rotation > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
        }

        if (rotation < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
        }
    }

    void Jumping()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }

    void CheckInGrounded()
    {
        isJumping = !groundChecked.IsGrounded();
    }

}
