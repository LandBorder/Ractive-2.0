using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagelightingController : MonoBehaviour
{
    public AudioSource lightSound;
    public GameObject actor;
    public Light directionalLight;
    public Light spotlightTop;
    public Light spotlightFront;
    public Light spotlightBack;

    void Update()
    {
        transform.LookAt(actor.transform);
    }

    public void ActivateStageLighting()
    {
        StartCoroutine(SwitchLightOnWithSound());
    }

    public void DeactivateStageLighting()
    {
        StartCoroutine(SwitchLightOffWithSound());
    }

    private IEnumerator SwitchLightOnWithSound()  
    {
        lightSound.Play();
        yield return new WaitForSeconds(0.2f);
        directionalLight.intensity = 0.3f;
        spotlightTop.intensity = 1;
        spotlightFront.intensity = 1;
        spotlightBack.intensity = 1;
    }

    private IEnumerator SwitchLightOffWithSound()
    {
        lightSound.Play();
        yield return new WaitForSeconds(0.2f);
        directionalLight.intensity = 1;
        spotlightTop.intensity = 0;
        spotlightFront.intensity = 0;
        spotlightBack.intensity = 0;
    }
}
