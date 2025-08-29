using UnityEngine;

public class PlayerCutscene : MonoBehaviour
{
    public Transform target; // O computador ou posição de destino
    public float speed = 5f; // Velocidade do movimento
    [SerializeField] private Animator anim;
    private bool reachedTarget = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        if (reachedTarget) { 
            transform.eulerAngles = new Vector2(0f, 0f);
            return; 
        }

        // Move o player para a esquerda até a posição do computador
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Ativa a animação de andar
        anim.SetInteger("transition", 1);

        // Faz o player olhar para a esquerda
        transform.eulerAngles = new Vector2(0, 180);

        // Quando chegar ao destino, para de andar e troca para idle
        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            anim.SetInteger("transition", 0);
            reachedTarget = true;
        }
    }
}
