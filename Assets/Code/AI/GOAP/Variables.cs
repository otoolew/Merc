using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Variables : MonoBehaviour
{
    [SerializeField] private UnityDictionary<string, Variable> variableDictionary;
    public UnityDictionary<string, Variable> VariableDictionary { get => variableDictionary; set => variableDictionary = value; }

    // public void OnVariableChanged(Variable variable)
    // {
    //     switch (variable.VariableType)
    //     {
    //         case VariableType.BOOL:
    //             SetBoolValue(variable.VariableName, );
    //             break;
    //         case VariableType.INT:
    //             break;
    //         case VariableType.FLOAT:
    //             break;
    //         case VariableType.VECTOR3:
    //             break;
    //         case VariableType.GAMEOBJECT:
    //             break;
    //         default:
    //             throw new ArgumentOutOfRangeException();
    //     }
    // }
    
    #region Int
    
    public void SetInt(string variableName, int value)
    {
        if (variableDictionary.TryGetValue(variableName, out Variable variable))
        {
            IntVariable intVariable = (IntVariable) variable;
            intVariable.SetValue(value);
        }
        else
        {
            AddIntVariable(variableName, value);
        }
    }
    
    public int GetIntValue(string variableName)
    {
        if (variableDictionary.TryGetValue(variableName, out Variable variable))
        {
            IntVariable intVariable = (IntVariable) variable;
            return (int) intVariable.GetValue();
        }
        else
        {
            AddIntVariable(variableName, 0);
        }
        return 0;
    }
    
    public void AddIntVariable(string variableName, int value)
    {
        if (!variableDictionary.ContainsKey(variableName))
        {
            variableDictionary.Add(new KeyValuePair<string, Variable>(variableName, IntVariable.Create(variableName,value)));
        }
    }
    
    #endregion
    
    #region Float
    
    public void SetFloatValue(string variableName, float value)
    {
        if (variableDictionary.TryGetValue(variableName, out Variable variable))
        {
            FloatVariable floatVariable = (FloatVariable) variable;
            floatVariable.SetValue(value);
        }
        else
        {
            AddFloatVariable(variableName, 0.0f);
        }
    }
    
    public float GetFloatValue(string variableName)
    {
        if (variableDictionary.TryGetValue(variableName, out Variable variable))
        {
            FloatVariable floatVariable = (FloatVariable) variable;
            return (float) floatVariable.GetValue();
        }
        else
        {
            AddFloatVariable(variableName, 0.0f);
        }
        
        return 0.0f;
    }
    
    public void AddFloatVariable(string variableName, float value)
    {
        if (!variableDictionary.ContainsKey(variableName))
        {
            variableDictionary.Add(new KeyValuePair<string, Variable>(variableName, FloatVariable.Create(variableName,value)));
        }
    }

    #endregion
    
    #region Bool
    
    public void SetBoolValue(string variableName, bool value)
    {
        if (variableDictionary.TryGetValue(variableName, out Variable variable))
        {
            BoolVariable boolVariable = (BoolVariable) variable;
            boolVariable.SetValue(value);
        }
        else
        {
            AddBoolVariable(variableName, false);
        }
    }
    
    public bool GetBoolValue(string variableName)
    {
        if (variableDictionary.TryGetValue(variableName, out Variable variable))
        {
            BoolVariable boolVariable = (BoolVariable) variable;
            return (bool) boolVariable.GetValue();
        }
        AddBoolVariable(variableName, false);
        return false;
    }
    
    public void AddBoolVariable(string variableName, bool value)
    {
        if (!variableDictionary.ContainsKey(variableName))
        {
            variableDictionary.Add(new KeyValuePair<string, Variable>(variableName, BoolVariable.Create(variableName,value)));
            
        }
    }
    
    #endregion
    
    #region Vector3
    
    public void SetVector3KeyValue(string variableName, Vector3 value)
    {
        if (variableDictionary.TryGetValue(variableName, out Variable variable))
        {
            Vector3Variable vector3Variable = (Vector3Variable) variable;
            vector3Variable.SetValue(value);
        } 
        else
        {
            AddVector3Variable(variableName, new Vector3(0,0,0));
        }
    }
    
    public Vector3 GetVector3KeyValue(string variableName)
    {
        if (variableDictionary.TryGetValue(variableName, out Variable variable))
        {
            Vector3Variable vector3Variable = (Vector3Variable) variable;
            return (Vector3) vector3Variable.GetValue();
        }
        else
        {
            AddVector3Variable(variableName, new Vector3(0,0,0));
        }
        return new Vector3(0, 0, 0);
    }
    
    public void AddVector3Variable(string variableName, Vector3 value)
    {
        if (!variableDictionary.ContainsKey(variableName))
        {
            variableDictionary.Add(new KeyValuePair<string, Variable>(variableName, Vector3Variable.Create(variableName,value)));
        }
    }
    
    #endregion
    
    #region GameObject
    
    public void SetGameObjectValue(string variableName, GameObject value)
    {
        if (variableDictionary.TryGetValue(variableName, out Variable variable))
        {
            GameObjectVariable gameObjectVariable = (GameObjectVariable) variable;
            gameObjectVariable.SetValue(value);
        }
        else
        {
            AddGameObjectVariable(variableName, null);
        }
    }
    public GameObject GetGameObjectValue(string variableName)
    {
        if (variableDictionary.TryGetValue(variableName, out Variable variable))
        {
            GameObjectVariable gameObjectVariable = (GameObjectVariable) variable;
            return (GameObject) gameObjectVariable.GetValue();
        }
        else
        {
            AddGameObjectVariable(variableName, null);
        }
        return null;
    }
    
    public void AddGameObjectVariable(string variableName, GameObject value)
    {
        if (!variableDictionary.ContainsKey(variableName))
        {
            variableDictionary.Add(new KeyValuePair<string, Variable>(variableName, GameObjectVariable.Create(variableName,value)));
        }
    }

    #endregion
}
