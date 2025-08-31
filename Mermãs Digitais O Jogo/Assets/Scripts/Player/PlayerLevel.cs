using System;
using UnityEngine;

public class PlayerLevel : MonoBehaviour
{
    public float moveSpeed = 15f;    // Velocidade para a direita
    public float jumpForce = 7f;    // Força do salto
    public float stopX = 0f;
    public BoxCollider2D ground;

    private Animator anim;
    private Rigidbody2D rb;
    private bool jumped = false;
    private bool finished = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

        Jump();
    }

    // Update is called once per frame
    void Update()
    {
        if (jumped && !finished) {
            if (transform.position.x >= stopX)
            {
                finished = true;
                rb.linearVelocity = Vector2.zero;
                anim.SetInteger("transition", 2); // Idle/parado
            }
        }
    }

    void Jump()
    {
        jumped = true;
        anim.SetInteger("transition", 1); // Animação de pulo
        rb.AddForce(new Vector2(moveSpeed, jumpForce), ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Quando encostar no chão
        if (collision.collider.CompareTag("Ground"))
        {
            anim.SetInteger("transition", 2); // Idle/parado
        }
    }
}
