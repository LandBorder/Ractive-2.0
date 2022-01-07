using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Directions : MonoBehaviour, InstructionSet
{
    private GameDataManger _gameDataManger;
    private ChoreographyHandler _choreographyHandler;

    public NavMeshAgent navMeshAgent;
    public AudioSource audioSource;

    void Start()
    {
        _gameDataManger = new GameDataManger();
        UpdateGameData();
    }

    public void UpdateGameData()
    {
        _gameDataManger.Load();
        _choreographyHandler = _gameDataManger.choreographyHandler;
    }

    public void AddSpeechToChoreography()
    {
        Debug.Log("Adding lines to choreography.");
    }

    public void MoveToPosition()
    {
        Debug.Log("Moving to position.");
        //_choreographyHandler.Load("tears_in_rain");
        UpdateGameData();
        navMeshAgent.SetDestination(_choreographyHandler.currentStoryBeat.targetPosition);
  
    }

    public void StartChoreography()
    {
        Debug.Log("Action!");

        _choreographyHandler.Load("tears_in_rain");

        audioSource.clip = _choreographyHandler.currentStoryBeat.speechAudioClip;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        navMeshAgent.SetDestination(_choreographyHandler.currentStoryBeat.targetPosition);

        /*while(navMeshAgent.transform.position != navMeshAgent.destination)
        {
            yield return new WaitForSecondsRealtime(0.5f);
        }*/

    }

    public void StopChoreography()
    {
        Debug.Log("Cut!");

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        navMeshAgent.SetDestination(navMeshAgent.transform.position);
    }

    public void MoveToPreviousPosition()
    {
        //_choreographyHandler.Load("tears_in_rain");
        UpdateGameData();
        navMeshAgent.SetDestination(_choreographyHandler.currentStoryBeat.previousPosition);
    }
}
