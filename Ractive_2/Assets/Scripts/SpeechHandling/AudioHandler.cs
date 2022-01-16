using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour
{
    public AudioSource audioSource;
    private AudioClip _audioClip;
    private string _audioPath;

    public void SetAudioClip(string audioName)
    {
        _audioPath = "file://" + Application.streamingAssetsPath + "/Audio/";
        StartCoroutine(LoadAudio(audioName));
    }

    public void StartAudioFile()
    {
        audioSource.clip = _audioClip;

        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void PauseAudioFile()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }
    }

    // Called on Cut!
    public void StopAudioFile()
    {
        audioSource.Stop();
    }

    private IEnumerator LoadAudio(string audioName)
    {
        WWW request = GetAudioFromFile(_audioPath, audioName);
        yield return request;

        _audioClip = request.GetAudioClip();
        _audioClip.name = audioName;
    }

    private WWW GetAudioFromFile(string path, string fileName)
    {
        string audioToLoad = path + fileName;
        WWW request = new WWW(audioToLoad);
        return request;
    }
}
