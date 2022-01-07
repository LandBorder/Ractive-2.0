using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[System.Serializable]

public class ChoreographyHandler : MonoBehaviour
{
    private DirectoryInfo _directoryInfo;
    private string _fileName;
    private StoryBeatHandler storyBeatHandler; 

    public Choreography choreography;
    public List<StoryBeat> storyBeatList = new List<StoryBeat>();
    public StoryBeat currentStoryBeat;

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

        //Debug.Log(choreography[0].speechAudioClip);
        //Debug.Log(choreography[1].speechAudioClip);
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
        // TODO: When list reaches end create new story beat

        if (currentStoryBeat != null)
        {
            currentStoryBeat = storyBeatList.SkipWhile(i => i != currentStoryBeat).Skip(1).First();
        }
        else
        {
            Debug.LogWarning("No Current Story Beat set.");
        }
    }

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

    public void NewStoryBeat()
    {
        string storyBeatName = "SB_" + choreography.screenplay; 
        storyBeatHandler = new StoryBeatHandler();
        string newStoryBeat = storyBeatHandler.CreateNewStoryBeat(storyBeatName);
        AddStoryBeat(newStoryBeat);
    }

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
