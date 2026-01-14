using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightController : MonoBehaviour
{
    public event Action<float> OnBatteryChanged;
    public event Action<bool> OnHasFlashlight;

    [SerializeField] private Light2D spotlight;
    [SerializeField] private float maxBattery = 100f;
    [SerializeField] private float drainPerSecond = 5f;
    [SerializeField] private string itemId = "flashlight";

    private float currentBattery;
    private bool isOn;
    private Coroutine drainCo;
    private Inventory_Player inventory;

    private void Awake()
    {
        currentBattery = maxBattery;
        spotlight.enabled = false;

        OnBatteryChanged?.Invoke(BatteryPercent);
    }

    private void Start()
    {
        inventory = GetComponentInParent<Inventory_Player>();
        inventory.OnInventoryChange += UpdateActiveStatus;

        UpdateActiveStatus();
    }

    private void OnEnable() => OnHasFlashlight?.Invoke(true);
    private void OnDisable() => OnHasFlashlight?.Invoke(false);

    public float BatteryPercent => currentBattery / maxBattery;

    private void UpdateActiveStatus()
    {
        bool hasFlashlight = inventory.GetItemById(itemId) != null;

        if (!hasFlashlight)
            TurnOff();

        gameObject.SetActive(hasFlashlight);
    }

    public bool TryIncreaseBattery(float amount)
    {
        if (!gameObject.activeSelf)
            return false;

        if (currentBattery >= maxBattery)
            return false;

        currentBattery += amount;
        currentBattery = Mathf.Min(currentBattery, maxBattery);

        OnBatteryChanged?.Invoke(currentBattery / maxBattery);
        return true;
    }

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
