using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Handles the JSON files in which the state of the actor is defined 

public class StoryBeatHandler : MonoBehaviour
{
    public StoryBeat storyBeat;
    private string _fileName = "storybeat_1.json";

    public void Save()
    {
        string json = JsonUtility.ToJson(storyBeat);
        WriteToFile(_fileName, json);
    }

    public void Load()
    {
        storyBeat = new StoryBeat();
        string json = ReadFromFile(_fileName);
        JsonUtility.FromJsonOverwrite(json, storyBeat);

        storyBeat.name = _fileName;
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

    private string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);

        if (File.Exists(path))
        {
            using (StreamReader reader = new StreamReader(path))
            {
                string json = reader.ReadToEnd();
                return json;
            }
        }
        else
        {
            Debug.LogWarning("File not found!");
            return "";
        }
    }

    // Returns name of newly created story beat.
    public string CreateNewStoryBeat(string storyBeatName = "storybeat")
    {
        storyBeatName += "_";

        for (int fileNumber = 1; fileNumber <= 20 ; ++fileNumber)
        {
            storyBeatName += fileNumber;

            string path = GetFilePath(storyBeatName + ".json");

            if (!File.Exists(path))
            {
                storyBeat = new StoryBeat();
                storyBeat.name = storyBeatName;
                storyBeat.audioControlCommand = ChoreographyHandler.AudioControlCommand.None;

                string json = JsonUtility.ToJson(storyBeat);
                WriteToFile(storyBeatName + ".json", json);
                return storyBeatName;
            }
            else
            {
                storyBeatName = storyBeatName.Remove(storyBeatName.Length - 1);
            }
        }

        Debug.LogWarning("Story Beat Creation failed!");
        return "";
    }

    private string GetFilePath()
    {
        string path = Directory.GetCurrentDirectory();
        return (path + "/Assets/Scripts/StoryBeats");
    }

    private string GetFilePath(string fileName)
    {
        //return Application.persistentDataPath + "/" + fileName;
        string path = Directory.GetCurrentDirectory();
        return (path + "/Assets/Scripts/StoryBeats/" + fileName);
    }
}
