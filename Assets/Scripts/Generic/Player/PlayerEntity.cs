using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEntity : Entity
{

    public States currentState = States.Idle;

    protected override void Awake()
    {
        if (!TryGetComponent(out health))
            health = gameObject.AddComponent<PlayerHealthComponent>();

        if (!TryGetComponent(out combat))
            combat = gameObject.AddComponent<PlayerCombatComponent>();

        if (!TryGetComponent(out movement))
            movement = gameObject.AddComponent<PlayerMovimentComponent>();

    }

}