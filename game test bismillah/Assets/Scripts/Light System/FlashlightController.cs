using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class FlashlightController : MonoBehaviour
{
    public event Action<float> OnBatteryChanged;
    public event Action<bool> OnHasFlashlight;

    [Header("Flashlight Setting")]
    [SerializeField] private string itemId = "flashlight";
    [SerializeField] private Light2D spotlight;
    [SerializeField] private float maxBattery = 100f;
    [SerializeField] private float drainPerSecond = 5f;
    [SerializeField] private float lowBatteryPower = 20f;

    [Header("Trigger Emitter")]
    [SerializeField] private LayerMask ghostMask;
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius;
    private AnomalyTriggerEmitter emitter;

    private float currentBattery;
    private bool isOn;
    private float normalIntensity;
    private bool isLowBattery;

    private Coroutine drainCo;
    private Coroutine flickerCo;
    private Inventory_Player inventory;

    private void Awake()
    {
        TryGetComponent(out emitter);

        normalIntensity = spotlight.intensity;
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isOn) return;

        CheckGhostInLight();
    }

    private void OnEnable() => OnHasFlashlight?.Invoke(true);
    private void OnDisable() => OnHasFlashlight?.Invoke(false);

    public float BatteryPercent => currentBattery / maxBattery; 
    //di sini BatteryPercent menggunakan nilai 0-1, gak di kali 100f karena buat ngasih nilai ke slider battery di UI
    
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

    private void CheckGhostInLight()
    {
        if (!isOn) return;
        var targets = Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, ghostMask);

        foreach (var target in targets)
        {
            if (target.GetComponent<Ghost>() == null) return;
            emitter?.TriggerEmit();
        }
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

        drainCo = StartCoroutine(DrainBatteryCo());

        if (currentBattery <= lowBatteryPower)
            StartFlicker();
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

        StopFlicker();
    }

    private IEnumerator DrainBatteryCo()
    {
        while (currentBattery > 0f)
        {
            currentBattery -= drainPerSecond * Time.deltaTime;
            currentBattery = Mathf.Max(currentBattery, 0f);

            OnBatteryChanged?.Invoke(BatteryPercent);

            if (isOn) CheckGhostInLight();

            if (currentBattery <= lowBatteryPower && !isLowBattery) //pake isLowBattery disini biar StartFlicker() dipanggil sekali
            {
                isLowBattery = true;
                StartFlicker();          // infinite flicker (low battery)
            }
            else if (currentBattery > lowBatteryPower && isLowBattery)
            {
                isLowBattery = false;
                StopFlicker();
            }

            if (currentBattery <= 0f)
            {
                StopFlicker();
                TurnOff();
                yield break;
            }

            yield return null;
        }
    }

    public void TriggerFlicker(float duration) => StartFlicker(duration);

    private void StartFlicker(float duration = -1f)
    {
        if (!isOn) return;

        if (flickerCo != null)
            StopCoroutine(flickerCo);

        flickerCo = StartCoroutine(FlickerCo(duration));

        AudioManager.instance.PlayGlobalSFX("light_flicker");
    }

    private void StopFlicker()
    {
        if (flickerCo != null)
            StopCoroutine(flickerCo);

        flickerCo = null;
        spotlight.intensity = normalIntensity;
    }

    private IEnumerator FlickerCo(float duration)
    {
        float timer = 0f;

        while (duration < 0f || timer < duration)
        {
            spotlight.enabled = Random.value > 0.5f;
            spotlight.intensity = normalIntensity * Random.Range(0.25f, 0.6f);

            yield return new WaitForSeconds(Random.Range(0.05f, 0.15f));

            spotlight.enabled = true;
            spotlight.intensity = normalIntensity;

            float wait = Random.Range(0.5f, 2.5f);
            timer += wait;
            yield return new WaitForSeconds(wait);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
