using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface InstructionSet
{
    public void StartChoreography();
    public void StopChoreography();
    public void MoveToPosition();
    public void MoveToPreviousPosition();
    public void AddSpeechToChoreography();

}
