using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class ChoreographyHandler : MonoBehaviour
{
    private DirectoryInfo _directoryInfo;
    private string _fileName;
    public Choreography choreography;
    public List<StoryBeat> storyBeatsList = new List<StoryBeat>();

    public void Save()
    {
        string json = JsonUtility.ToJson(choreography);
        WriteToFile(_fileName, json);
    }

    public void Load(string fileName)
    {
        fileName += ".json";
        _fileName = fileName;
        choreography = new Choreography();
        string json = ReadFromFile(fileName);
        JsonUtility.FromJsonOverwrite(json, choreography);
    }

    private string ReadFromFile(string fileName)
    {
        string path = GetFilePath(fileName);

        if (!File.Exists(path))
        {
            Debug.LogWarning("File not found!");
            Debug.LogWarning("Creating new choreography!");

            choreography.screenplay = _fileName.Replace(".json", "");
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
                storyBeatsList.Add((StoryBeat)JsonUtility.FromJson(json, typeof(StoryBeat)));
            }
        }

        //Debug.Log(choreography[0].speechAudioClip);
        //Debug.Log(choreography[1].speechAudioClip);
    }

    public void AddStoryBeat(string storyBeatName)
    {
        string path = GetFilePath() + "/" + storyBeatName;

        using (StreamReader reader = new StreamReader(path))
        {
            string json = File.ReadAllText(path);
            storyBeatsList.Add((StoryBeat)JsonUtility.FromJson(json, typeof(StoryBeat)));
        }
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
}
