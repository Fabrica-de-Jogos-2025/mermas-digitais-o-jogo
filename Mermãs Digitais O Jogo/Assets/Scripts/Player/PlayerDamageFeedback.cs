using System.Collections;
using UnityEngine;

public class PlayerDamageFeedback : MonoBehaviour
{
    [Header("Referências")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private SpriteRenderer[] spriteRenderers; // se vazio, busca nos filhos

    [Header("Invencibilidade / Pisca")]
    [SerializeField] private float invincibilityDuration = 1f;
    [SerializeField] private float blinkInterval = 0.1f;
    [SerializeField, Range(0f, 1f)] private float blinkAlpha = 0.25f;

    [Header("Recuo")]
    [SerializeField] private Vector2 knockbackVelocity = new Vector2(5f, 6f); // x = para trás, y = pulinho
    [SerializeField] private float knockbackDuration = 0.3f;

    [Header("Colisão")]
    [SerializeField] private string enemyLayerName = "Enemy";

    public bool IsInvincible { get; private set; }
    public bool IsKnockedBack { get; private set; }

    private Coroutine invincibilityRoutine;
    private Coroutine knockbackRoutine;

    private int playerLayer;
    private int enemyLayer;

    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (spriteRenderers == null || spriteRenderers.Length == 0)
            spriteRenderers = GetComponentsInChildren<SpriteRenderer>();

        playerLayer = gameObject.layer;
        enemyLayer = LayerMask.NameToLayer(enemyLayerName);

        if (enemyLayer < 0)
            Debug.LogWarning($"Layer '{enemyLayerName}' não encontrada. A colisão não será desativada.");
    }

    /// <summary>Recuo + pisca + invencibilidade. Chamado quando a personagem sobrevive ao dano.</summary>
    public void StartDamageFeedback(Vector2 damageSourcePosition)
    {
        float dir = Mathf.Sign(transform.position.x - damageSourcePosition.x);
        if (dir == 0) dir = 1f;

        if (knockbackRoutine != null) StopCoroutine(knockbackRoutine);
        knockbackRoutine = StartCoroutine(KnockbackRoutine(dir));

        StartInvincibility();
    }

    /// <summary>Só pisca + invencibilidade (útil após respawn).</summary>
    public void StartInvincibility()
    {
        if (invincibilityRoutine != null) StopCoroutine(invincibilityRoutine);
        invincibilityRoutine = StartCoroutine(InvincibilityRoutine());
    }

    /// <summary>Limpa qualquer efeito em andamento (chamar no Respawn).</summary>
    public void ResetState()
    {
        if (knockbackRoutine != null) StopCoroutine(knockbackRoutine);
        if (invincibilityRoutine != null) StopCoroutine(invincibilityRoutine);

        IsKnockedBack = false;
        IsInvincible = false;
        SetAlpha(1f);
        SetEnemyCollisionIgnored(false);

        if (rb != null) rb.linearVelocity = Vector2.zero;
    }

    private IEnumerator KnockbackRoutine(float dir)
    {
        IsKnockedBack = true;
        rb.linearVelocity = new Vector2(dir * knockbackVelocity.x, knockbackVelocity.y);

        yield return new WaitForSeconds(knockbackDuration);

        // O movimento do jogador é feito via transform, então sobraria velocidade X no ar
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y);
        IsKnockedBack = false;
    }

    private IEnumerator InvincibilityRoutine()
    {
        IsInvincible = true;
        SetEnemyCollisionIgnored(true);

        float elapsed = 0f;
        bool faded = false;

        while (elapsed < invincibilityDuration)
        {
            faded = !faded;
            SetAlpha(faded ? blinkAlpha : 1f);

            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        SetAlpha(1f);
        SetEnemyCollisionIgnored(false);
        IsInvincible = false;
    }

    private void SetEnemyCollisionIgnored(bool ignore)
    {
        if (enemyLayer < 0) return;
        Physics2D.IgnoreLayerCollision(playerLayer, enemyLayer, ignore);
    }

    private void SetAlpha(float alpha)
    {
        foreach (var sr in spriteRenderers)
        {
            if (sr == null) continue; // evita erro se for destruído (ex.: Die())
            Color c = sr.color;
            c.a = alpha;
            sr.color = c;
        }
    }

    // Também é chamado quando o objeto é destruído (ex.: Die()).
    // Essencial aqui: IgnoreLayerCollision é global e persiste entre cenas.
    private void OnDisable()
    {
        SetAlpha(1f);
        SetEnemyCollisionIgnored(false);
        IsInvincible = false;
        IsKnockedBack = false;
    }
}
