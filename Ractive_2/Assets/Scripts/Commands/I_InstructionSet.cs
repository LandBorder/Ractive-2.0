using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface I_InstructionSet
{
    public void StartChoreography();
    public void StopChoreography();
    public void MoveToPosition();
    public void MoveToPreviousPosition();
    public void KeepPosition();
    public void MoveToNextStoryBeat();
    public void StartAudio();
    public void PauseAudio();
    public void AddAnimation(string animationName);
    public void AddFacialExpression(string animationName);

}
