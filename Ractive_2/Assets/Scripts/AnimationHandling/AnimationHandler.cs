using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Check that the last animation bool has been set to false before setting a new animation

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private string _animationName;

    public void TriggerAnimation(string animationName)
    {
        Debug.Log("Setting Trigger: " + animationName);
        //_animator.SetTrigger(animationName);
        _animator.SetBool(animationName, true);
        _animationName = animationName;
    }

    public void StopAnimation()
    {
        //_animator.SetTrigger("Cut");
        _animator.SetBool(_animationName, false);
    }
}
