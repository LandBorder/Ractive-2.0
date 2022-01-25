using RogoDigital.Lipsync;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// In here all Rogo Digital files are referenced. 
// e.g. LipSync Clips, Emotions, Eye Movement

public class LipSyncLoader : MonoBehaviour
{
    [SerializeField] private LipSyncData _tearsInRainClip;
    [SerializeField] private LipSyncData _stCrispinsDayClip;

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
}
