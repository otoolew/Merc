using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    #region Components
    [Header("Components")]
    [SerializeField] private AICharacter assignedCharacter;
    public AICharacter AssignedCharacter { get => assignedCharacter; set => assignedCharacter = value; }

    [SerializeField] private LevelArea assignedLevelArea;
    public LevelArea AssignedLevelArea { get => assignedLevelArea; set => assignedLevelArea = value; }

    [SerializeField] private BehaviourTree behaviourTree;
    public BehaviourTree BehaviourTree { get => behaviourTree; set => behaviourTree = value; }
    
    
    #endregion
    
    #region Monobehaviour
    // Start is called before the first frame update
    private void Start()
    {
    
        behaviourTree = behaviourTree.Clone();
        behaviourTree.Bind(this);
        
        assignedCharacter.Controller = this;
        AssignedCharacter.VisionPerception.OnPerceptionUpdate.AddListener(OnPerceptionUpdate);
    }
    
    private void Update()
    {

    }

    #endregion

    #region Perception

    public void OnPerceptionUpdate(Character character)
    {
        if (character)
        {
            if (character.IsValid())
            {
                //Variables.SetGameObjectValue("CurrentTarget", character.gameObject);
            }
        }    
    }
    
    #endregion

    #region Behaviour

    private Context CreateBehaviourTreeContext() 
    {
        return Context.CreateFromGameObject(gameObject);
    }

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
