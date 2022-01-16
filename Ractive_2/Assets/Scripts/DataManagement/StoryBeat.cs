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

    // TODO: add

    // animation

    // speech audio

    // speech LipSync

    // facial expression

    // TODO: Come up with structure that allows saving a multitude of those entries. 
    //       This story beat is one state, another state is the following state.
    //       A multitude of story beats forms a choreography.
}
