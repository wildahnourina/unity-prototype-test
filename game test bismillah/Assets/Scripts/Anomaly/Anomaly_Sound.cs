using UnityEngine;

public class Anomaly_Sound : Anomaly
{
    [Header("Sound Details")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private string audioName;
    [SerializeField] private bool loop;

    protected override void OnTriggered(AnomalyTriggerContext ctx)
    {
        var data = AudioManager.instance.GetEnvironment(audioName);
        var clip = data.GetRandomClip();

        if (loop)
        {
            audioSource.clip = clip;
            audioSource.loop = true;

            if (!audioSource.isPlaying)
                audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(clip);
        }
    }
}
