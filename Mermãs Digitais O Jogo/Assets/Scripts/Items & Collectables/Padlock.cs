using UnityEngine;
using System;

public class Padlock : MonoBehaviour
{
    private Animator anim;
    public bool i = false;
    public Door d;
    public PlayerMovement pm;

    public Animator Anim { get => anim; set => anim = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Anim = GetComponent<Animator>();
        d = FindObjectOfType<Door>();
        pm = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        if (i)
        {
            d.i = true;
            pm.i = true;
            //Destroy(this.gameObject);
            gameObject.SetActive(false);
        }   
    }   
}
