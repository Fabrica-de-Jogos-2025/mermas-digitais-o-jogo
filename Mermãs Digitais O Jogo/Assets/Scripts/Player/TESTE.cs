using UnityEngine;

public class TESTE : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayer;

    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    public bool IsGrounded()
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