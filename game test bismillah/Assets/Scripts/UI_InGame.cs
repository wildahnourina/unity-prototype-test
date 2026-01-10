using TMPro;
using UnityEngine.UI;
using UnityEngine;

public class UI_InGame : MonoBehaviour
{
    private Player player;

    [Header("Battery Percent")]
    [SerializeField] private Slider batterySlider;
    [SerializeField] private TextMeshProUGUI batteryText;

    private void Start()
    {
        player = FindFirstObjectByType<Player>();        
        player.flashlight.OnBatteryChanged += UpdateUI;

        UpdateUI(player.flashlight.BatteryPercent);
    }

    private void UpdateUI(float percent)
    {
        batterySlider.value = percent;
        int batteryValue = Mathf.RoundToInt(percent * 100f);
        batteryText.text = batteryValue.ToString();
    }

    private void OnDestroy()
    {
        if (player.flashlight != null)
            player.flashlight.OnBatteryChanged -= UpdateUI;
    }
}
