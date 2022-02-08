using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class RigController : MonoBehaviour
{
    private Rig _rig;
    private float _targetWeight;
    public bool lookAtTarget;

    void Start()
    {
        _rig = GetComponent<Rig>();
        _targetWeight = 0f;
    }

    void Update()
    {
        _rig.weight = Mathf.Lerp(_rig.weight, _targetWeight, Time.deltaTime * 10f);

        if (lookAtTarget)
        {
            _targetWeight = 1f;
        }
        else
        {
            _targetWeight = 0f;
        }
    }

    public void LookAtFigment(bool shouldLookAtFigment)
    {
        if(shouldLookAtFigment)
        {
            _targetWeight = 1f;
        }
        else
        {
            _targetWeight = 0f;
        }
    }

   
}
