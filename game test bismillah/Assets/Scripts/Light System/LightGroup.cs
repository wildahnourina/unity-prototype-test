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

    private void Awake()
    {
        switchOn = startOn;
        SetLights();
    }

    public void Toggle()
    {
        switchOn = !switchOn;

        if (!switchOn)        
            StopFlicker();

        SetLights();
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

    void SetLights()
    {
        foreach (var light in lights)
            if (light != null)
                light.enabled = switchOn;
    }
}
