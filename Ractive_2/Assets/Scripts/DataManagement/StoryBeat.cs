using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Defines the state of the actor.
// A multitude of Story Beats forms a choreography.

[System.Serializable]
public class StoryBeat
{
    public string name;

    public Vector3 targetPosition = Vector3.zero;
    public Vector3 previousPosition = Vector3.zero;

    public ChoreographyHandler.AudioControlCommand audioControlCommand;

    public string animationName = "";

    public string facialExpressionName = "";

}
