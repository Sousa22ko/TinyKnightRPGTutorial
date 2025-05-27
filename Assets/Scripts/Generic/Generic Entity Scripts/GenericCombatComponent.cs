using UnityEngine;

public abstract class GenericCombatComponent : MonoBehaviour
{

    protected Entity entity;
    
    public Transform attackPoint;
    public LayerMask enemyLayer;

    public virtual float attackRange => 1f;
    public virtual float damage => 5f;
    public virtual float stunTime => 5f;
    public virtual float knockBackForce => 5f;
    public virtual float attackCooldown => 2f;
    public float attackCooldownTimer;

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
        }
    }

    // triggered in animation hit frame by Entity.invokeChangeHealth
    public void causeDamage()
    {
        Collider2D[] enemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayer);

        foreach (Collider2D enemy in enemies)
        {
            if (enemy is CapsuleCollider2D)
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