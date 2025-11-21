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
    [SerializeField] private int soundEffects = 100;
    [SerializeField] private int music = 50;

    [SerializeField] private AudioClip clip;
    [SerializeField] private GameplayAudio sfxClick;

    [SerializeField] private BrightnessLight2D brightnessController;
    [SerializeField] private MusicController musicVolumeController;
    public int Brightness { get => brightness; set => brightness = value; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        brightness = PlayerPrefs.GetInt("brightness", 50);
        soundEffects = PlayerPrefs.GetInt("soundEffects", 100);
        music = PlayerPrefs.GetInt("music", 50);

        brightText.text = brightness.ToString();
        soundEffectText.text = soundEffects.ToString();
        musicText.text = music.ToString();

        highAndLowButtons[0].onClick.AddListener(() => LowButtons(brightText, ref brightness, true));
        highAndLowButtons[1].onClick.AddListener(() => HighButtons(brightText, ref brightness, true));
        highAndLowButtons[2].onClick.AddListener(() => LowButtons(soundEffectText, ref soundEffects, false));
        highAndLowButtons[3].onClick.AddListener(() => HighButtons(soundEffectText, ref soundEffects, false));
        highAndLowButtons[4].onClick.AddListener(() => LowButtons(musicText, ref music, false));
        highAndLowButtons[5].onClick.AddListener(() => HighButtons(musicText, ref music, false));

        /*brightText.text = brightness.ToString();
        soundEffectText.text = soundEffects.ToString();
        musicText.text = music.ToString();*/
    }

    public void LowButtons(TextMeshProUGUI text, ref int value, bool updateBrightness)
    {
        sfxClick.Audio(clip);
        if (value > 0)
        {
            value -= 10;
            text.text = value.ToString();
            SavePreferences();

            if (updateBrightness && brightnessController != null)
                brightnessController.ApplyBrightness();

            //if (text == musicText && musicVolumeController != null)
            //  musicVolumeController.ApplyMusicVolume();
            if (text == musicText && AudioController.Instance != null)
                AudioController.Instance.SetMusicVolume(music);

            if (text == soundEffectText && AudioController.Instance != null)
                AudioController.Instance.SetSFXVolume(soundEffects);

        }
    }

    public void HighButtons(TextMeshProUGUI text, ref int value, bool updateBrightness)
    {
        sfxClick.Audio(clip);
        if (value < 100)
        {
            value += 10;
            text.text = value.ToString();
            SavePreferences();

            if (updateBrightness && brightnessController != null)
                brightnessController.ApplyBrightness();

            // if (text == musicText && musicVolumeController != null)
            // musicVolumeController.ApplyMusicVolume();

            if (text == musicText && AudioController.Instance != null)
                AudioController.Instance.SetMusicVolume(music);

            if (text == soundEffectText && AudioController.Instance != null)
                AudioController.Instance.SetSFXVolume(soundEffects);

        }
    }

    private void SavePreferences()
    {
        PlayerPrefs.SetInt("brightness", brightness);
        PlayerPrefs.SetInt("soundEffects", soundEffects);
        PlayerPrefs.SetInt("music", music);
        PlayerPrefs.Save();
    }
}
