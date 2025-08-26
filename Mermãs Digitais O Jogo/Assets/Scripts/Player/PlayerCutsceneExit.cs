using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCutsceneExit : MonoBehaviour
{
    public float moveSpeed = 3f;    // Velocidade para a direita
    public float jumpForce = 7f;    // Força do salto
    public float delayBeforeExit = 0.5f; // Tempo de espera após o spawn antes de andar
    public Transform exitPoint;     // Ponto para onde a personagem anda antes do salto (opcional)

    private Animator anim;
    private Rigidbody2D rb;
    [SerializeField] private Loader loader;
    private bool exiting = false;
    private bool jumped = false;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Chamado quando os inimigos terminarem de ser gerados
    public void StartExit()
    {
        if (!exiting)
        {
            exiting = true;
            Invoke(nameof(BeginMove), delayBeforeExit);
        }
    }

    private void BeginMove()
    {
        // Ativa animação de andar
        anim.SetInteger("transition", 1);
        // Faz o Player olhar para a direita
        transform.eulerAngles = Vector2.zero;
    }

    void Update()
    {
        if (!exiting) return;

        // Move para a direita até atingir o ponto de saída (ou infinito, se não definir exitPoint)
        transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

        // Quando atingir o ponto de saída (se houver) e ainda não tiver pulado, dá o salto
        if (exitPoint != null && !jumped)
        {
            if (transform.position.x >= exitPoint.position.x)
                JumpAndExit();
        }
    }

    private void JumpAndExit()
    {
        jumped = true;
        anim.SetInteger("transition", 2); // animação de pulo
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce); // aplica força para cima
        // Aqui você pode acionar algo para encerrar a cutscene (desativar câmera, carregar cena, etc.)
        // loader.StartCoroutine(loader.CarregarFase("Level Selector"));
        StartCoroutine(TransitionScene());
    }

    private IEnumerator TransitionScene()
    {
        yield return new WaitForSeconds(3f);
        loader.CarregarFase("Level Selector");
    }
}
