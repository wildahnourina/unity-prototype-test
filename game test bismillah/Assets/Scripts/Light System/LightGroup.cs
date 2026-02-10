using Spine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LightGroup : MonoBehaviour
{
    [SerializeField] private bool startOn = false;
    public List<Light2D> lights;

    private Coroutine flickerCo;
    private bool switchOn;
    public bool IsOn => switchOn;

    private AnomalyTriggerEmitter emitter;
    private Coroutine emitCo;
    private AudioSource audioSource;

    private void Awake()
    {
        TryGetComponent(out emitter);
        audioSource = GetComponentInChildren<AudioSource>();

        switchOn = startOn;
        SetLights();
    }

    //private void Update()
    //{
    //    if (switchOn) emitter?.TriggerEmit();
    //}

    public void Toggle()
    {
        switchOn = !switchOn;

        if (!switchOn)
            StopFlicker();

        SetLights();

        HandleEmit();
        HandleAudio();
    }

    // FLICKER (EVENT)
    public void StartFlicker(float duration)
    {
        if (!switchOn) return;

        StopFlicker();
        flickerCo = StartCoroutine(FlickerCo(duration));
    }

    void StopFlicker()
    {
        if (flickerCo != null)
            StopCoroutine(flickerCo);
            
        flickerCo = null;
    }

    IEnumerator FlickerCo(float duration)
    {
        float t = 0f;

        while (t < duration && switchOn)
        {
            bool on = Random.value > 0.5f;
            foreach (var light in lights)
                if (light != null)
                    light.enabled = on;

            float wait = Random.Range(0.05f, 0.25f);
            yield return new WaitForSeconds(wait);
            t += wait;
        }

        //kembali ke kondisi saklar
        SetLights();
        flickerCo = null;
    }

    private void HandleAudio()
    {
        var data = AudioManager.instance.GetEnvironment("bulb_on");
        audioSource.clip = data.GetRandomClip();
        audioSource.loop = true;
        if (switchOn)
            audioSource.Play();
        else
            audioSource.Stop();
    }

    private void HandleEmit()
    {
        if (switchOn)
        {
            if (emitCo == null)
                emitCo = StartCoroutine(EmitCo());
        }
        else
        {
            if (emitCo != null)
            {
                StopCoroutine(emitCo);
                emitCo = null;
            }
        }
    }

    private IEnumerator EmitCo()
    {
        while (switchOn)
        {
            emitter?.TriggerEmit();
            yield return null;
        }
    }

    private void SetLights()
    {
        foreach (var light in lights)
            if (light != null)
                light.enabled = switchOn;
    }

    //private void SetLights(bool value)
    //{
    //    if (switchOn == value) return;

    //    switchOn = value;

    //    if (!switchOn) StopFlicker();

    //    ApplyLightVisual();
    //}

    //private void ApplyLightVisual()
    //{
    //    foreach (var light in lights)
    //        if (light != null)
    //            light.enabled = switchOn;
    //}
}
