using RogoDigital.Lipsync;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In here all Rogo Digital files are referenced. 
// e.g. LipSync Clips, Emotions, Eye Movement

public class LipSyncLoader : MonoBehaviour
{
    [SerializeField] private LipSyncData _tearsInRainClip;
    [SerializeField] private LipSyncData _tearsInRainClipAngry;
    [SerializeField] private LipSyncData _tearsInRainClipContent;

    [SerializeField] private LipSyncData _stCrispinsDayClip;
    [SerializeField] private LipSyncData _stCrispinsDayClipHappy;
    [SerializeField] private LipSyncData _stCrispinsDayClipBattle;
    [SerializeField] private LipSyncData _stCrispinsDayClipAngry;

    public LipSyncData GetClip(string clipName)
    {
        switch (clipName)
        {
            case "tears_in_rain":
                return _tearsInRainClip;
            case "stCrispinsDay":
                return _stCrispinsDayClip;
            default:
                Debug.LogWarning("No LipSync Clip with the name " + clipName + " found!");
                return null;
        }
    }

    public LipSyncData GetClipWithEmotion(string clipName, string emotion)
    {
        if (clipName == "tears_in_rain")
        {
            switch (emotion)
            {
                case "angry":
                case "determination":
                case "stern":
                case "maniacal":
                    return _tearsInRainClipAngry;
                case "admiration":
                case "content":
                case "enchanting":
                case "happy":
                case "confused":
                case "perplexed":
                case "laughter":
                    return _tearsInRainClipContent;
                default:
                    return _tearsInRainClip;
            }
        }
        else if (clipName == "stCrispinsDay")
        {
            switch (emotion)
            {
                case "angry":
                case "stern":
                case "disgusted":
                    //Debug.Log("Set Clip Fury");
                    return _stCrispinsDayClipAngry;
                case "determination":
                case "maniacal":
                    //Debug.Log("Set Clip Battle");
                    return _stCrispinsDayClipBattle;
                case "admiration":
                case "content":
                case "enchanting":
                case "happy":
                case "laughter":
                    //Debug.Log("Set Clip Happy");
                    return _stCrispinsDayClipHappy;
                default:
                    //Debug.Log("Set Clip Serious");
                    return _stCrispinsDayClip;
            }
        }
        else
        {
            Debug.LogWarning("No LipSync Clip with the name " + clipName + " found!");
            return null;
        }
    }
}
