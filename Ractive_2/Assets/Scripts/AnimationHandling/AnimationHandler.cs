using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void TriggerAnimation(string animationName)
    {
        Debug.Log("Setting Trigger: " + animationName);
        _animator.SetTrigger(animationName);
    }

    public void StopAnimation()
    {
        _animator.SetTrigger("Cut");
    }
}
