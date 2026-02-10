using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Audio/Audio Database")]
public class SO_AudioDatabase : ScriptableObject
{
    public List<AudioClipData> player;//steps, caught
    public List<AudioClipData> ghost;//crying, humming, chase
    public List<AudioClipData> environment;//light flicker, candle, wind
    public List<AudioClipData> stinger;//suara2 tegang
    public List<AudioClipData> objectInteract;//door, notes, pickup
    public List<AudioClipData> uiAudio;//button click, dialogue text

    [Header("Music Lists")]
    public List<AudioClipData> mainMenuMusic;
    public List<AudioClipData> mapMusic;//real life, another dimensions

    private Dictionary<string, AudioClipData> clipCollection; //TKey audioName, TValue audioclip

    private void OnEnable()
    {
        clipCollection = new Dictionary<string, AudioClipData>();

        AddToCollection(player);
        AddToCollection(ghost);
        AddToCollection(environment);
        AddToCollection(ghost);
        AddToCollection(stinger);
        AddToCollection(objectInteract);
        AddToCollection(uiAudio);

        AddToCollection(mainMenuMusic);
        AddToCollection(mapMusic);
    }

    public AudioClipData Get(string groupName)
    {
        return clipCollection.TryGetValue(groupName, out var data) ? data : null;
        //cari di ClipCollection dengan nama groupName, kalo true return data, kalo false return null
    }

    //public AudioClip GetEnvironmentClip(string groupName)
    //{
    //    foreach (var data in environment)
    //    {
    //        if (data.audioName == groupName)
    //            return data.GetRandomClip();
    //    }
    //    return null;
    //}

    private void AddToCollection(List<AudioClipData> listToAdd)
    {
        foreach (var data in listToAdd)
        {
            if (data != null && clipCollection.ContainsKey(data.audioName) == false)
            {
                clipCollection.Add(data.audioName, data);
            }
        }
    }
}

[System.Serializable]
public class AudioClipData
{
    public string audioName;
    public List<AudioClip> clips = new List<AudioClip>();
    [Range(0f, 1f)] public float maxVolume = 1f;

    public AudioClip GetRandomClip()
    {
        if (clips == null || clips.Count == 0)
            return null;

        return clips[Random.Range(0, clips.Count)];
    }
}
