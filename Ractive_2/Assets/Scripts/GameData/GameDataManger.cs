using System.IO;
using UnityEngine;

public class GameDataManger : MonoBehaviour
{
    private string _fileName = "GameData.json";
    private GameData _gameData;

    public ChoreographyHandler choreographyHandler;

    
    public void Save()
    {
        choreographyHandler.Save();
        string json = JsonUtility.ToJson(_gameData);
        WriteToFile(json);
    }

    public void Load()
    {
        _gameData = new GameData();
        string json = ReadFromFile();
        JsonUtility.FromJsonOverwrite(json, _gameData);

        choreographyHandler = _gameData.currentChoreographyHandler;
    }

    public void SetChoreography(string choreographyName)
    {
        choreographyHandler.Load(choreographyName);
        Save();
    }

    public void Display(string id)
    {
        Debug.Log(id + ": " + choreographyHandler.choreography.screenplay);
    }

    private void WriteToFile(string json)
    {
        string path = GetFilePath();
        FileStream fileStream = new FileStream(path, FileMode.Create);

        using (StreamWriter writer = new StreamWriter(fileStream))
        {
            writer.Write(json);
        }
    }

    private string ReadFromFile()
    {
        string path = GetFilePath();

        if (!File.Exists(path))
        {
            Debug.LogWarning("GameData file not found!");
            Debug.LogWarning("Creating new Game Data!");
            _gameData.test = "hi";
            _gameData.currentChoreographyHandler = choreographyHandler;

            Save();
        }

        using (StreamReader reader = new StreamReader(path))
        {
            string json = reader.ReadToEnd();
            return json;
        } 
    }

    private string GetFilePath()
    {
        string path = Directory.GetCurrentDirectory();
        return (path + "/Assets/Scripts/GameData/GameData.json");
    }
}
