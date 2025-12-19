using Spine.Unity;
using UnityEngine;

public class SpineAnimator
{
    private SkeletonAnimation skeleton;

    public SpineAnimator(SkeletonAnimation skeleton)
    {
        this.skeleton = skeleton;
    }

    public void Play(string animName, bool loop = true, int track = 0, float mix = 0.1f)
    {
        if (skeleton == null) return;

        var entry = skeleton.AnimationState.SetAnimation(track, animName, loop);
        entry.MixDuration = mix;
    }
}
