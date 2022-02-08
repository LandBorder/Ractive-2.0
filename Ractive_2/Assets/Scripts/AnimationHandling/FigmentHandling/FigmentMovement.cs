using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigmentMovement : MonoBehaviour
{
    private float _timeCounter;
    private GameObject _actor;

    public float speed;
    public float circleWidth;
    public float circleHeight;

    // Start is called before the first frame update
    void Start()
    {
        _timeCounter = 0;
        _actor = GameObject.Find("Actor");

        speed = 0.5f;
        circleWidth = 0.6f;
        circleHeight = 0.2f;
    }

    // Update is called once per frame
    void Update()
    {
        /*_timeCounter += Time.deltaTime * speed;

        float xOffsett = Mathf.Cos(_timeCounter) * circleWidth;
        float yOffset = Mathf.Sin(_timeCounter) * circleHeight + 1.4f;

        // Has to be in front of actor
        float zOffset = (-1) + _actor.transform.position.z;
        */
        //transform.position = new Vector3(transform.position.x + xOffsett, transform.position.y + yOffset, transform.position.z + zOffset);
    }
}
