using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class AutoScaleCheckpoint : MonoBehaviour
{
    [SerializeField] private float targetSize = 1f; // tamanho desejado em unidades do mundo
    [SerializeField] private bool alignToGround = true;
    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr.sprite == null) return;

        // --- Escala ---
        Vector2 spriteSize = sr.bounds.size;
        float scaleFactor = targetSize / Mathf.Max(spriteSize.x, spriteSize.y);
        transform.localScale = new Vector3(scaleFactor, scaleFactor, 1f);

        // --- Recalcular posição no chão ---
        if (alignToGround)
        {
            // Recalcula o bounds após a escala
            Bounds b = sr.bounds;

            // Calcula o deslocamento necessário para alinhar a base do sprite ao chão (y = 0)
            float offsetY = b.extents.y; // metade da altura
            transform.position = new Vector3(transform.position.x, offsetY, transform.position.z);
        }
    }
}

