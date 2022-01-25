using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RogoDigital.Lipsync;
using System.IO;
using UnityEngine.Networking;

public class AudioHandler : MonoBehaviour
{
    public AudioSource audioSource;
    public LipSync lipSyncAvatar;
    public LipSyncLoader lipSyncLoader;

    private AudioClip _audioClip;
    private string _audioPath;
    private LipSyncData _lipSyncClip;

    public void SetAudioClip(string audioName)
    {
        _audioPath = "file://" + Directory.GetCurrentDirectory() + "/Assets/Audio/";
        StartCoroutine(LoadAudio(audioName + ".wav"));

        audioSource.clip = _audioClip;
        _lipSyncClip = lipSyncLoader.GetClip(audioName);
    }

    public void StartAudioFile()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }

        if (_lipSyncClip != null)
        {
            lipSyncAvatar.Play(_lipSyncClip);
        }
    }

    public void PauseAudioFile()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Pause();
        }

        if (lipSyncAvatar.IsPlaying)
        {
            lipSyncAvatar.Pause();
        }
    }

    // Called on Cut!
    public void StopAudioFile()
    {
        audioSource.Stop();
        //lipSyncAvatar.Stop(false);
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
