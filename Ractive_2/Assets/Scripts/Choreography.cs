using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class Choreography : MonoBehaviour
{
    private DirectoryInfo _directoryInfo;
    public List<StoryBeat> choreography = new List<StoryBeat>();

    public void AddAllStoryBeats()
    {
        _directoryInfo = new DirectoryInfo(GetFilePath());

        var files = _directoryInfo.GetFiles().Where(o => o.Name.EndsWith(".json")).ToArray();

        for (int i = 0; i < files.Length; i++)
        {
            using (StreamReader reader = new StreamReader(files[i].FullName))
            {
                string json = File.ReadAllText(files[i].FullName);
                choreography.Add((StoryBeat)JsonUtility.FromJson(json, typeof(StoryBeat)));
            }
        }

        Debug.Log(choreography[0].speechAudioClip);
        Debug.Log(choreography[1].speechAudioClip);
    }

    public void AddStoryBeat(string storyBeatName)
    {
        string path = GetFilePath() + "/" + storyBeatName;

        using (StreamReader reader = new StreamReader(path))
        {
            string json = File.ReadAllText(path);
            choreography.Add((StoryBeat)JsonUtility.FromJson(json, typeof(StoryBeat)));
        }
    }

    private string GetFilePath()
    {
        string path = Directory.GetCurrentDirectory();
        return (path + "/Assets/Scripts/StoryBeats");
    }
}
