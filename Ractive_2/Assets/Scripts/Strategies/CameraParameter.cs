using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParameter : Parameter
{
    private Camera _value;
    public Camera GetValue() { return _value; }
    public void SetValue(Camera value) { _value = value; }
    public CameraParameter(string name, Camera defaultvalue) : base(name)
    {
        _value = defaultvalue;
    }
}
