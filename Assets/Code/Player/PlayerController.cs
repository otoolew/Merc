using System.Collections;
using System.Collections.Generic;
using Merc;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    #region Input
    
    [SerializeField] private PlayerControls inputActions;
    public PlayerControls InputActions { get => inputActions; set => inputActions = value; }
    #endregion

    #region Components
    [SerializeField] private PlayerCamera playerCamera;
    public PlayerCamera PlayerCamera { get => playerCamera; set => playerCamera = value; }

    [SerializeField] private PlayerUI playerUI;
    public PlayerUI PlayerUI { get => playerUI; set => playerUI = value; }
    #endregion

    [SerializeField] private bool mouseEnabled;
    public bool MouseEnabled { get => mouseEnabled; set => mouseEnabled = value; }

    [SerializeField] private PlayerCharacter playerCharacter;
    public PlayerCharacter PlayerCharacter { get => playerCharacter; set => playerCharacter = value; }

    [SerializeField] private Vector2 moveInput;
    public Vector2 MoveInput { get => moveInput; set => moveInput = value; }

    [SerializeField] private Vector2 lookInput;
    public Vector2 LookInput { get => lookInput; set => lookInput = value; }
    
    [SerializeField] private bool interactInput;
    public bool InteractInput { get => interactInput; set => interactInput = value; }
    
    #region Monobehaviour 
    private void Awake()
    {   
        inputActions = new PlayerControls();
    }

    private void OnEnable()
    {
        inputActions.UI.Pause.performed += PauseOnPerformed;
        inputActions.UI.Enable();
        
        inputActions.Character.Move.started += MoveOnPerformed;
        inputActions.Character.Move.performed += MoveOnPerformed;
        inputActions.Character.Move.canceled += MoveOnPerformed;
        
        inputActions.Character.Look.performed += LookOnPerformed;
        
        inputActions.Character.Interaction.started += InteractOnStarted;
        inputActions.Character.Interaction.performed += InteractOnPerformed;
        inputActions.Character.Interaction.canceled += InteractOnCanceled;
        
        inputActions.Character.Enable();
    }




    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    private void OnDisable()
    {
        ReleaseCharacter();
        if (inputActions != null)
        {
            inputActions.UI.Pause.performed -= PauseOnPerformed;
            
            inputActions.Character.Move.started -= MoveOnPerformed;
            inputActions.Character.Move.performed -= MoveOnPerformed;
            inputActions.Character.Move.canceled -= MoveOnPerformed;
            
            inputActions.Character.Look.performed -= LookOnPerformed;
            
            inputActions.Character.Interaction.performed -= InteractOnPerformed;
            inputActions.Character.Interaction.performed -= InteractOnPerformed;
            inputActions.Character.Interaction.canceled -= InteractOnCanceled;
            inputActions.Character.Disable();

        }
        
    }

    private void OnDestroy()
    {
        if (playerCharacter)
        {
            ReleaseCharacter();
        }
    }

    #endregion
    #region PlayerInput Calls
    private void PauseOnPerformed(InputAction.CallbackContext context)
    {
        if (playerUI)
        {
            if (context.performed)
            {
                if (GameManager.Instance.GameMode.CurrentGameState != GameState.PAUSED)
                {
                    PlayerUI.OpenPauseMenu();
                }
                else
                {
                    PlayerUI.ClosePauseMenu();
                }
            }
        }
    }
    private void MoveOnPerformed(InputAction.CallbackContext context)
    {
        if (context.started || context.performed)
        {
            moveInput = context.ReadValue<Vector2>();
        }
        else
        {
            moveInput = Vector2.zero;
        }
    }
    private void LookOnPerformed(InputAction.CallbackContext context)
    {
        lookInput = context.ReadValue<Vector2>();
        if (context.action.activeControl?.device.description.deviceClass == "Mouse")
        {
            MouseEnabled = true;
        }
        else
        {
            MouseEnabled = false;
        }
        
        
    }
    private void InteractOnStarted(InputAction.CallbackContext context)
    {
        interactInput = true;
        if (PlayerCharacter != null)
        {
            playerCharacter.onUseInteractable.Invoke();
        }
    }
    private void InteractOnPerformed(InputAction.CallbackContext context)
    {
        
        interactInput = true;
    }
    private void InteractOnCanceled(InputAction.CallbackContext context)
    {
        interactInput = false;
    }
    
    #endregion
    #region Character Methods
    public void PossessCharacter(PlayerCharacter character)
    {
        if (character)
        {
            // CharacterInput
            //character.PlayerController = this;
            playerCharacter = character;

            //character.GetComponent<PlayerMovement>().InputActions = inputActions;
            //character.GetComponent<PlayerMovement>().GamepadEnabled = gamepadEnabled;
  
            #region Character Input
            /*
            inputActions.Character.Enable();
            inputActions.Character.Look.started += playerCharacter.MovementComp.OnLook;
            inputActions.Character.Look.performed += playerCharacter.MovementComp.OnLook;
            inputActions.Character.Look.canceled += playerCharacter.MovementComp.OnLook;

            inputActions.Character.Move.started += playerCharacter.MovementComp.OnMove;
            inputActions.Character.Move.performed += playerCharacter.MovementComp.OnMove;
            inputActions.Character.Move.canceled += playerCharacter.MovementComp.OnMove;
            
            inputActions.Character.Left_PullTrigger.started += playerCharacter.OnLeftPullTrigger;
            inputActions.Character.Right_PullTrigger.performed += playerCharacter.OnRightPullTrigger;
            inputActions.Character.Left_PullTrigger.canceled += playerCharacter.OnLeftPullTrigger;
            inputActions.Character.Left_Reload.started += playerCharacter.OnLeftReload;

            inputActions.Character.Right_PullTrigger.started += playerCharacter.OnRightPullTrigger;
            inputActions.Character.Right_PullTrigger.performed += playerCharacter.OnRightPullTrigger;
            inputActions.Character.Right_PullTrigger.canceled += playerCharacter.OnRightPullTrigger;
            inputActions.Character.Right_Reload.started += playerCharacter.OnRightReload;

            inputActions.Character.UseInteraction.started += playerCharacter.OnUseInteraction;

            inputActions.Character.Dash.started += playerCharacter.OnDashPullTrigger;
            inputActions.Character.Dash.canceled += playerCharacter.OnDashPullTrigger;
            */
            
            #endregion
            playerCharacter.SetUpPlayerUI(PlayerUI);

            PlayerCamera.AssignFollowTarget(playerCharacter.transform);
        }
    }
    public void ReleaseCharacter()
    {
        if (playerCharacter)
        {
            /*inputActions.Character.Look.started -= playerCharacter.OnLook;
            inputActions.Character.Look.performed -= playerCharacter.MovementComp.OnLook;
            inputActions.Character.Look.canceled -= playerCharacter.MovementComp.OnLook;
            
            inputActions.Character.Move.started -= playerCharacter.MovementComp.OnMove;
            inputActions.Character.Move.performed -= playerCharacter.MovementComp.OnMove;
            inputActions.Character.Move.canceled -= playerCharacter.MovementComp.OnMove;
            
            inputActions.Character.Left_PullTrigger.started -= playerCharacter.OnLeftPullTrigger;
            inputActions.Character.Left_PullTrigger.canceled -= playerCharacter.OnLeftPullTrigger;
            inputActions.Character.Left_Reload.started -= playerCharacter.OnLeftReload;

            inputActions.Character.Right_PullTrigger.started -= playerCharacter.OnRightPullTrigger;
            inputActions.Character.Right_PullTrigger.canceled -= playerCharacter.OnRightPullTrigger;
            inputActions.Character.Right_Reload.started -= playerCharacter.OnRightReload;

            inputActions.Character.UseInteraction.started -= playerCharacter.OnUseInteraction;

            inputActions.Character.Dash.started -= playerCharacter.OnDashPullTrigger;
            inputActions.Character.Dash.canceled -= playerCharacter.OnDashPullTrigger;*/

            inputActions.Character.Disable();
        }
        playerCharacter = null;
    }

    public void EnableCharacterInput()
    {
        inputActions.Character.Enable();
    }
    public void DisableCharacterInput()
    {
        inputActions.Character.Disable();
    }

    private void OnMove()
    {
        
    }
    #endregion
}
