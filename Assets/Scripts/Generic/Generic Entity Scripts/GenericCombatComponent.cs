using UnityEngine;

public abstract class GenericCombatComponent : MonoBehaviour
{

    protected Entity entity;
    
    protected Transform attackPoint;
    protected LayerMask enemyLayer;

    protected virtual float attackRange => 1f;
    protected virtual float damage => 5f;
    protected virtual float stunTime => 5f;
    protected virtual float knockBackForce => 5f;
    protected virtual float attackCooldown => 2f;
    protected float attackCooldownTimer;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
    }

    protected void Update()
    {
        if (attackCooldownTimer > 0) 
        {
            attackCooldownTimer -= Time.deltaTime;
        }
    }

    // scripts
    protected virtual void attack()
    {
        if (attackCooldownTimer <= 0)
        {
            entity.setCurrentState(States.Attacking);
            attackCooldownTimer = attackCooldown;

            Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

            foreach (Collider2D enemy in enemies)
            { 
                //enemy.
            }
        }
    }

}