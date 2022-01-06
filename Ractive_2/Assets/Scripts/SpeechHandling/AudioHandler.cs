using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Delete

public class AudioHandler : MonoBehaviour
{
    private StoryBeatHandler _storyBeatHandler = new StoryBeatHandler();
        
    public AudioClip stCrispinsDaySpeech;
    public AudioClip tearsInRain;

    public void SetAudioClip(string clipName)
    {
        _storyBeatHandler.Load();
         
        switch (clipName)
        {
            case "tears_in_rain":
                _storyBeatHandler.storyBeat.speechAudioClip = tearsInRain;
                break;
            case "stCrispinsDay":
                _storyBeatHandler.storyBeat.speechAudioClip = stCrispinsDaySpeech;
                break;
        }

        _storyBeatHandler.Save();
    }
}
