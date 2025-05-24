using UnityEngine;

public abstract class GenericHealthComponent : MonoBehaviour
{

    protected Entity entity;

    protected float health = 10f;
    protected virtual float maxHealth => 10f;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();

        health = maxHealth;
    }

    public virtual void changeHealth(float ammount)
    {
        health += ammount;

        if (health <= 0)
            Destroy(gameObject);
    }

}