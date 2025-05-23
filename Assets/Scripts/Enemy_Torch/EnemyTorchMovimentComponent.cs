using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyTorchMovimentComponent : GenericMovimentComponent
{

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
    protected override void Awake()
    {
        base.Awake();
    }


    // scripts
    protected override void handleMoviment()
    {

    }

}
