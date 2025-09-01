using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class BrightnessLight2D : MonoBehaviour
{
    [SerializeField] private Volume volume; // arraste o Global Volume aqui
    private ColorAdjustments colorAdjustments;
    [SerializeField] private OptionScreen brightness;

    private void Start()
    {
        if (volume.profile.TryGet(out colorAdjustments))
        {
            ApplyBrightness();
        }
    }

    public void ApplyBrightness()
    {
        if (colorAdjustments != null && brightness != null)
        {
            // Mapear 0–100 para -2 a +2 de exposição (ajuste se quiser mais forte)
            float exposure = Mathf.Lerp(-2f, 2f, brightness.Brightness / 100f);
            colorAdjustments.postExposure.value = exposure;
        }
    }
}
