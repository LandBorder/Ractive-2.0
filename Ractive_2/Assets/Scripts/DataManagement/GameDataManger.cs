using System.IO;
using UnityEngine;

public class GameDataManger : MonoBehaviour
{
    public ChoreographyHandler choreographyHandler;

    public void Save()
    {
        if(choreographyHandler != null) 
        {
            choreographyHandler.Save();
        }

        //string json = JsonUtility.ToJson(_gameData);
        //WriteToFile(json);
    }

    public void Load()
    {
        choreographyHandler = GameObject.FindObjectOfType<ChoreographyHandler>();

        if (choreographyHandler == null)
        {
            Debug.LogError("No Choreography Handler found.");
        }
    }

    public void SetChoreography(string choreographyName)
    {
        choreographyHandler.storyBeatList.Clear();
        choreographyHandler.Load(choreographyName);
        choreographyHandler.audioHandler.SetAudioClip(choreographyName);
        Save();
    }

    public void Display(string id)
    {
        Debug.Log(id + ": " + choreographyHandler.choreography.screenplay);
    }

    // DELETE
   /* private void WriteToFile(string json)
    {
        string path = GetFilePath();
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    // DELETE
    private string ReadFromFile()
    {
        string path = GetFilePath();

        if (!File.Exists(path))
        {
            Debug.LogWarning("GameData file not found!");
            Debug.LogWarning("Creating new Game Data!");

            _gameData.currentChoreographyHandler = choreographyHandler; // <- save guid (id changes at restart)
            _gameData.choreographyHandlerName = choreographyHandler.name;
            Save();
        }

        using (StreamReader reader = new StreamReader(path))
        {
            string json = reader.ReadToEnd();
            return json;
        } 
    }

    // DELETE
    private string GetFilePath()
    {
        string path = Directory.GetCurrentDirectory();
        return (path + "/Assets/Scripts/GameData/GameData.json");
    }*/
}
