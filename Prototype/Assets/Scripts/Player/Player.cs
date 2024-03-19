using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed;
    public float jumpForce;
    public bool isJumping;
    public Vector2 direction;

    private Robo robot;
    private Rigidbody2D rig;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        robot = FindObjectOfType<Robo>();
    }

    // Update is called once per frame
    void Update()
    {
        direction = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        OnMove();
        Jumping();
    }

    void OnMove()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Horizontal"), 0f, 0f);
        transform.position += movement * Time.deltaTime * Speed;

        float rotation = Input.GetAxis("Horizontal");

        if (rotation > 0)
        {
            transform.eulerAngles = new Vector2(0f, 0f);
            robot.transform.eulerAngles = new Vector2(0f, 0f);
        }

        if (rotation < 0)
        {
            transform.eulerAngles = new Vector2(0f, 180f);
            robot.transform.eulerAngles = new Vector2(0f, 180f);
        }
    }

    void Jumping()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            rig.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
        }
    }
}
