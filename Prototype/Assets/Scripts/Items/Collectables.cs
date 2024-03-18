using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Tree;
using UnityEngine;

public class Collectables : MonoBehaviour
{ 
    private void OnTriggerEnter2D(Collider2D collision) {    
        if (collision.tag == "Player") {
                collision.gameObject.GetComponent<PlayerItems>().card++;
                Destroy(gameObject);
            }
    }
}
