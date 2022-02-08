using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FacialExpressionController : MonoBehaviour
{
    private int _currentExpression;
    [SerializeField] private int _newExpression;
    [SerializeField] private int _expressionValue;

    public SkinnedMeshRenderer skinnedMeshRenderer;

    Dictionary<string, int> FacialExpressionsDictionary = new Dictionary<string, int>()
    {
        {"admiration", 16},
        {"angry", 17},
        {"confused", 19},
        {"content", 20},
        {"crying", 21},
        {"determination", 22},
        {"disgusted", 23},
        {"drunken", 24},
        {"enchanting", 25},
        {"fear", 26},
        {"happy", 27},
        {"laughter", 28},
        {"maniacal", 29},
        {"perplexed", 30},
        {"sad", 31},
        {"stern", 32},
        {"terror", 33}
    };

    void Start()
    {
        //skinnedMeshRenderer.SetBlendShapeWeight(FacialExpressionsDictionary["stern"], 70); 
        _currentExpression = 0;
        _newExpression = 0;
    }

    private void Update()
    {
       /* // Sets gradually new facial expression if currently no expression is set
        if (_newExpression != 0 && _currentExpression == 0)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(_newExpression, Mathf.Lerp(skinnedMeshRenderer.GetBlendShapeWeight(_newExpression), _expressionValue, Time.deltaTime * 1f));
            _currentExpression = _newExpression;
        }

        // Decreases current expression while increasing new expression
        if (_newExpression != 0 && _currentExpression != 0 && _newExpression != _currentExpression)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(_currentExpression, Mathf.Lerp(0, skinnedMeshRenderer.GetBlendShapeWeight(_currentExpression), Time.deltaTime * 1f));
            skinnedMeshRenderer.SetBlendShapeWeight(_newExpression, Mathf.Lerp(skinnedMeshRenderer.GetBlendShapeWeight(_newExpression), _expressionValue, Time.deltaTime * 1f));
            _currentExpression = _newExpression;
        }*/
    }

    public void SetFacialExpression(string expressionName, int expressionValue)
    {
        //skinnedMeshRenderer.SetBlendShapeWeight(FacialExpressionsDictionary[expressionName], expressionValue);
        _newExpression = FacialExpressionsDictionary[expressionName];
        _expressionValue = expressionValue;
    }

}
