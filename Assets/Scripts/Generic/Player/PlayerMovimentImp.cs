using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovimentImp : GenericMovimentComponent
{

    // public variables 
    public InputActionAsset InputActions;

    // private internal variables 
    private InputAction moveActions;

    //overrides
    [SerializeField] public float customSpeed = 8f;
    protected override float speed => customSpeed;

    [SerializeField] public bool customFacingRight = true;
    protected override bool facingRight => customFacingRight;

    // lifecycle 
    private void OnEnable() => InputActions.FindActionMap("Player").Enable();

    private void OnDisable() => InputActions.FindActionMap("Player").Disable();

    private void Awake() => moveActions = InputActions.FindActionMap("Player").FindAction("Move");


    // scripts
    protected override void handleMoviment()
    {
        Rigidbody.linearVelocity = moveActions.ReadValue<Vector2>().normalized * speed;
    }

}
