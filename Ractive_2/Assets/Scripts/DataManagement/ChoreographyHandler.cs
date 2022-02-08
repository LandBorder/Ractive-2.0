using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.AI;

[System.Serializable]

public class ChoreographyHandler : MonoBehaviour
{
    private DirectoryInfo _directoryInfo;
    private string _fileName;
    private StoryBeatHandler _storyBeatHandler; 

    public Choreography choreography;
    public List<StoryBeat> storyBeatList = new List<StoryBeat>();
    public StoryBeat currentStoryBeat;
    public AudioHandler audioHandler;
    public AnimationHandler animationHandler;

    public enum AudioControlCommand { None, Start, Pause, Stop }

    public void Save()
    {
        choreography.storyBeatList = storyBeatList;
        string json = JsonUtility.ToJson(choreography);
        WriteToFile(_fileName, json);
    }

    public void Load(string fileName)
    {
        _fileName = fileName + ".json";
        choreography = new Choreography();
        string json = ReadFromFile(_fileName);
        Debug.Log(json);
        JsonUtility.FromJsonOverwrite(json, choreography);

        // Check if choreography already has saved story beats
        if (!IsEmpty(choreography.storyBeatList))
        {
            storyBeatList = choreography.storyBeatList;
            currentStoryBeat = storyBeatList.First();
        }

        PrintStoryBeatList();
    }

