using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.Windows.Speech;
using System.IO;
using System;

public class ActorController : MonoBehaviour
{
    public ChoreographyHandler choreographyHandler;
    public GameDataManger gameDataManger;

    public Directions directions;
    Invoker invoker;

    public NavMeshAgent navMeshAgent;
    public Camera xrCamera;
    public ThirdPersonCharacter thirdPersonCharacter;

    protected PhraseRecognizer phraseRecognizer;
    public ConfidenceLevel speechConfidenceLevel = ConfidenceLevel.Low;

    MovementStrategy movementStrategy;

    public AudioHandler audioHandler;

    void Start()
    {
        movementStrategy = new AnimationDrivenMovementStrategy(navMeshAgent, xrCamera, thirdPersonCharacter);
        movementStrategy.Initialize();

        string path = Directory.GetCurrentDirectory();
        phraseRecognizer = new GrammarRecognizer(path + "/Assets/Scripts/Grammar/grammar.xml");
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
        MoveToPositionCommand moveToPositionCommand = new MoveToPositionCommand(directions);
        MoveToNextStoryBeatCommand moveToNextStoryBeatCommand = new MoveToNextStoryBeatCommand(directions);

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
                    invoker = new Invoker(actionCommand);
                    invoker.ExecuteCommand();
                    break;
                case "stopChoreography":
                    invoker = new Invoker(cutCommand);
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

            }

        }
    }

    private void SetChoreography(string choreographyName)
    {
        /*choreographyHandler.Load(choreographyName);
        gameDataManger.choreographyHandler = choreographyHandler;
        gameDataManger.Save();*/

        gameDataManger.SetChoreography(choreographyName);
        //gameDataManger.choreographyHandler.AddStoryBeat("SB_stCrispinsDay_2");
        gameDataManger.Display("SetChoreography");  
    }
}
