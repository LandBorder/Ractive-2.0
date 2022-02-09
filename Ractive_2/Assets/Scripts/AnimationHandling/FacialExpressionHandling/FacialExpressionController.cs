using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Does not work as planned. It should stop as soon as the right expression value is reached. But it adjusts the value all the time.

public class FacialExpressionController : MonoBehaviour
{
    [SerializeField] private int _currentExpression;
    [SerializeField] private float _currentExpressionValue;
    [SerializeField] private int _newExpression;
    [SerializeField] private int _newExpressionValue;

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
        _currentExpression = 0;
        _newExpression = 0;
        _currentExpressionValue = 0;
    }

    private void Update()
    {
        // Sets gradually new facial expression if currently no expression is set
        if (_newExpression != 0 && _currentExpression == 0)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(_newExpression, Mathf.Lerp(skinnedMeshRenderer.GetBlendShapeWeight(_newExpression), _newExpressionValue, Time.deltaTime * 1f));

            // The interpolation will not lead to the excat value, therefore an epsilon is needed 
            if (skinnedMeshRenderer.GetBlendShapeWeight(_newExpression) > (_newExpressionValue - 1.5f))
            {
                _currentExpression = _newExpression;
                _currentExpressionValue = skinnedMeshRenderer.GetBlendShapeWeight(_newExpression);
            }
        }
        // Decreases current expression while increasing new expression
        else if (_newExpression != _currentExpression && _newExpression != 0 && _currentExpression != 0)
        {
            skinnedMeshRenderer.SetBlendShapeWeight(_currentExpression, Mathf.Lerp(0, skinnedMeshRenderer.GetBlendShapeWeight(_currentExpression), Time.deltaTime * 1f));
            skinnedMeshRenderer.SetBlendShapeWeight(_newExpression, Mathf.Lerp(skinnedMeshRenderer.GetBlendShapeWeight(_newExpression), _newExpressionValue, Time.deltaTime * 1f));

            // The interpolation will not lead to the excat value, therefore an epsilon is needed 
            if (skinnedMeshRenderer.GetBlendShapeWeight(_newExpression) > (_newExpressionValue - 1.5f))
            {
                _currentExpression = _newExpression;
                _currentExpressionValue = skinnedMeshRenderer.GetBlendShapeWeight(_newExpression);
            }
        }
        // Adjust value of current facial expression
        else if(_newExpression == _currentExpression) // && (_newExpressionValue < (_currentExpressionValue - 1.5f) || _newExpressionValue > (_currentExpressionValue + 1.5f)))
        {
            skinnedMeshRenderer.SetBlendShapeWeight(_newExpression, Mathf.Lerp(skinnedMeshRenderer.GetBlendShapeWeight(_newExpression), _newExpressionValue, Time.deltaTime * 1f));

            /*if (_newExpressionValue > (_currentExpressionValue - 1.5f) && _newExpressionValue < (_currentExpressionValue + 1.5f))
            {
                _currentExpressionValue = skinnedMeshRenderer.GetBlendShapeWeight(_newExpression);
            }*/
        }
    }

    public void SetFacialExpression(string expressionName, int expressionValue)
    {
        _newExpression = FacialExpressionsDictionary[expressionName];
        _newExpressionValue = expressionValue;
    }

    public void UndoFacialExpression()
    {
        _newExpressionValue = 0;
        skinnedMeshRenderer.SetBlendShapeWeight(_currentExpression, 0);
        skinnedMeshRenderer.SetBlendShapeWeight(_newExpression, 0);
    }

}
