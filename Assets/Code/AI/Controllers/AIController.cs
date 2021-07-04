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
    
    [SerializeField] private StateBehaviour currentState;
    public StateBehaviour CurrentState { get => currentState; set => currentState = value; }
    
    #endregion
    
    #region Monobehaviour

    // Start is called before the first frame update
    private void Start()
    {
        assignedCharacter.Controller = this;
        AssignedCharacter.VisionPerception.OnPerceptionUpdate.AddListener(OnPerceptionUpdate);
        
        currentState.Enter(this);
    }
    
    private void Update()
    {
        if (currentState != null)
        {
            currentState.StateUpdate();
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


    public void TransitionToState(StateBehaviour state)
    {
        if (currentState != null)
        {
            currentState.IsActiveState = false;
        }
        currentState = state;
        currentState.IsActiveState = true;
        currentState.Enter(this);
    }
    #endregion

    #region State

    private void StateComplete(StateBehaviour state)
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
