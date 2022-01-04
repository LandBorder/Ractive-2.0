using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class ThirdPersonCharacterParameter : Parameter
{
    private ThirdPersonCharacter _value;
    public ThirdPersonCharacter GetValue() { return _value; }
    public void SetValue(ThirdPersonCharacter value) { _value = value; }
    public ThirdPersonCharacterParameter(string name, ThirdPersonCharacter defaultvalue) : base(name)
    {
        _value = defaultvalue;
    }
}
