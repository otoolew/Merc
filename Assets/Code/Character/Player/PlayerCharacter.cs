using System;
using System.Collections;
using System.Collections.Generic;
using Merc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerCharacter : Character
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; set => playerController = value; }

    [SerializeField] private float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }
    
    [SerializeField] private float lookSpeed;
    public float LookSpeed { get => lookSpeed; set => lookSpeed = value; }
    
    [SerializeField] private Quaternion targetRotation;
    public Quaternion TargetRotation { get => targetRotation; set => targetRotation = value; }
    
    [SerializeField] private Vector3 lookOffset;
    public Vector3 LookOffset { get => lookOffset; set => lookOffset = value; }
    
    [SerializeField] private LayerMask mouseLookLayer;
    public LayerMask MouseLookLayer { get => mouseLookLayer; set => mouseLookLayer = value; }
    #region Character

    [SerializeField] private Rigidbody rigidbodyComp;

    protected override Rigidbody RigidbodyComp
    {
        get => rigidbodyComp;
        set => rigidbodyComp = value;
    }

    [SerializeField] private Animator animatorComp;

    protected override Animator AnimatorComp
    {
        get => animatorComp;
        set => animatorComp = value;
    }

    [SerializeField] private CharacterController characterControllerComp;

    public CharacterController CharacterControllerComp
    {
        get => characterControllerComp;
        set => characterControllerComp = value;
    }

    [SerializeField] private HealthComponent healthComp;

    public override HealthComponent HealthComp
    {
        get => healthComp;
        set => healthComp = value;
    }

    #endregion

    #region Ability

    [Header("Ability Controllers")] [SerializeField]
    private WeaponAbilityController leftAbilityController;

    public WeaponAbilityController LeftAbilityController
    {
        get => leftAbilityController;
        set => leftAbilityController = value;
    }

    [SerializeField] private WeaponAbilityController rightAbilityController;

    public WeaponAbilityController RightAbilityController
    {
        get => rightAbilityController;
        set => rightAbilityController = value;
    }

    [SerializeField] private DashAbilityController dashAbilityController;

    public DashAbilityController DashAbilityController
    {
        get => dashAbilityController;
        set => dashAbilityController = value;
    }

    [Header("Ability Configs")] [SerializeField]
    private MeleeAbilityConfig meleeAbilityConfigConfig;

    public MeleeAbilityConfig MeleeAbilityConfigConfig
    {
        get => meleeAbilityConfigConfig;
        set => meleeAbilityConfigConfig = value;
    }

    [SerializeField] private RaycastAbilityConfig raycastAbilityConfig;

    public RaycastAbilityConfig RaycastAbilityConfig
    {
        get => raycastAbilityConfig;
        set => raycastAbilityConfig = value;
    }

    [SerializeField] private ProjectileAbilityConfig projectileAbilityConfig;

    public ProjectileAbilityConfig ProjectileAbilityConfig
    {
        get => projectileAbilityConfig;
        set => projectileAbilityConfig = value;
    }

    #endregion

    public UnityEvent onUseInteractable;

    #region Monobehaviour

    private void Awake()
    {
        onUseInteractable = new UnityEvent();
        SetUpAbility(LeftAbilityController, meleeAbilityConfigConfig);
        SetUpAbility(RightAbilityController, projectileAbilityConfig);
        SetUpAbility(DashAbilityController);
    }

    void Start()
    {
        HealthComp.Died.AddListener(character => OnDeath(this));
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter(PlayerController.MoveInput);
        RotateCharacter(PlayerController.LookInput);
    }

    private void OnDestroy()
    {
        HealthComp.Died.RemoveListener(character => OnDeath(this));
        onUseInteractable.RemoveAllListeners();
    }

    #endregion

    #region PlayerCharacter

    public void SetUpPlayerUI(PlayerUI playerUI)
    {
        //Debug.Log("Setup Player UI");
        playerUI.CharacterPanel.gameObject.SetActive(true);
        playerUI.CharacterPanel.AssignCharacterUIElements(this);
    }

    private void SetUpAbility(AbilityController abilityController, AbilityConfig abilityConfig)
    {
        abilityController.Owner = this;
        abilityController.EquipAbility(abilityConfig, gameObject.tag);
    }

    private void SetUpAbility(DashAbilityController abilityController)
    {
        abilityController.Owner = this;
    }


    #endregion

    #region Character

    
    public void MoveCharacter(Vector2 value)
    {
        CharacterControllerComp.Move(new Vector3(value.x, 0.0f, value.y) * Time.deltaTime * moveSpeed);
    }

    /*public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>().normalized;
    }
    public void OnLook(InputValue.InputContext value)
    {
        lookInput = value.Get<Vector2>().normalized;
    }*/
    public void RotateCharacter(Vector2 value)
    {
        if (PlayerController.MouseEnabled)
        {
            MouseLook();
            if (targetRotation.eulerAngles != Vector3.zero)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, lookSpeed * Time.deltaTime);
            }
        }
        else
        {
            Vector3 direction = (Vector3.right * value.x) + (Vector3.forward * value.y);
            if (direction != Vector3.zero)
            {
                targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 180 * Time.deltaTime);
            }
        }


    }
    public void MouseLook()
    {
        if (MouseToWorldPoint(Mouse.current.position.ReadValue(), out Vector3 mousePoint))
        {
            Vector3 playerToMouse = mousePoint - transform.position;

            Quaternion lookRotation = Quaternion.LookRotation(playerToMouse);

            if (lookRotation.eulerAngles != Vector3.zero) // It already shouldn't be...
            {
                lookRotation.x = 0f;
                lookRotation.z = 0f;
                lookRotation.eulerAngles += lookOffset;
                targetRotation = lookRotation;
            }
        }
    }

    private bool MouseToWorldPoint(Vector2 mouseScreen, out Vector3 mousePoint)
    {
        Ray ray = Camera.main.ScreenPointToRay(mouseScreen);
        //ray.origin += offset;
        if (Physics.Raycast(ray, out RaycastHit rayHit, 500.0f, mouseLookLayer)) // 1000 can be better
        {
            mousePoint = rayHit.point;
            return true;
        }
        mousePoint = Vector3.zero;
        return false;
    }
    #endregion
    public override bool IsDead()
    {
        return HealthComp.IsDead;
    }

    public override void OnDeath(Character character)
    {
        Debug.LogWarning("TODO: Implement PLAYER Character Death...");
    }
    
    
    public override bool IsValid()
    {
        if (healthComp.IsDead)
        {
            return false;
        }
        return true;
    }

    
}
