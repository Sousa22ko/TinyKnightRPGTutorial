using UnityEngine;

public abstract class Entity : MonoBehaviour
{

    protected GenericHealthComponent health;
    protected GenericCombatComponent combat;
    protected GenericMovimentComponent movement;

    protected virtual void Awake()
    {
        health = GetComponent<GenericHealthComponent>();
        combat = GetComponent<GenericCombatComponent>();
        movement = GetComponent<GenericMovimentComponent>();
    }
}