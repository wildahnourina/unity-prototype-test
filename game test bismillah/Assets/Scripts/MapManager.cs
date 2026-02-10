using UnityEngine;

public class MapManager : MonoBehaviour
{
    [SerializeField] private string musicName;

    private void Start()
    {
        AudioManager.instance.StartBGM(musicName);
    }
}
