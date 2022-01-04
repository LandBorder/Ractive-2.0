using System.Collections;
using System.Collections.Generic;

public abstract class Parameter
{
    private string _name;
    public string GetName() { return _name; }
    public Parameter(string name) { this._name = name; }
}
