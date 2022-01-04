using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

// Handles the JSON files in which the state of the actor is defined 

public class DataManager : MonoBehaviour
{
    public StoryBeat storyBeat;
    private string _fileName = "storybeat.txt";

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

    private string GetFilePath(string fileName)
    {
        return Application.persistentDataPath + "/" + fileName;
    }
}
