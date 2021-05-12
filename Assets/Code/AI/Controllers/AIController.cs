using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Ludiq;
using Bolt;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    #region Components
    [Header("Components")]
    [SerializeField] private AICharacter assignedCharacter;
    public AICharacter AssignedCharacter { get => assignedCharacter; set => assignedCharacter = value; }

    [SerializeField] private Animator animatorComp;
    public Animator AnimatorComp { get => animatorComp; set => animatorComp = value; }

    [SerializeField] private LevelArea assignedLevelArea;
    public LevelArea AssignedLevelArea { get => assignedLevelArea; set => assignedLevelArea = value; }
    
    [SerializeField] private StateMachine stateMachine;
    public StateMachine StateMachine { get => stateMachine; set => stateMachine = value; }
    
    #endregion

    #region Monobehaviour
    // Start is called before the first frame update
    private void Start()
    {
        assignedCharacter.Controller = this;
        AssignedCharacter.VisionPerception.OnPerceptionUpdate.AddListener(OnPerceptionUpdate);
    }

    private void Update()
    {
        UpdateVariables();
    }

    #endregion

    #region Character Methods
    public bool PossessCharacter(AICharacter character)
    {
        assignedCharacter = character;
        assignedCharacter.Controller = this;
        return assignedCharacter;
    }

    public void UpdateVariables()
    {
        if (assignedCharacter.MovementComp.DestinationInRange)
        {
            Debug.Log("In RangE!");
        }
        Variables.Object(gameObject).Set("InPosition", assignedCharacter.MovementComp.DestinationInRange);
    }

    public void Deploy()
    {
        assignedCharacter.MovementComp.ContinueToPatrolPoint();
    }
    
    public void OnPerceptionUpdate(Character character)
    {
        if (character)
        {
            if (character.IsValid())
            {
                Variables.Object(gameObject).Set("CurrentTarget", character.gameObject);
            }
        }    
    }
    
    public void MoveToFirePoint(Vector3 pos)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized * assignedCharacter.VisionPerception.Radius;
    }
    
    #endregion

    // #region AI
    //
    // private void OnTaskFinished(BehaviorTask behaviorTask)
    // {
    //     Debug.Log("BehaviorTask Finished: " + behaviorTask.TaskName);
    // }
    //
    // #region Keys
    //
    // private void LoadKeyVariables(BehaviorTask behaviorTask)
    // {
    //     for (int i = 0; i < behaviorTask.KeyVariables.Length; i++)
    //     {
    //         switch (behaviorTask.KeyVariables[i].VariableType)
    //         {
    //             case KeyVariableType.INT:
    //                 variableDictionary.Add(behaviorTask.KeyVariables[i].VariableName, KeyVariableFactory.CreateIntVariable(behaviorTask.KeyVariables[i], 0));
    //                 break;
    //             case KeyVariableType.FLOAT:
    //                 variableDictionary.Add(behaviorTask.KeyVariables[i].VariableName, KeyVariableFactory.CreateFloatVariable(behaviorTask.KeyVariables[i], 0.0f));
    //                 break;
    //             case KeyVariableType.BOOL:
    //                 variableDictionary.Add(behaviorTask.KeyVariables[i].VariableName, KeyVariableFactory.CreateBoolVariable(behaviorTask.KeyVariables[i], false));
    //                 break;
    //             case KeyVariableType.VECTOR3:
    //                 variableDictionary.Add(behaviorTask.KeyVariables[i].VariableName, KeyVariableFactory.CreateVector3Variable(behaviorTask.KeyVariables[i], Vector3.zero));
    //                 break;
    //             case KeyVariableType.GAMEOBJECT:
    //                 variableDictionary.Add(behaviorTask.KeyVariables[i].VariableName, KeyVariableFactory.CreateGameObjectVariable(behaviorTask.KeyVariables[i], null));
    //                 break;
    //             default:
    //                 Debug.Log("Key Variable Type is not supported");
    //                 break;
    //         }
    //     }
    // }
    //
    // #endregion
    //
    //
    // #region Tasks
    // // public bool HasTask(string taskName)
    // // {
    // //     return taskDictionary.TryGetValue(taskName, out BehaviorTask behaviorTask);
    // // }
    // // public bool TransitionToTask(string taskName)
    // // {
    // //     if (taskDictionary.TryGetValue(taskName, out BehaviorTask behaviorTask))
    // //     {
    // //         //behaviorStack.Push(behaviorTask);
    // //         return true;
    // //     }
    // //     return false;
    // // }
    // public void TaskFinished(BehaviorTask behaviorTask)
    // {
    //     Debug.Log("Task " + behaviorTask.TaskName + " Finished!");
    //     // if (behaviorStack.Peek().Equals(behaviorTask) && behaviorStack.Count > 1)
    //     // {
    //     //     behaviorStack.Pop();
    //     // }
    // }
    //
    // #endregion
    //
    //
    // #region Int
    // public void SetInt(string variableName, int value)
    // {
    //     if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
    //     {
    //         IntVariable floatVariable = (IntVariable) variable;
    //         floatVariable.SetValue(value);
    //     }
    // }
    //
    // public int GetIntValue(string variableName)
    // {
    //     if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
    //     {
    //         IntVariable intVariable = (IntVariable) variable;
    //         return (int) intVariable.GetValue();
    //     }
    //
    //     return 0;
    // }
    //
    // #endregion
    //
    // #region Float
    // public void SetFloatKeyValue(string variableName, float value)
    // {
    //     if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
    //     {
    //         FloatVariable floatVariable = (FloatVariable) variable;
    //         floatVariable.SetValue(value);
    //     }
    // }
    //
    // public float GetFloatKeyValue(string variableName)
    // {
    //     if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
    //     {
    //         FloatVariable floatVariable = (FloatVariable) variable;
    //         return (float) floatVariable.GetValue();
    //     }
    //     return 0.0f;
    // }
    //
    // #endregion
    //
    // #region Bool
    //
    // public void SetBoolKeyValue(string variableName, bool value)
    // {
    //     if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
    //     {
    //         BoolVariable boolVariable = (BoolVariable) variable;
    //         boolVariable.SetValue(value);
    //     }
    // }
    //
    // public bool GetBoolKeyValue(string variableName)
    // {
    //     if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
    //     {
    //         BoolVariable boolVariable = (BoolVariable) variable;
    //         return (bool) boolVariable.GetValue();
    //     }
    //
    //     return false;
    // }
    //
    // #endregion
    //
    // #region Vector3
    //
    // public void SetVector3KeyValue(string variableName, Vector3 value)
    // {
    //     if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
    //     {
    //         Vector3Variable vector3Variable = (Vector3Variable) variable;
    //         vector3Variable.SetValue(value);
    //     }
    // }
    //
    // public Vector3 GetVector3KeyValue(string variableName)
    // {
    //     if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
    //     {
    //         Vector3Variable vector3Variable = (Vector3Variable) variable;
    //         return (Vector3) vector3Variable.GetValue();
    //     }
    //
    //     return new Vector3(0, 0, 0);
    // }
    //
    // #endregion
    //
    // #region GameObject
    //
    // public void SetGameObjectKeyValue(string variableName, GameObject value)
    // {
    //     if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
    //     {
    //         GameObjectVariable gameObjectVariable = (GameObjectVariable) variable;
    //         gameObjectVariable.SetValue(value);
    //     }
    // }
    //
    // public GameObject GetGameObjectKeyValue(string variableName)
    // {
    //     if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
    //     {
    //         GameObjectVariable gameObjectVariable = (GameObjectVariable) variable;
    //         return (GameObject) gameObjectVariable.GetValue();
    //     }
    //
    //     return null;
    // }
    //
    // #endregion
    //
    // #endregion
    
    #region Editor
    /// <summary>
    /// On Validate is only called in Editor. By performing checks here was can rest assured they will not be null.
    /// Usually what is in the Components region is in here.
    /// </summary>
    protected void OnValidate()
    {
        if (AssignedCharacter == null)
        {
            Debug.LogError("No Character assigned");
        }
    }

    #endregion
}
