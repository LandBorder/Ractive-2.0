using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Directions : MonoBehaviour, InstructionSet
{
    private GameDataManger _gameDataManger;
    private ChoreographyHandler _choreographyHandler;
    private NavMeshAgent _navMeshAgent;

    public GameObject actor;
    public AudioSource audioSource;

    void Start()
    {
        _gameDataManger = new GameDataManger();
        UpdateGameData();

        _navMeshAgent = actor.GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;
    }

    public void MoveToNextStoryBeat()
    {
        Debug.Log("Moving to next Story Beat.");
        _choreographyHandler.NextStoryBeat();
        _choreographyHandler.PrintStoryBeatList();

    }

    public void MoveToPosition()
    {
        Debug.Log("Moving to position.");
        UpdateGameData();
        _navMeshAgent.SetDestination(_choreographyHandler.currentStoryBeat.targetPosition);
        //_choreographyHandler.Execute(actor);
  
    }

    public void StartChoreography()
    {
        Debug.Log("Action!");

        UpdateGameData();
        //_choreographyHandler.AddStoryBeat("SB_stCrispinsDay_2");

        // TODO: Execute each StoryBeat of the current Choreography after one another.
        // _choreographyHandler.ExecuteCurrentStoryBeat();
        // _choreographyHandler.Execute(); <-- Executes the currentStoryBeat, sets the next, executes it,...

        /*audioSource.clip = _choreographyHandler.currentStoryBeat.speechAudioClip;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        _navMeshAgent.SetDestination(_choreographyHandler.currentStoryBeat.targetPosition);//*/

        StartCoroutine(_choreographyHandler.Execute(actor));
        _choreographyHandler.SetCurrentStoryBeatToStartOfChoreography();
    }

    public void StopChoreography()
    {
        Debug.Log("Cut!");

        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        _navMeshAgent.SetDestination(_navMeshAgent.transform.position);
    }

    public void MoveToPreviousPosition()
    {
        UpdateGameData();
        _navMeshAgent.SetDestination(_choreographyHandler.currentStoryBeat.previousPosition);
    }

    private void UpdateGameData()
    {
        _gameDataManger.Load();
        _choreographyHandler = _gameDataManger.choreographyHandler;
    }
}
