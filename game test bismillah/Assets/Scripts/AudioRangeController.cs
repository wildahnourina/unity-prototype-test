using UnityEngine;

public class AudioRangeController : MonoBehaviour
{
    private AudioSource source;
    private Transform player;

    [SerializeField] private float minDistanceToHearSound = 12;
    [SerializeField] private bool showGizmo;
    private float maxVolume;

    private void Start()
    {
        player = Player.instance.transform;
        source = transform.parent.GetComponentInChildren<AudioSource>();

        maxVolume = source.volume;
    }

    private void Update()
    {
        if (player == null)
            return;

        //semakin dekat semakin jelas audionya
        float distance = Vector2.Distance(player.position, transform.position);
        float t = Mathf.Clamp01(1 - (distance / minDistanceToHearSound));

        float targetVolume = Mathf.Lerp(0, maxVolume, t * t);//pakai t * t instead of t aja, biar lebih smooth naik turun volume
        source.volume = Mathf.Lerp(source.volume, targetVolume, Time.deltaTime * 3);
        // kalo tbtb player dash menjauh dari audio source, kan penurunan volume secara tbtb, nah ini biar volume naik turun sesuai time.deltatime, lebih smooth
    }

    private void OnDrawGizmos()
    {
        if (showGizmo)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireSphere(transform.position, minDistanceToHearSound);
        }
    }
}