    private string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);

        if (!File.Exists(path))
        {
            Debug.LogWarning("Choreography file not found!");
            Debug.LogWarning("Creating new choreography!");

            choreography.screenplay = _fileName.Replace(".json", "");

            NewStoryBeat();
            Save();
             
        }
       
        using (StreamReader reader = new StreamReader(path))
        {
            string json = reader.ReadToEnd();
            return json;
        }
    }

    private void WriteToFile(string fileName, string json)
    {
        string path = GetFilePath(fileName);
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    public void AddStoryBeat(string storyBeatName)
    {
        storyBeatName += ".json";
        string path = GetFilePath(storyBeatName);

        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                storyBeatList.Add((StoryBeat)JsonUtility.FromJson(json, typeof(StoryBeat)));
            }

            if(currentStoryBeat == null)
            {
                currentStoryBeat = storyBeatList.First();
            }

            Debug.Log("StoryBeat " + storyBeatName + " added.");
        }
        else
        {
            Debug.LogWarning("StoryBeat " + storyBeatName + " not found.");
        }
    }

    public void AddStoryBeat(StoryBeat storyBeat)
    {

        storyBeatList.Add(storyBeat);

        if (currentStoryBeat == null)
        {
            currentStoryBeat = storyBeatList.First();
        }

    }

    public void DeleteStoryBeat(string storyBeatName)
    {
        var itemToRemove = storyBeatList.Single(storyBeat => storyBeat.name == storyBeatName);
        storyBeatList.Remove(itemToRemove);
    }

    public void NextStoryBeat()
    {
        // Creates new storybeat when list reaches end

        if (currentStoryBeat != null)
        {
            currentStoryBeat = storyBeatList.SkipWhile(i => i != currentStoryBeat).Skip(1).FirstOrDefault();

            //var indexOf = storyBeatList.IndexOf(currentStoryBeat);
            //currentStoryBeat = storyBeatList[indexOf == storyBeatList.Count - 1 ? 0 : indexOf + 1];

            if (currentStoryBeat == null)
            {
                //Debug.Log("Creating new storybeat.");
                NewStoryBeat();
                //currentStoryBeat = storyBeatList.SkipWhile(i => i != currentStoryBeat).Skip(1).DefaultIfEmpty(storyBeatList[0]).FirstOrDefault();
                currentStoryBeat = storyBeatList.LastOrDefault();
                Save();
            }
        }
        else
        {
            Debug.LogWarning("No Current Story Beat set.");
        }
    }

    public void NewStoryBeat()
    {
        string storyBeatName = "SB_" + choreography.screenplay; 
        _storyBeatHandler = new StoryBeatHandler();
        string newStoryBeat;

        // Checks if it is the first storybeat to be added. If not, some previous 
        // values will need to be passed to the new storybeat.
        if (storyBeatList.Count == 0)
        {
            newStoryBeat = _storyBeatHandler.CreateNewStoryBeat(storyBeatName);
        }
        else
        {
            newStoryBeat = _storyBeatHandler.CreateNewStoryBeat(storyBeatName, storyBeatList.Last());
        }

        AddStoryBeat(newStoryBeat);
    }

    public IEnumerator Execute(GameObject actor)
    {
        // TODO: Possibly one seperate Execute for each component of the storybeat (movement, speech etc.)
        //       Because movement and speech might need to be executed concurently, but in this set up one 
        //       yield would stall the execution of the next component.


        // TODO: If one storybeat has no target position this component has to be set to the agents current position instead of 0,0,0
        NavMeshAgent navMeshAgent = actor.GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;

        PrintStoryBeatList();
        foreach (StoryBeat storyBeat in storyBeatList)
        {
            Debug.Log("Executing StoryBeat: " + storyBeat.name);

            // Audio
            Debug.Log("Audio Control Command: " + storyBeat.audioControlCommand);
            ControlAudio(storyBeat.audioControlCommand);

            // Movement
            Debug.Log("Target position: " + storyBeat.targetPosition);
            navMeshAgent.SetDestination(storyBeat.targetPosition);

            // Wait until the target position of the storybeat is reached
            while (navMeshAgent.pathPending || navMeshAgent.remainingDistance > 0.5f)
            {
                //ExecuteStoryBeat(actor, storyBeat);
                yield return null;
            }

            // Animation
            animationHandler.TriggerAnimation(storyBeat.animationName);
        }

        Debug.Log("Finished executing StoryBeats");
    }

    public void ExecuteStoryBeat(GameObject actor, StoryBeat storyBeat)
    {
        NavMeshAgent navMeshAgent = actor.GetComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        navMeshAgent.SetDestination(storyBeat.targetPosition);
        Debug.Log("Target position: " + storyBeat.targetPosition);

    }

    public void SetCurrentStoryBeatToStartOfChoreography()
    {
        currentStoryBeat = storyBeatList.First();
    }

    public void ControlAudio(ChoreographyHandler.AudioControlCommand enumMember)
    {
        switch (enumMember)
        {
            case ChoreographyHandler.AudioControlCommand.Start:
                StartAudio();
                break;
            case ChoreographyHandler.AudioControlCommand.Pause:
                PauseAudio();
                break;
            case ChoreographyHandler.AudioControlCommand.Stop:
                StopAudio();
                break;
            case ChoreographyHandler.AudioControlCommand.None:
                break;
        }
    }

    public void StartAudio()
    {
        audioHandler.StartAudioFile();
    }

    public void PauseAudio()
    {
        audioHandler.PauseAudioFile();
    }

    public void StopAudio()
    {
        audioHandler.StopAudioFile();
    }

    /*
     * ------------------------------------------------------------------------
     * Debug Functions
     * 
     */

    public void PrintStoryBeatList()
    {
        if (!IsEmpty(storyBeatList))
        {
            string allStoryBeats = "";
            foreach (var item in storyBeatList)
            {
                allStoryBeats = allStoryBeats + item.name + ", ";
            }

            Debug.Log("\"" + choreography.screenplay + "\"" + " contains: " + allStoryBeats);
        }
        else
        {
            Debug.LogWarning("Story Beat List is empty!");
        }
    }


    public void AddAllStoryBeats()
    {
        _directoryInfo = new DirectoryInfo(GetFilePath());

        var files = _directoryInfo.GetFiles().Where(o => o.Name.EndsWith(".json")).ToArray();

        for (int i = 0; i < files.Length; i++)
        {
            using (StreamReader reader = new StreamReader(files[i].FullName))
            {
                string json = File.ReadAllText(files[i].FullName);
                storyBeatList.Add((StoryBeat)JsonUtility.FromJson(json, typeof(StoryBeat)));
            }
        }
    }

    /*
     * ------------------------------------------------------------------------
     */

    private string GetFilePath()
{
    string path = Directory.GetCurrentDirectory();
    return (path + "/Assets/Scripts/StoryBeats");
}

private string GetFilePath(string fileName)
{
    string path = Directory.GetCurrentDirectory();
    return (path + "/Assets/Scripts/StoryBeats/" + fileName);
}

private static bool IsEmpty<StoryBeat>(List<StoryBeat> list)
{
    if (list == null)
    {
        return true;
    }

    return !list.Any();
}

}
