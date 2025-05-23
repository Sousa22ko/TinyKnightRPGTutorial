using System;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    protected Animator animator;
    public Rigidbody2D rigidBody;

    protected States currentState;

    protected GenericHealthComponent health;
    protected GenericCombatComponent combat;
    protected GenericMovimentComponent movement;

    protected virtual void Awake()
    {
        animator = GetComponent<Animator>();
        rigidBody = GetComponent<Rigidbody2D>();

        currentState = States.Idle;
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
            animator.SetBool($"is{state}", state == currentState);
        }
    }
}