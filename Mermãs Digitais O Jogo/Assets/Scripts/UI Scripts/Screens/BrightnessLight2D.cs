using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Volume))]
public class BrightnessLight2D : MonoBehaviour
{
    [SerializeField] private Volume volume; // arraste o Global Volume aqui
    private ColorAdjustments colorAdjustments;
    private const string PREF_BRIGHT = "brightness";
    // [SerializeField] private OptionScreen brightness;

    private void Awake()
    {
        // Garante que exista apenas um deste objeto
        var existing = FindObjectsByType<BrightnessLight2D>(FindObjectsSortMode.None);
        if (existing.Length > 1)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        if (volume == null)
            volume = GetComponent<Volume>();

        // IMPORTANTE: clonar o profile para evitar side-effects de assets compartilhados/reloads
        if (volume != null && volume.profile != null)
        {
            volume.profile = Instantiate(volume.profile);
        }

        // Tornar global e com alta prioridade para evitar ser sobrescrito por outros volumes
        volume.isGlobal = true;
        volume.priority = 1000;
        volume.weight = 1f;

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        TryGetAdjustments();
        ApplyBrightness();
        /*if (volume.profile.TryGet(out colorAdjustments))
        {
            ApplyBrightness();
        }*/
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        TryGetAdjustments();
        ApplyBrightness();

        var cam = Camera.main;
        if (cam != null)
        {
            var camData = cam.GetUniversalAdditionalCameraData();
            if (camData != null)
                camData.renderPostProcessing = true; // garante que o post-processing esteja ativo
        }

        // DisableOtherVolumesInScene();
    }

    private void TryGetAdjustments()
    {
        /*if (volume != null && volume.profile != null)
        {
            // Caso já exista, atualiza a referência; se não, tenta obter
            if (!volume.profile.TryGet(out colorAdjustments))
            {
                // Se não encontrou, tenta buscar na stack do Volume (fallback)
                colorAdjustments = null;
            }
        }*/

        if (volume != null && volume.profile != null)
            volume.profile.TryGet(out colorAdjustments);
    }

    public void ApplyBrightness()
    {
        /*if (colorAdjustments != null)
        {
            int savedBrightness = PlayerPrefs.GetInt("brightness", 50);
            // Mapear 0–100 para -2 a +2 de exposição (ajuste se quiser mais forte)
            float exposure = Mathf.Lerp(-2f, 2f, savedBrightness / 100f);
            colorAdjustments.postExposure.value = exposure;
        }*/

        /*if (volume == null)
        {
            Debug.LogWarning("[BrightnessLight2D] Volume não atribuído.");
            return;
        }

        if (volume.profile == null)
        {
            Debug.LogWarning("[BrightnessLight2D] Volume.profile é nulo.");
            return;
        }

        // garante a referência mais atual
        if (!volume.profile.TryGet(out colorAdjustments) || colorAdjustments == null)
        {
            TryGetAdjustments();
        }

        if (colorAdjustments != null)
        {
            int savedBrightness = PlayerPrefs.GetInt(PREF_BRIGHT, 50);
            float exposure = Mathf.Lerp(-2f, 2f, savedBrightness / 100f);
            colorAdjustments.postExposure.value = exposure;

            Debug.Log($"[BrightnessLight2D] Aplicado brilho {savedBrightness} -> exposure {exposure}");
        }
        else
        {
            Debug.LogWarning("[BrightnessLight2D] ColorAdjustments não encontrado no profile.");
        } */

        if (colorAdjustments == null)
            TryGetAdjustments();

        if (colorAdjustments != null)
        {
            int savedBrightness = PlayerPrefs.GetInt(PREF_BRIGHT, 50);
            float exposure = Mathf.Lerp(-2f, 2f, savedBrightness / 100f);
            colorAdjustments.postExposure.value = exposure;

            // Debug.Log($"[BrightnessLight2D] Brilho reaplicado: {savedBrightness} -> {exposure}");
        }
    }

    /*private void DisableOtherVolumesInScene()
    {
        Volume[] volumes = FindObjectsByType<Volume>(FindObjectsSortMode.None);
        foreach (var v in volumes)
        {
            if (v == volume) continue;
            // reduz o peso para zero para que não sobrescrevam
            v.weight = 0f;
            // alternativamente: if (v.isGlobal) v.enabled = false;
        }
    }*/
}
