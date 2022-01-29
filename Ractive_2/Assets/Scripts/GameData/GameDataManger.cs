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

}
