using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform exitPoint;
    private Animator anim;
    private bool hasPlayed = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!hasPlayed && anim != null)
        {
            anim.SetInteger("transition", 0); // Ativa a animação da porta
            hasPlayed = true; // Marca que a animação já foi executada
        }
    }

    public Vector2 GetExitPosition()
    {
        anim.SetInteger("transition", 1);
        return exitPoint != null ? exitPoint.position : transform.position;
    }

    public void ResetAnimation()
    {
        hasPlayed = false;
    }
}
