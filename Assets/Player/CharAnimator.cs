using System.Collections;
using System.Collections.Generic;
using DragonBones;
using UnityEngine;

public class CharAnimator : MonoBehaviour
{

    private UnityArmatureComponent _animator;
    
    void Start()
    {
        _animator = GetComponent<UnityArmatureComponent>();
        _animator.animation.Play("idle");
    }

    public void TriggerHappy()
    {
        var state = _animator.animation.Play("happy");
        var duration = state._duration;

        StartCoroutine(PlayAfterDuration(duration, "idle"));
    }
    
    public void TriggerSad()
    {
        var state = _animator.animation.Play("sad");
        var duration = state._duration;

        StartCoroutine(PlayAfterDuration(duration, "idle"));
    }

    public IEnumerator PlayAfterDuration(float duration, string animation)
    {
        yield return new WaitForSeconds(duration);

        _animator.animation.Play(animation);
    }
}
