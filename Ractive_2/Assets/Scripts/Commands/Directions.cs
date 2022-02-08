using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class Directions : MonoBehaviour, I_InstructionSet
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
        Debug.Log("Moving to next storybeat.");
        //Debug.Log("Saved: " + _choreographyHandler.currentStoryBeat.targetPosition);
        _choreographyHandler.NextStoryBeat();
        Debug.Log("Current storybeat: " + _choreographyHandler.currentStoryBeat.name);
        _choreographyHandler.PrintStoryBeatList();
    }

    public void MoveToPosition()
    {
        //Debug.Log("Moving to position.");
        UpdateGameData();
        //_navMeshAgent.SetDestination(_choreographyHandler.currentStoryBeat.targetPosition);
    }

    public void StartChoreography()
    {
        Debug.Log("Action!");

        UpdateGameData();

        StartCoroutine(_choreographyHandler.Execute(actor));
        _choreographyHandler.SetCurrentStoryBeatToStartOfChoreography();
    }

    public void StopChoreography()
    {
        Debug.Log("Cut!");

        _choreographyHandler.StopAudio();

        _navMeshAgent.SetDestination(_navMeshAgent.transform.position);
        _choreographyHandler.SetCurrentStoryBeatToStartOfChoreography();
    }

    public void MoveToPreviousPosition()
    {
        UpdateGameData();
       // _navMeshAgent.SetDestination(_choreographyHandler.currentStoryBeat.previousPosition);
        _navMeshAgent.SetDestination(_choreographyHandler.storyBeatList.First().previousPosition);
    }

    private void UpdateGameData()
    {
        _gameDataManger.Load();
        _choreographyHandler = _gameDataManger.choreographyHandler;
    }

    public void StartAudio()
    {
        Debug.Log("Play Audio");
        //_choreographyHandler.StartAudio();
        _choreographyHandler.currentStoryBeat.audioControlCommand = ChoreographyHandler.AudioControlCommand.Start;
        _gameDataManger.Save();
    }

    public void PauseAudio()
    {
        Debug.Log("Pause Audio");
        //_choreographyHandler.PauseAudio();
        _choreographyHandler.currentStoryBeat.audioControlCommand = ChoreographyHandler.AudioControlCommand.Pause;
        _gameDataManger.Save();
    }

    public void AddAnimation(string animationName)
    {
        Debug.Log("Set animation " + animationName);
        _choreographyHandler.currentStoryBeat.animationName = animationName;
        _gameDataManger.Save();
    }
}
