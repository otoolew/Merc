using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    
    [SerializeField] private Variables variables;
    public Variables Variables { get => variables; set => variables = value; }
    
    [SerializeField] private StateComponent currentState;
    public StateComponent CurrentState { get => currentState; set => currentState = value; }
    
    [SerializeField] private StateBehaviour currentStateBehaviour;
    public StateBehaviour CurrentStateBehaviour { get => currentStateBehaviour; set => currentStateBehaviour = value; }
    
    [SerializeField] private IdleStateBehaviour idleStateBehaviour;
    public IdleStateBehaviour IdleStateBehaviour { get => idleStateBehaviour; set => idleStateBehaviour = value; }
    

    private Stack<StateBehaviour> stateStack = new Stack<StateBehaviour>();
    public Stack<StateBehaviour> StateStack { get => stateStack; set => stateStack = value; }
    
    [SerializeReference] private List<IVariable> stateList = new List<IVariable>();
    public List<IVariable> StateList { get => stateList; set => stateList = value; }
    
    #endregion
    
    #region Monobehaviour

    // Start is called before the first frame update
    private void Start()
    {
        PushState(currentState);
        assignedCharacter.Controller = this;
        AssignedCharacter.VisionPerception.OnPerceptionUpdate.AddListener(OnPerceptionUpdate);
        StateList.Add(new BoolVariable("SomeBull", true));
        StateList.Add(new FloatVariable("SomeFloat", 0.5f));
        //currentState.EnterState(this);
    }
    
    private void Update()
    {
        if (currentState != null)
        {
            //currentState.UpdateState(this);
        }
    }

    #endregion

    #region Character Methods

    public void OnPerceptionUpdate(Character character)
    {
        if (character)
        {
            if (character.IsValid())
            {
                Variables.SetGameObjectValue("CurrentTarget", character.gameObject);
            }
        }    
    }
    
    public void MoveToFirePoint(Vector3 pos)
    {
        Vector2 randomPoint = Random.insideUnitCircle.normalized * assignedCharacter.VisionPerception.Radius;
    }
        
    // public void PushState(StateBehaviour state)
    // {
    //     state.OnOrdersComplete.AddListener(OnStateOrdersComplete);
    //     StateStack.Push(state);
    //     //currentState = StateStack.Peek();
    // }

    public void OnStateOrdersComplete(StateBehaviour state, StateBehaviour nextState)
    {
        Debug.Log(state.StateName + " Complete");
        state.OnOrdersComplete.RemoveAllListeners();
        
        if (StateStack.Count > 0)
        {
            if (StateStack.Peek() == state)
            {
                StateStack.Pop();
                //PushState(nextState);
            }
        }
        else
        {
            //PushState(nextState);
        }
    }
    public void TransitionToState(StateComponent state)
    {
        if (currentState != null)
        {
            //currentState.ExitState(this);
        }
        currentState = state;
        //currentState.EnterState(this);
    }
    // public void TransitionToState(StateBehaviour state)
    // {
    //     if (currentState != null)
    //     {
    //         currentState.IsActiveState = false;
    //     }
    //     currentState = state;
    //     currentState.IsActiveState = true;
    //     currentState.Enter(this);
    // }
    #endregion

    #region State

    public void PushState(StateComponent stateComp)
    {
        
    }
    private void CompleteState(StateBehaviour state)
    {
        
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

    #endregion
}
