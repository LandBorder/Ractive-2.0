using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void TriggerAnimation(string animationName)
    {
        _animator.SetTrigger(animationName);
    }

    public void PlayAnimation(string animationName)
    {
        //_animator.Play("Hands Layer." + animationName);
        _animator.Play(animationName);
    }

    public void StopAnimation()
    {
        
    }
}
