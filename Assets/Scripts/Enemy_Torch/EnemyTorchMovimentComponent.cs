using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyTorchMovimentComponent : GenericMovimentComponent
{

    public GameObject target;

    //overrides
    [SerializeField] public float customSpeed = 1.5f;
    protected override float speed => customSpeed;

    public bool enableAgroRange = false;


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
            if (targetDistance <= entity.combat.attackRange)
            {
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

    private void NonAgro()
    {
        if (entity.getCurrentState() == States.Attacking) return;

        float distance = Vector2.Distance(entity.combat.attackPoint.position, target.transform.position);

        if (entity.getCurrentState() == States.Idle && distance <= entity.combat.attackRange) return;

        entity.setCurrentState(States.Chasing);
    }

    private void OnTriggerStay2D(Collider2D entityCollider)
    {
        if (!enableAgroRange) 
        {
            NonAgro();
            return;
        }
        if (!entityCollider.gameObject.CompareTag("Player")) return;
        if (entity.getCurrentState() == States.Attacking) return;

        float distance = Vector2.Distance(entity.combat.attackPoint.position, entityCollider.transform.position);

        if (entity.getCurrentState() == States.Idle && distance <= entity.combat.attackRange) return;

        entity.setCurrentState(States.Chasing);
    }

    private void OnTriggerExit2D(Collider2D entityCollider)
    {
        if (entityCollider.gameObject.CompareTag("Player"))
        {
            entity.setCurrentState(States.Idle);
        }
    }

}
