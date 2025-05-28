using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatComponent : GenericCombatComponent
{
    // public variables 
    public InputActionAsset InputActions;

    // private internal variables 
    private InputAction attackActions;

    //overrides
    [SerializeField] public float customCooldown = 0.5f;
    public override float attackCooldown => customCooldown;

    [SerializeField] public float customDamage = 50f;
    public override float damage => customDamage;
    

    // lifecycle 
    private void OnEnable() => InputActions.FindActionMap("Player").Enable();

    private void OnDisable() => InputActions.FindActionMap("Player").Disable();

    protected override void Awake()
    {
        base.Awake();
        enemyLayer = LayerMask.GetMask("Enemy");

        if (InputActions == null)
            InputActions = Resources.Load<InputActionAsset>("InputSystem_Actions");

        attackActions = InputActions.FindActionMap("Player").FindAction("Attack");
    }

    // scripts
    protected override void attack()
    {
        if (attackActions.IsPressed())
        {
            base.attack();
        }
    }  
    

}