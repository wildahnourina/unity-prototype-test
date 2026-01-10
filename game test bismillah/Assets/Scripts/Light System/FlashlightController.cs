using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightController : MonoBehaviour
{
    public event Action<float> OnBatteryChanged;

    [SerializeField] private Light2D spotlight;
    [SerializeField] private float maxBattery = 100f;
    [SerializeField] private float drainPerSecond = 5f;

    private float currentBattery;
    private bool isOn;
    private Coroutine drainCo;

    void Awake()
    {
        currentBattery = maxBattery;
        spotlight.enabled = false;

        OnBatteryChanged?.Invoke(BatteryPercent);
    }

    public float BatteryPercent => currentBattery / maxBattery;

    public void Toggle()
    {
        if (isOn) TurnOff();
        else TurnOn();
    }

    private void TurnOn()
    {
        if (currentBattery <= 0f) return;

        isOn = true;
        spotlight.enabled = true;

        if (drainCo != null)
            StopCoroutine(drainCo);

        drainCo = StartCoroutine(DrainBattery());
    }

    private void TurnOff()
    {
        isOn = false;
        spotlight.enabled = false;

        if (drainCo != null)
        {
            StopCoroutine(drainCo);
            drainCo = null;
        }
    }

    private IEnumerator DrainBattery()
    {
        while (currentBattery > 0f)
        {
            currentBattery -= drainPerSecond * Time.deltaTime;
            OnBatteryChanged?.Invoke(BatteryPercent);

            if (currentBattery <= 0f)
            {
                currentBattery = 0f;
                OnBatteryChanged?.Invoke(0f);
                TurnOff();
                yield break;
            }

            yield return null;
        }
    }
}
