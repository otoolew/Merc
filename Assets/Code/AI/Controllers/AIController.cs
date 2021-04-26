
using System;
using UnityEngine;
using HutongGames.PlayMaker;
using Random = UnityEngine.Random;

public class AIController : MonoBehaviour
{
    [DisableFloat]
    public float someFloat;

    #region Components
    [Header("Components")]
    [SerializeField] private AICharacter assignedCharacter;
    public AICharacter AssignedCharacter { get => assignedCharacter; set => assignedCharacter = value; }

    [SerializeField] private Animator animatorComp;
    public Animator AnimatorComp { get => animatorComp; set => animatorComp = value; }
    
    [SerializeField] private StateStackController stateStackController;
    public StateStackController StateStackController { get => stateStackController; set => stateStackController = value; }
    
    [SerializeField] private Vector3 currentDestination;
    public Vector3 CurrentDestination { get => currentDestination; set => currentDestination = value; }
    
    [SerializeField] private PlayMakerFSM playMaker;
    public PlayMakerFSM PlayMaker { get => playMaker; set => playMaker = value; }

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
                Debug.Log(gameObject.name + "'s Perception Update " + character.name);
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
