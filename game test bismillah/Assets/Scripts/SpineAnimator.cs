using Spine;
using Spine.Unity;
using UnityEngine;

public class SpineAnimator
{
    private SkeletonAnimation skeleton;

    public SpineAnimator(SkeletonAnimation skeleton)
    {
        this.skeleton = skeleton;
    }

    //public void Play(string animName, bool loop, int track = 0, float mix = 0.1f)
    //{
    //    if (skeleton == null) return;

    //    var entry = skeleton.AnimationState.SetAnimation(track, animName, loop);
    //    entry.MixDuration = mix;
    //}

    public TrackEntry Play(string animName, bool loop = true, int track = 0, float mix = 0.1f)
    {
        if (skeleton == null) return null;

        var entry = skeleton.AnimationState.SetAnimation(track, animName, loop);
        entry.MixDuration = mix;

        return entry;
    }

    public TrackEntry Queue(string animName, bool loop = true, int track = 0, float delay = 0f)
    {
        if (skeleton == null) return null;

        var entry = skeleton.AnimationState.AddAnimation(track, animName, loop, delay);

        return entry;
    }

    //TRACK CONTROL
    public void ClearTrack(int track)
    {
        skeleton?.AnimationState.ClearTrack(track);
    }

    public void ClearAllTracks()
    {
        skeleton?.AnimationState.ClearTracks();
    }

    public TrackEntry Current(int track = 0)
    {
        return skeleton?.AnimationState.GetCurrent(track);
    }

    //FLIP (bukan untuk flip gameobject, cuma flip animasi saja untuk kebutuhan animasi saja)
    public void FlipAnim(bool flip)
    {
        if (skeleton == null) return;
        skeleton.Skeleton.ScaleX = flip ? -1f : 1f;
    }

    //SPEED CONTROL
    public void SetTimeScale(float scale)
    {
        if (skeleton == null) return;
        skeleton.timeScale = scale;
    }

    //EVENTS / CALLBACK
    public void OnComplete(TrackEntry entry, System.Action callback)
    {
        if (entry == null || callback == null) return;
        entry.Complete += _ => callback();
    }
}
