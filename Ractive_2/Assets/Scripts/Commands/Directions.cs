using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Directions : MonoBehaviour, InstructionSet
{
    private DataManager _dataManager = new DataManager();
    public NavMeshAgent navMeshAgent;

    public void AddToChoreography()
    {
        Debug.Log("Adding direction to choreography.");
    }

    public void MoveToPosition()
    {
        Debug.Log("Moving to position.");
        _dataManager.Load();
        navMeshAgent.SetDestination(_dataManager.storyBeat.targetPosition);
    }

    public void StartChoreography()
    {
        Debug.Log("Action!");
    }

    public void StopChoreography()
    {
        Debug.Log("Cut!");
        navMeshAgent.SetDestination(navMeshAgent.transform.position);
    }

    public void MoveToPreviousPosition()
    {
        _dataManager.Load();
        navMeshAgent.SetDestination(_dataManager.storyBeat.previousPosition);
    }
}
