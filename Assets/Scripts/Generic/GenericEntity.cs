using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected Animator animator;
    public Rigidbody2D rigidBody;

    protected States currentState;

    public GenericHealthComponent health;
    public GenericCombatComponent combat;
    public GenericMovimentComponent movement;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        currentState = States.Idle;
        ignoreBoundryOnSpawn();
    }

    public void setCurrentState(States newState)
    {
        if(newState == currentState) return;
        currentState = newState;

        updateAnimatorState();
    }

    public States getCurrentState() { return currentState; }

    public void updateAnimatorState()
    {
        foreach (States state in Enum.GetValues(typeof(States))) 
        {
            if (HasParameter($"{state}"))
            {
                animator.SetBool($"is{state}", state == currentState);
            }
        }
    }

    private bool HasParameter(string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == $"is{paramName}")
                return true;
        }
        return false;
    }

    public void invokeChangeHealth() 
    {
        combat.causeDamage();
    }

    // inicialmente desativa a colisão com boundrys
    private void ignoreBoundryOnSpawn()
    {
        Collider2D entityCollider = GetComponent<Collider2D>();
        if (entityCollider == null) return;

        GameObject boundryGO = GameObject.Find("BoundryCollider");
        if (boundryGO == null) return;

        Collider2D[] boundryColliders = boundryGO.GetComponentsInChildren<Collider2D>();

        foreach (Collider2D boundry in boundryColliders)
        {
            Physics2D.IgnoreCollision(entityCollider, boundry, true);
        }

    }
}

