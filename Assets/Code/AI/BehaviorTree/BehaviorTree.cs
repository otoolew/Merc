using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BehaviorTree : MonoBehaviour
{
    [SerializeField] private AICharacter characterOwner;
    public AICharacter CharacterOwner { get => characterOwner; set => characterOwner = value; }
    
    [Header("Available Behavior Nodes")]
    [SerializeField] private List<BehaviorTask> behaviorTasksList;
    public List<BehaviorTask> BehaviorTasksList { get => behaviorTasksList; set => behaviorTasksList = value; }
    
    [SerializeField] private UnityDictionary<string, KeyVariable> variableDictionary;
    public UnityDictionary<string, KeyVariable> VariableDictionary { get => variableDictionary; set => variableDictionary = value; }

    [SerializeField] private UnityDictionary<string, BehaviorTask> taskDictionary;
    public UnityDictionary<string, BehaviorTask> TaskDictionary { get => taskDictionary; set => taskDictionary = value; }
    
    private void Start()
    {
        SetUp();
        SetInt("SomeCount", 10);
    }

    public void SetUp()
    {
        for (int i = 0; i < behaviorTasksList.Count; i++)
        {
            BehaviorTask task = Instantiate(behaviorTasksList[i]);
            task.AssignedBehaviorTree = this;
            if (!taskDictionary.ContainsKey(task.TaskName))
            {
                taskDictionary.Add(task.TaskName, task);
                LoadKeyVariables(task);
            }
        }
    }

    private void LoadKeyVariables(BehaviorTask behaviorTask)
    {
        for (int i = 0; i < behaviorTask.KeyVariables.Length; i++)
        {
            switch (behaviorTask.KeyVariables[i].VariableType)
            {
                case KeyVariableType.INT:
                    variableDictionary.Add(behaviorTask.KeyVariables[i].VariableName, KeyVariableFactory.CreateIntVariable(behaviorTask.KeyVariables[i], 0));
                    break;
                case KeyVariableType.FLOAT:
                    variableDictionary.Add(behaviorTask.KeyVariables[i].VariableName, KeyVariableFactory.CreateFloatVariable(behaviorTask.KeyVariables[i], 0.0f));
                    break;
                case KeyVariableType.BOOL:
                    variableDictionary.Add(behaviorTask.KeyVariables[i].VariableName, KeyVariableFactory.CreateBoolVariable(behaviorTask.KeyVariables[i], false));
                    break;
                case KeyVariableType.VECTOR3:
                    variableDictionary.Add(behaviorTask.KeyVariables[i].VariableName, KeyVariableFactory.CreateVector3Variable(behaviorTask.KeyVariables[i], Vector3.zero));
                    break;
                case KeyVariableType.GAMEOBJECT:
                    variableDictionary.Add(behaviorTask.KeyVariables[i].VariableName, KeyVariableFactory.CreateGameObjectVariable(behaviorTask.KeyVariables[i], null));
                    break;
                default:
                    Debug.Log("Key Variable Type is not supported");
                    break;
            }
        }
    }

    public void PushTask(string taskName)
    {
        
    }
    
    
    #region BehaviorVariables

    #region Int

    public void SetInt(string variableName, int value)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            IntVariable floatVariable = (IntVariable) variable;
            floatVariable.SetValue(value);
        }
    }

    public int GetIntValue(string variableName)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            IntVariable intVariable = (IntVariable) variable;
            return (int) intVariable.GetValue();
        }

        return 0;
    }

    #endregion


    #region Float

    public void SetFloat(string variableName, float value)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            FloatVariable floatVariable = (FloatVariable) variable;
            floatVariable.SetValue(value);
        }
    }

    public float GetFloatValue(string variableName)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            FloatVariable floatVariable = (FloatVariable) variable;
            return (float) floatVariable.GetValue();
        }
        return 0.0f;
    }

    #endregion

    #region Bool

    public void SetBool(string variableName, bool value)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            BoolVariable boolVariable = (BoolVariable) variable;
            boolVariable.SetValue(value);
        }
    }

    public bool GetBoolValue(string variableName)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            BoolVariable boolVariable = (BoolVariable) variable;
            return (bool) boolVariable.GetValue();
        }

        return false;
    }

    #endregion


    #region Vector3

    public void SetVector3(string variableName, Vector3 value)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            Vector3Variable vector3Variable = (Vector3Variable) variable;
            vector3Variable.SetValue(value);
        }
    }

    public Vector3 GetVector3Value(string variableName)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            Vector3Variable vector3Variable = (Vector3Variable) variable;
            return (Vector3) vector3Variable.GetValue();
        }

        return new Vector3(0, 0, 0);
    }

    #endregion

    #region GameObject

    public void SetGameObject(string variableName, GameObject value)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            GameObjectVariable gameObjectVariable = (GameObjectVariable) variable;
            gameObjectVariable.SetValue(value);
        }
    }

    public GameObject GetGameObjectValue(string variableName)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            GameObjectVariable gameObjectVariable = (GameObjectVariable) variable;
            return (GameObject) gameObjectVariable.GetValue();
        }

        return null;
    }

    #endregion

    #endregion
}