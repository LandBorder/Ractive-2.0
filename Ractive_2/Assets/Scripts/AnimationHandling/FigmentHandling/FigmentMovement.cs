using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FigmentMovement : MonoBehaviour
{
    private float _timeCounter;
    private GameObject _actor;
    private Vector3 _initPos;
    private Vector3 _movementDirection;

    public float circleWidth;
    public float circleHeight;

    public bool movement;

    void Start()
    {
        _timeCounter = 0;
        _actor = GameObject.Find("Actor");
        _initPos = transform.localPosition;
        _movementDirection = Vector3.left;

        circleWidth = 0.6f;
        circleHeight = 0.2f;
    }

    void Update()
    {
        if (movement)
        {
            //LeftRightMovement(0.3f, 1.2f);
            CircleMovement(0.4f);
        }
        else
        {
            transform.localPosition = _initPos;
        }
    }

    private void LeftRightMovement(float speed, float range)
    {
        if (transform.localPosition.x > range)
        {
            _movementDirection = Vector3.right;
        }
        if (transform.localPosition.x < ((-1f) * range))
        {
            _movementDirection = Vector3.left;
        }

        transform.Translate(_movementDirection * Time.deltaTime * speed);

    }

    private void CircleMovement(float speed)
    {
        _timeCounter += Time.deltaTime * speed;

        float xOffset = Mathf.Cos(_timeCounter) * circleWidth;
        float yOffset = Mathf.Sin(_timeCounter) * circleHeight;

        transform.localPosition = (new Vector3(xOffset, yOffset + 1.4f, 1.67f));
    }
}
