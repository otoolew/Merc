using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    #region Components
    [Header("Components")]
    [SerializeField] private AICharacter assignedCharacter;
    public AICharacter AssignedCharacter { get => assignedCharacter; set => assignedCharacter = value; }

    [SerializeField] private Animator animatorComp;
    public Animator AnimatorComp { get => animatorComp; set => animatorComp = value; }

    [SerializeField] private PlayMakerFSM playMaker;
    public PlayMakerFSM PlayMaker { get => playMaker; set => playMaker = value; }
    
    [Header("Available Behavior Nodes")]
    [SerializeField] private List<BehaviorTask> behaviorTasksList;
    public List<BehaviorTask> BehaviorTasksList { get => behaviorTasksList; set => behaviorTasksList = value; }
    
    [SerializeField] private UnityDictionary<string, KeyVariable> variableDictionary;
    public UnityDictionary<string, KeyVariable> VariableDictionary { get => variableDictionary; set => variableDictionary = value; }

    [SerializeField] private UnityDictionary<string, BehaviorTask> taskDictionary;
    public UnityDictionary<string, BehaviorTask> TaskDictionary { get => taskDictionary; set => taskDictionary = value; }
    
    #endregion

    #region Values
    [Header("Values")]
    [SerializeField] private PlayerCharacter playerCharacter;
    public PlayerCharacter PlayerCharacter { get => playerCharacter; set => playerCharacter = value; }
    

    #endregion

    #region Monobehaviour
    // Start is called before the first frame update
    private void Start()
    {
        playerCharacter = FindObjectOfType<PlayerCharacter>();
        assignedCharacter.Controller = this;
        AssignedCharacter.VisionPerception.OnPerceptionUpdate.AddListener(OnPerceptionUpdate);
        
        variableDictionary.Add("PrimaryTarget", KeyVariableFactory.CreateGameObjectVariable(new KeyVariableInfo(KeyVariableType.GAMEOBJECT,"PrimaryTarget" ), null));
    }

    private void Update()
    {
        if (assignedCharacter is null) return;

        if (assignedCharacter.VisionPerception.HasTarget)
        {
            LookAt(assignedCharacter.VisionPerception.CurrentTarget.transform.position);
            AssignedCharacter.AbilityController.CurrentAbility.Fire();
        }
    }
    
    #endregion

    #region Character Methods
    public bool PossessCharacter(AICharacter character)
    {
        assignedCharacter = character;
        assignedCharacter.Controller = this;
        return assignedCharacter;
    }

    public void OnPerceptionUpdate(Character character)
    {
        if (character)
        {
            if (character.IsValid())
            {
                SetGameObjectKeyValue("PrimaryTarget", character.gameObject);
            }
        }    
    }


    
    
    public void LookAt(Vector3 pos)
    {
        AssignedCharacter.RotationComp.RotateTo(pos);
    }
    
    public void MoveToFirePoint(Vector3 pos)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized * assignedCharacter.VisionPerception.Radius;
    }
    
    #endregion

    #region Key Variables
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

    public void SetFloatKeyValue(string variableName, float value)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            FloatVariable floatVariable = (FloatVariable) variable;
            floatVariable.SetValue(value);
        }
    }

    public float GetFloatKeyValue(string variableName)
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

    public void SetBoolKeyValue(string variableName, bool value)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            BoolVariable boolVariable = (BoolVariable) variable;
            boolVariable.SetValue(value);
        }
    }

    public bool GetBoolKeyValue(string variableName)
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

    public void SetVector3KeyValue(string variableName, Vector3 value)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            Vector3Variable vector3Variable = (Vector3Variable) variable;
            vector3Variable.SetValue(value);
        }
    }

    public Vector3 GetVector3KeyValue(string variableName)
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

    public void SetGameObjectKeyValue(string variableName, GameObject value)
    {
        if (variableDictionary.TryGetValue(variableName, out KeyVariable variable))
        {
            GameObjectVariable gameObjectVariable = (GameObjectVariable) variable;
            gameObjectVariable.SetValue(value);
        }
    }

    public GameObject GetGameObjectKeyValue(string variableName)
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
    

    #endregion
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
    private void Reset()
    {
        //Output the message to the Console
        Debug.Log("Reset");
        if (!playerCharacter)
            playerCharacter = FindObjectOfType<PlayerCharacter>();
    }
    
    #endregion
}
