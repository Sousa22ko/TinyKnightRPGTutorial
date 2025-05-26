using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyTorchMovimentComponent : GenericMovimentComponent
{

    public GameObject target;

    //overrides
    [SerializeField] public float customSpeed = 1.5f;
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
        entity.setCurrentState(States.Idle);
        entity.rigidBody.linearVelocity = Vector2.zero;

        target = GameObject.FindWithTag("Player");

    }


    // scripts
    protected override void handleMoviment()
    {

        if (entity.getCurrentState() == States.Chasing)
        {
            float targetDistance = Vector2.Distance(target.transform.position, transform.position);

            if (targetDistance <= entity.GetComponent<GenericCombatComponent>().attackRange)
            {
                entity.setCurrentState(States.Attacking); 
                entity.rigidBody.linearVelocity = Vector2.zero;
                return;
            }

            Vector2 targetDirection = (target.transform.position - transform.position).normalized;
            entity.rigidBody.linearVelocity = targetDirection * speed;

            handleFlipMovimentAnimation();
        }

        if (entity.getCurrentState() == States.Idle)
        {
            entity.rigidBody.linearVelocity = Vector2.zero;
        }

    }

    private void OnTriggerStay2D(Collider2D entityCollider)
    {
        if (entityCollider.gameObject.CompareTag("Player"))
        {
            entity.setCurrentState(States.Chasing);
        }
    }

    private void OnTriggerExit2D(Collider2D entityCollider)
    {
        if (entityCollider.gameObject.CompareTag("Player"))
        {
            entity.setCurrentState(States.Idle);
        }
    }

}
