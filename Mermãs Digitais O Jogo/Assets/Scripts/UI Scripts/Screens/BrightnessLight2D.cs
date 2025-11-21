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
    }

    private void TryGetAdjustments()
    {
        if (volume != null && volume.profile != null)
            volume.profile.TryGet(out colorAdjustments);
    }

    public void ApplyBrightness()
    {
        if (colorAdjustments == null)
            TryGetAdjustments();

        if (colorAdjustments != null)
        {
            int savedBrightness = PlayerPrefs.GetInt(PREF_BRIGHT, 50);
            float exposure = Mathf.Lerp(-2f, 2f, savedBrightness / 100f);
            colorAdjustments.postExposure.value = exposure;
        }
    }
}
