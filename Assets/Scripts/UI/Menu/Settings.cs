using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public GameObject root;
    public Slider soundVolumeSlider;
    public Slider musicVolumeSlider;
    public GameSettings gameSettings;

    void Awake()
    {
        InitializeUI();
        gameSettings.LoadSettings();
        CloseSettings();
    }

    private void InitializeUI()
    {
        soundVolumeSlider.value = gameSettings.soundVolume;
        musicVolumeSlider.value = gameSettings.musicVolume;

        soundVolumeSlider.onValueChanged.AddListener(HandleSoundVolumeChange);
        musicVolumeSlider.onValueChanged.AddListener(HandleMusicVolumeChange);
    }

    public void OpenSettings()
    {
        root.SetActive(true);
    }

    public void CloseSettings()
    {
        root.SetActive(false);
        gameSettings.SaveSettings();
    }

    private void HandleSoundVolumeChange(float value)
    {
        gameSettings.soundVolume = value;
        SoundEffectsManager.Instance?.UpdateSoundEffectsVolume(value);
    }

    private void HandleMusicVolumeChange(float value)
    {
        gameSettings.musicVolume = value;
        BackgroundMusicManager.Instance?.UpdateMusicVolume(value);
    }

    public void HandleBackButton()
    {
        SoundEffectsManager.Instance?.PlayButtonPressSound();
        CloseSettings();
    }
}