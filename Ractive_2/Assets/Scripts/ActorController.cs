using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;
using UnityEngine.Windows.Speech;
using System.IO;

public class ActorController : MonoBehaviour
{
    private StoryBeatHandler _storyBeatHandler = new StoryBeatHandler();

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

        //ChoreographyHandler ch = new ChoreographyHandler();
        //ch.Load("tears_in_rain");
        //ch.AddStoryBeat("storybeat_1");
        //ch.AddStoryBeat(ch.currentStoryBeat);
        /*ch.PrintStoryBeatList();
        Debug.Log(ch.currentStoryBeat.name);
        ch.NextStoryBeat();
        Debug.Log(ch.currentStoryBeat.name);*/
        //ch.NewStoryBeat();

        //ch.Save();
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
        AddSpeechToChoreographyCommand addSpeechToChoreographyCommand = new AddSpeechToChoreographyCommand(directions);

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
                    // TODO: Load "tears_in_rain" choreography
                    //       
                    audioHandler.SetAudioClip(grammarTag);
                    break;
                case "stCrispinsDay":
                    audioHandler.SetAudioClip(grammarTag);
                    break;

            }


        }
    }
}
