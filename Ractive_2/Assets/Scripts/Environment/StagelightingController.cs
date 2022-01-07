using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagelightingController : MonoBehaviour
{
    public AudioSource lightSound;
    public GameObject actor;
    public Light directionalLight;
    public Light spotlight;

    void Update()
    {
        transform.LookAt(actor.transform);
    }

    public void ActivateStageLighting()
    {
        StartCoroutine(SwitchLightOnWithSound());
    }

    public void DectivateStageLighting()
    {
        StartCoroutine(SwitchLightOffWithSound());
    }

    private IEnumerator SwitchLightOnWithSound()  
    {
        lightSound.Play();
        yield return new WaitForSeconds(0.2f);
        directionalLight.intensity = 0;
        spotlight.intensity = 1;
    }

    private IEnumerator SwitchLightOffWithSound()
    {
        lightSound.Play();
        yield return new WaitForSeconds(0.2f);
        directionalLight.intensity = 1;
        spotlight.intensity = 0;
    }
}
