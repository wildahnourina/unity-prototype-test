using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class UI_Options : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private float mixerMultiplier = 25;

    [Header("BGM Volume Settings")]
    [SerializeField] private Slider bgmSlider; //jangan lupa Min value nya slider di ubah di inspektor, dari 0 jadi 0.001, karena di Audio Mixer 0 itu kebaca 0 DB (ada suara)
    [SerializeField] private string bgmParametr;

    [Header("SFX Volume Settings")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private string sfxParametr;

    public void BGMSliderValue(float value)
    {
        float newValue = MathF.Log10(value) * mixerMultiplier;
        audioMixer.SetFloat(bgmParametr, newValue);
    }

    public void SFXSliderValue(float value)
    {
        float newValue = MathF.Log10(value) * mixerMultiplier;
        audioMixer.SetFloat(sfxParametr, newValue);
    }

    //public void GoMainMenuBTN() => GameManager.instance.ChangeScene("MainMenu", RespawnType.NonSpecific);


    //ngesave nilai float slider volume pakai PlayerPrefs issoke, tidak untuk lain yang lebih kompleks !!!

    private void OnEnable()
    {
        sfxSlider.value = PlayerPrefs.GetFloat(sfxParametr, .6f); //default value 0.6f
        bgmSlider.value = PlayerPrefs.GetFloat(bgmParametr, .6f);

        sfxSlider.onValueChanged.Invoke(sfxSlider.value);
        bgmSlider.onValueChanged.Invoke(bgmSlider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(sfxParametr, sfxSlider.value);
        PlayerPrefs.SetFloat(bgmParametr, bgmSlider.value);
    }

    public void LoadUpVolume()
    {
        sfxSlider.value = PlayerPrefs.GetFloat(sfxParametr, .6f);
        bgmSlider.value = PlayerPrefs.GetFloat(bgmParametr, .6f);

        sfxSlider.onValueChanged.Invoke(sfxSlider.value);
        bgmSlider.onValueChanged.Invoke(bgmSlider.value);
    }
}
