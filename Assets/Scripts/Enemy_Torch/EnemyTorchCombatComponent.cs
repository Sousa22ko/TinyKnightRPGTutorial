using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyTorchCombatComponent : GenericCombatComponent
{

    private GameObject target;

    //overrides
    [SerializeField] public float customAttackDamage = 2f;
    public override float damage => customAttackDamage;

    [SerializeField] public float customCooldown = 2f;
    public override float attackCooldown => customCooldown;


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
            if (distance <= attackRange)
            {
                entity.rigidBody.linearVelocity = Vector2.zero;
                base.attack();
            }
        }
    }
}