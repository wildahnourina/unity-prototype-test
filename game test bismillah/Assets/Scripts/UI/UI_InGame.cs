using TMPro;
using UnityEngine.UI;
using UnityEngine;
using System;

public class UI_InGame : MonoBehaviour
{
    private Player player;

    [Header("Battery Percent")]
    [SerializeField] private Slider batterySlider;
    [SerializeField] private TextMeshProUGUI batteryText;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();        

        player.flashlight.OnBatteryChanged += UpdateBatteryBar;
        UpdateBatteryBar(player.flashlight.BatteryPercent);

        player.flashlight.OnHasFlashlight += FlashlightActive;
        FlashlightActive(player.flashlight.gameObject.activeSelf);
    }

    private void FlashlightActive(bool active) => batterySlider.gameObject.SetActive(active);

    private void UpdateBatteryBar(float percent)
    {
        batterySlider.value = percent;
        int batteryValue = Mathf.RoundToInt(percent * 100f);
        batteryText.text = batteryValue.ToString();
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
