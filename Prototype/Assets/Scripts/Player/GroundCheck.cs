using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class GroundCheck : MonoBehaviour
{
    public LayerMask groundLayer;
    // Player player;
    // Start is called before the first frame update
    /*void Start()
    {
        player = gameObject.transform.parent.gameObject.GetComponent<Player>();
    }*/

    // Update is called once per frame
    /*void Update()
    {
        
    }*/

    /*private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            player.isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.layer != 3)
        {
            player.isJumping = true;
        }
    }*/

    public bool IsGrounded ()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.2f, groundLayer);

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                return true;
            }
        }
        return false;
    }
}
