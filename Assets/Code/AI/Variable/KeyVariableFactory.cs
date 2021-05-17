using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class KeyVariableFactory
{
    #region Int Variable

    public static IntVariable CreateIntVariable(KeyVariableInfo info)
    {
        IntVariable variable = ScriptableObject.CreateInstance<IntVariable>();
        variable.VariableName = info.VariableName;
        return variable;
    }
    public static IntVariable CreateIntVariable(KeyVariableInfo info, int startingValue)
    {
        IntVariable variable = ScriptableObject.CreateInstance<IntVariable>();
        variable.VariableName = info.VariableName;
        variable.SetValue(startingValue);
        return variable;
    }

    #endregion

    #region Float Variable

    // public static FloatVariable CreateFloatVariable(KeyVariableInfo info)
    // {
    //     FloatVariable variable = ScriptableObject.CreateInstance<FloatVariable>();
    //     variable.VariableName = info.VariableName;
    //     return variable;
    // }
    // public static FloatVariable CreateFloatVariable(KeyVariableInfo info, float startingValue)
    // {
    //     FloatVariable variable = ScriptableObject.CreateInstance<FloatVariable>();
    //     variable.VariableName = info.VariableName;
    //     variable.SetValue(startingValue);
    //     return variable;
    // }

    #endregion

    #region Bool Variable

    // public static BoolVariable CreateBoolVariable(KeyVariableInfo info)
    // {
    //     BoolVariable variable = ScriptableObject.CreateInstance<BoolVariable>();
    //     variable.VariableName = info.VariableName;
    //     return variable;
    // }
    // public static BoolVariable CreateBoolVariable(KeyVariableInfo info, bool startingValue)
    // {
    //     BoolVariable variable = ScriptableObject.CreateInstance<BoolVariable>();
    //     variable.VariableName = info.VariableName;
    //     variable.SetValue(startingValue);
    //     return variable;
    // }

    #endregion
    
    #region Vector3 Variable

    public static Vector3Variable CreateVector3Variable(KeyVariableInfo info)
    {
        Vector3Variable variable = ScriptableObject.CreateInstance<Vector3Variable>();
        variable.VariableName = info.VariableName;
        return variable;
    }
    public static Vector3Variable CreateVector3Variable(KeyVariableInfo info, Vector3 startingValue)
    {
        Vector3Variable variable = ScriptableObject.CreateInstance<Vector3Variable>();
        variable.VariableName = info.VariableName;
        variable.SetValue(startingValue);
        return variable;
    }

    #endregion
    
    #region GameObject Variable

    public static GameObjectVariable CreateGameObjectVariable(KeyVariableInfo info)
    {
        GameObjectVariable variable = ScriptableObject.CreateInstance<GameObjectVariable>();
        variable.VariableName = info.VariableName;
        return variable;
    }
    public static GameObjectVariable CreateGameObjectVariable(KeyVariableInfo info, GameObject startingValue)
    {
        GameObjectVariable variable = ScriptableObject.CreateInstance<GameObjectVariable>();
        variable.VariableName = info.VariableName;
        variable.SetValue(startingValue);
        return variable;
    }

    #endregion
    
}

