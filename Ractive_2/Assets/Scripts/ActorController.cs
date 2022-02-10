using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.Windows.Speech;
using System.IO;
using System;
using System.Collections;

public class ActorController : MonoBehaviour
{
    public ChoreographyHandler choreographyHandler;
    public GameDataManger gameDataManger;
    public StagelightingController stagelightingController;

    public Directions directions;
    Invoker invoker;

    public NavMeshAgent navMeshAgent;
    public Camera xrCamera;
    public ThirdPersonCharacter thirdPersonCharacter;

    protected PhraseRecognizer phraseRecognizer;
    public ConfidenceLevel speechConfidenceLevel = ConfidenceLevel.Low;

    MovementStrategy movementStrategy;


    void Start()
    {
        movementStrategy = new AnimationDrivenMovementStrategy(navMeshAgent, xrCamera, thirdPersonCharacter);
        movementStrategy.Initialize();

        string path = Directory.GetCurrentDirectory();
        phraseRecognizer = new GrammarRecognizer(path + "/Assets/Scripts/Grammar/grammar_commands.xml");
        phraseRecognizer.OnPhraseRecognized += PhraseRecognizer_OnPhraseRecognized;
        phraseRecognizer.Start();

        gameDataManger.Load();
        SetChoreography("Default");
        gameDataManger.Display("Start");
    }

    void Update()
    {
        movementStrategy.Execute();
    }

    private void PhraseRecognizer_OnPhraseRecognized(PhraseRecognizedEventArgs args)
    {
        ActionCommand actionCommand = new ActionCommand(directions);
        CutCommand cutCommand = new CutCommand(directions);
        DeleteChoreographyCommand deleteChoreographyCommand = new DeleteChoreographyCommand(directions);
        MoveToPositionCommand moveToPositionCommand = new MoveToPositionCommand(directions);
        MoveToNextStoryBeatCommand moveToNextStoryBeatCommand = new MoveToNextStoryBeatCommand(directions);
        MoveToLastStoryBeatCommand moveToLastStoryBeatCommand = new MoveToLastStoryBeatCommand(directions);
        StartAudioCommand startAudioCommand = new StartAudioCommand(directions);
        PauseAudioCommand pauseAudioCommand = new PauseAudioCommand(directions);
        AddAnimationCommand addAnimationCommand = new AddAnimationCommand(directions);
        AddFacialExpressionCommand addFacialExpressionCommand = new AddFacialExpressionCommand(directions);
        KeepPositionCommand keepPositionCommand = new KeepPositionCommand(directions);

        string grammarTag = null;

        if (args.semanticMeanings != null)
        {
            foreach (SemanticMeaning sm in args.semanticMeanings)
            {
                foreach (string val in sm.values)
                {
                    grammarTag = val;
                }

                Debug.Log(args.text + " --- " + grammarTag);
            }

            switch (grammarTag)
            {
                case "startChoreography":
                    //stagelightingController.ActivateStageLighting();
                    StartCoroutine(ExecuteCommandAfterSeconds(actionCommand, 1.5f));
                    break;
                case "stopChoreography":
                    //stagelightingController.DeactivateStageLighting();
                    invoker = new Invoker(cutCommand);
                    invoker.ExecuteCommand();
                    break;
                case "deleteChoreography":
                    invoker = new Invoker(deleteChoreographyCommand);
                    invoker.ExecuteCommand();
                    break;
                case "moveToPosition":
                    invoker = new Invoker(moveToPositionCommand);
                    invoker.ExecuteCommand();
                    break;
                case "moveToPreviousPosition":
                    invoker = new Invoker(moveToPositionCommand);
                    invoker.UndoCommand();
                    break;
                case "keepPosition":
                    invoker = new Invoker(keepPositionCommand);
                    invoker.ExecuteCommand();
                    break;
                case "changeWalkingSpeedToSlow":
                    thirdPersonCharacter.m_onlyWalkingSpeed = true;
                    break;
                case "changeWalkingSpeedToFast":
                    thirdPersonCharacter.m_onlyWalkingSpeed = false;
                    break;
                case "tears_in_rain":
                    SetChoreography("tears_in_rain");
                    break;
                case "stCrispinsDay":
                    SetChoreography("stCrispinsDay");
                    break;
                case "nextStoryBeat":
                    invoker = new Invoker(moveToNextStoryBeatCommand);
                    invoker.ExecuteCommand();
                    break;
                case "lastStoryBeat":
                    invoker = new Invoker(moveToLastStoryBeatCommand);
                    invoker.ExecuteCommand();
                    break;
                case "playAudio":
                    invoker = new Invoker(startAudioCommand);
                    invoker.ExecuteCommand();
                    break;
                case "pauseAudio":
                    invoker = new Invoker(pauseAudioCommand);
                    invoker.ExecuteCommand();
                    break;
                case string a when a.Contains("Animation"):
                    invoker = new Invoker(addAnimationCommand);
                    // Each animation tag begins with "Animation_" followed by the name of the animation.
                    // The next line removes the beginning and passes only the name
                    invoker.ExecuteCommandWithParameter(grammarTag.Remove(0, 10));
                    break;
                case string a when a.Contains("FacialExpression"):
                    invoker = new Invoker(addFacialExpressionCommand);
                    // Each facial expression tag begins with "FacialExpression_" followed by the name of the expression.
                    // The next line removes the beginning and passes only the name
                    invoker.ExecuteCommandWithParameter(grammarTag.Remove(0, 17));
                    break;

            }

        }
    }

    private void SetChoreography(string choreographyName)
    {
        gameDataManger.SetChoreography(choreographyName);
        gameDataManger.Display("SetChoreography");  
    }

    private IEnumerator ExecuteCommandAfterSeconds(Command command, float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        invoker = new Invoker(command);
        invoker.ExecuteCommand();
    }
}
