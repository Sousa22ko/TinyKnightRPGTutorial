using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyTorchCombatComponent : GenericCombatComponent
{

    private GameObject target;


    protected override void Awake()
    {
        base.Awake();
        enemyLayer = LayerMask.GetMask("Player");
        target = GameObject.FindWithTag("Player");
    }


    protected override void attack()
    {
        if (attackCooldownTimer <= 0 && target != null)
        {
            float distance = Vector2.Distance(transform.position, target.transform.position);
            if (distance <= attackRange + 0.1f) // 0.1f de margem
            {
                entity.setCurrentState(States.Attacking);
                attackCooldownTimer = attackCooldown;

                entity.rigidBody.linearVelocity = Vector2.zero;
            }
        }
    }
}