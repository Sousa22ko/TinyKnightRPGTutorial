using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovimentComponent : GenericMovimentComponent
{

    // public variables 
    public InputActionAsset InputActions;

    // private internal variables 
    private InputAction moveActions;

    //overrides
    [SerializeField] public float customSpeed = 8f;
    protected override float speed => customSpeed;

    [SerializeField] public bool customFacingRight = true;
    protected override bool facingRight
    {
        get => customFacingRight;
        set => customFacingRight = value;
    }

    // lifecycle 
    private void OnEnable() => InputActions.FindActionMap("Player").Enable();

    private void OnDisable() => InputActions.FindActionMap("Player").Disable();

    protected override void Awake()
    {
        base.Awake();

        if (InputActions == null)
            InputActions = Resources.Load<InputActionAsset>("InputSystem_Actions");

        moveActions = InputActions.FindActionMap("Player").FindAction("Move");
    }


    // scripts
    protected override void handleMoviment()
    {
        var input = moveActions.ReadValue<Vector2>().normalized;

        // andando
        if (input != Vector2.zero)
        {
            entity.rigidBody.linearVelocity = input * speed;
            base.handleFlipMovimentAnimation();
            entity.setCurrentState(States.Moving);
        }
        else if (entity.getCurrentState() == States.Moving) {
            entity.rigidBody.linearVelocity = Vector2.zero;
            entity.setCurrentState(States.Idle);
        }
    }

}
