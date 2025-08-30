using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionScreen : MonoBehaviour
{
    [SerializeField] private Button[] highAndLowButtons;
    [SerializeField] private TextMeshProUGUI brightText;
    [SerializeField] private TextMeshProUGUI soundEffectText;
    [SerializeField] private TextMeshProUGUI musicText;

    [SerializeField] private int brightness = 50;
    [SerializeField] private int soundEffects = 50;
    [SerializeField] private int music = 50;

    [SerializeField] private BrightnessLight2D brightnessController;
    public int Brightness { get => brightness; set => brightness = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        highAndLowButtons[0].onClick.AddListener(() => LowButtons(brightText, ref brightness, true));
        highAndLowButtons[1].onClick.AddListener(() => HighButtons(brightText, ref brightness, true));
        highAndLowButtons[2].onClick.AddListener(() => LowButtons(soundEffectText, ref soundEffects, false));
        highAndLowButtons[3].onClick.AddListener(() => HighButtons(soundEffectText, ref soundEffects, false));
        highAndLowButtons[4].onClick.AddListener(() => LowButtons(musicText, ref music, false));
        highAndLowButtons[5].onClick.AddListener(() => HighButtons(musicText, ref music, false));

        brightText.text = brightness.ToString();
        soundEffectText.text = soundEffects.ToString();
        musicText.text = music.ToString();
    }

    public void LowButtons(TextMeshProUGUI text, ref int value, bool updateBrightness)
    {
        if (value > 0)
        {
            value -= 10;
            text.text = value.ToString();

            if (updateBrightness && brightnessController != null)
                brightnessController.ApplyBrightness();
        }
    }

    public void HighButtons(TextMeshProUGUI text, ref int value, bool updateBrightness)
    {
        if (value < 100)
        {
            value += 10;
            text.text = value.ToString();

            if (updateBrightness && brightnessController != null)
                brightnessController.ApplyBrightness();
        }
    }
}
