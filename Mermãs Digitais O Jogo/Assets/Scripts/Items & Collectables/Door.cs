using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform exitPoint;
    private Animator anim;
    private bool hasPlayed = false;
    [SerializeField] private Padlock currentPadlock;
    public bool i = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayAnimation()
    {
        if (!hasPlayed && anim != null && i)
        {
            anim.SetInteger("transition", 0);
            hasPlayed = true;

            // Chama o método com delay de 0.8 segundos
            Invoke(nameof(TriggerTransition), 0.8f);
        }
    }

    private void TriggerTransition()
    {
    if (anim != null && anim.GetInteger("transition") == 0)
    {
        hasPlayed = false;
        anim.SetInteger("transition", 1);
    }
    }

    public Vector2 GetExitPosition()
    {
        anim.SetInteger("transition", 1);
        return exitPoint != null ? exitPoint.position : transform.position;
    }
}
