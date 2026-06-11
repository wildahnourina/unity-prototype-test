using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UI_InGame : MonoBehaviour
{
    private Player player;

    [Header("Battery Percent")]
    //[SerializeField] private Slider batterySlider;
    [SerializeField] private GameObject batteryUI;
    [SerializeField] private Image batteryImage;
    [SerializeField] private Sprite[] batterySprites;
    [SerializeField] private TextMeshProUGUI batteryText;

    [Header("Objective Text")]
    [SerializeField] private TextMeshProUGUI objectiveText;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();        

        player.flashlight.OnBatteryChanged += UpdateBatteryBar;
        UpdateBatteryBar(player.flashlight.BatteryPercent);

        player.flashlight.OnHasFlashlight += FlashlightActive;
        FlashlightActive(player.flashlight.gameObject.activeSelf);

        ObjectiveManager.instance.OnObjectiveChanged += UpdateObjectiveText;
        UpdateObjectiveText("");
    }

    private void UpdateObjectiveText(string text)
    {
        objectiveText.text = text;
        objectiveText.transform.parent.gameObject.SetActive(!string.IsNullOrEmpty(text));
    }

    private void FlashlightActive(bool active) => /*batterySlider*/batteryUI.gameObject.SetActive(active);

    private void UpdateBatteryBar(float percent)
    {
        //batterySlider.value = percent;
        //int batteryValue = Mathf.RoundToInt(percent * 100f);
        //batteryText.text = batteryValue.ToString();

        int batteryValue = Mathf.RoundToInt(percent * 100f);
        batteryText.text = batteryValue.ToString();

        int spriteIndex;

        if (percent <= 0f)
            spriteIndex = 0; // kosong
        else
            spriteIndex = Mathf.CeilToInt(percent * (batterySprites.Length - 1));

        batteryImage.sprite = batterySprites[spriteIndex];

    }

    private void OnDestroy()
    {
        if (player.flashlight != null)
        {
            player.flashlight.OnBatteryChanged -= UpdateBatteryBar;
            player.flashlight.OnHasFlashlight -= FlashlightActive;
        }
    }


}
