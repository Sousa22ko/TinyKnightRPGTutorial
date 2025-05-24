using UnityEngine;

public abstract class GenericCombatComponent : MonoBehaviour
{

    protected Entity entity;
    
    protected Transform attackPoint;
    protected LayerMask enemyLayer;

    protected virtual float attackRange => 0.5f;
    protected virtual float damage => 5f;
    protected virtual float stunTime => 5f;
    protected virtual float knockBackForce => 5f;
    protected virtual float attackCooldown => 2f;
    protected float attackCooldownTimer;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        attackPoint = transform.Find("AttackPoint");
    }

    protected void Update()
    {
        if (attackCooldownTimer > 0) 
        {
            attackCooldownTimer -= Time.deltaTime;
        }
        attack();
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
                enemy.GetComponent<GenericHealthComponent>().changeHealth(-damage);
            }
        }
    }

    // Debug
    private void OnDrawGizmos()
    {
        if (attackPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(attackPoint.position, attackRange);
        }
    }

}