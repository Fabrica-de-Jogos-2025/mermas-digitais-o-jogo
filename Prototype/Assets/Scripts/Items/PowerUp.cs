using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    private Robo robot;

    private void Start()
    {
        robot = FindObjectOfType<Robo>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerItems>().poweredUp = true;
            Destroy(gameObject);
            robot.anim.SetBool("powerup", true);
        }   
    }
}
