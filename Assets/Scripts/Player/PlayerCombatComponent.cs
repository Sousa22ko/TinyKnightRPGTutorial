using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombatComponent : GenericCombatComponent
{
    // public variables 
    public InputActionAsset InputActions;

    // private internal variables 
    private InputAction moveActions;

    //overrides


    // lifecycle 
    private void OnEnable() => InputActions.FindActionMap("Player").Enable();

    private void OnDisable() => InputActions.FindActionMap("Player").Disable();

    protected override void Awake()
    {
        base.Awake();

        if (InputActions == null)
            InputActions = Resources.Load<InputActionAsset>("InputSystem_Actions");

        moveActions = InputActions.FindActionMap("Player").FindAction("Attack");
    }


    // scripts
   

}