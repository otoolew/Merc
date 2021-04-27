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

    [SerializeField] private Vector3 currentDestination;
    public Vector3 CurrentDestination { get => currentDestination; set => currentDestination = value; }
    
    [SerializeField] private PlayMakerFSM playMaker;
    public PlayMakerFSM PlayMaker { get => playMaker; set => playMaker = value; }
    

    
    #endregion

    #region Values
    [Header("Values")]
    [SerializeField] private PlayerCharacter playerCharacter;
    public PlayerCharacter PlayerCharacter { get => playerCharacter; set => playerCharacter = value; }
    
    [SerializeField] private UnityDictionary<string, StateVariable> variableDictionary;
    public UnityDictionary<string, StateVariable> VariableDictionary { get => variableDictionary; set => variableDictionary = value; }
    
    [SerializeField] private UnityDictionary<string, StateNode> stateDictionary;
    public UnityDictionary<string, StateNode> StateDictionary { get => stateDictionary; set => stateDictionary = value; }
    
    [SerializeField] private List<StateNode> stateList;
    public List<StateNode> StateList { get => stateList; set => stateList = value; }
    #endregion

    #region Monobehaviour
    // Start is called before the first frame update
    private void Start()
    {
        playerCharacter = FindObjectOfType<PlayerCharacter>();
        assignedCharacter.Controller = this;
        AssignedCharacter.VisionPerception.OnPerceptionUpdate.AddListener(OnPerceptionUpdate);
        
        variableDictionary.Add("Destination", Vector3Variable.CreateVariable("Destination", new Vector3(0,0,0)));
        variableDictionary.Add("CoverPosition", Vector3Variable.CreateVariable("CoverPosition", new Vector3(0,0,0)));
        variableDictionary.Add("HasTarget", BoolVariable.CreateVariable("HasTarget", false));
        variableDictionary.Add("CurrentTarget", GameObjectVariable.CreateVariable("CurrentTarget", null));
        
        
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
                SetStateGameObject("CurrentTarget", character.gameObject);
            }
        }    
    }

    public void SetStateFloat(string variableName, float value)
    {
        if (variableDictionary.TryGetValue(variableName, out StateVariable variable))
        {
            FloatVariable floatVariable = (FloatVariable) variable;
            floatVariable.SetValue(value);
        }
        else
        {
            variableDictionary.Add(variableName, FloatVariable.CreateVariable(variableName, value));
        }
    }
    
    public void SetStateBool(string variableName, bool value)
    {
        if (variableDictionary.TryGetValue(variableName, out StateVariable variable))
        {
            BoolVariable boolVariable = (BoolVariable) variable;
            boolVariable.SetValue(value);
        }
        else
        {
            variableDictionary.Add(variableName, BoolVariable.CreateVariable(variableName, value));
        }
    }
    
    public void SetStateVector3(string variableName, Vector3 value)
    {
        if (variableDictionary.TryGetValue(variableName, out StateVariable variable))
        {
            Vector3Variable vector3Variable = (Vector3Variable) variable;
            vector3Variable.SetValue(value);
        }
        else
        {
            variableDictionary.Add(variableName, Vector3Variable.CreateVariable(variableName, value));
        }
    } 
    
    public void SetStateGameObject(string variableName, GameObject value)
    {
        if (variableDictionary.TryGetValue(variableName, out StateVariable variable))
        {
            GameObjectVariable gameObjectVariable = (GameObjectVariable) variable;
            gameObjectVariable.SetValue(value);
        }
        else
        {
            variableDictionary.Add(variableName, GameObjectVariable.CreateVariable(variableName, value));
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
