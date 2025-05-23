using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerEntity : Entity
{

    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent(out health))
            health = gameObject.AddComponent<PlayerHealthComponent>();

        if (!TryGetComponent(out combat))
            combat = gameObject.AddComponent<PlayerCombatComponent>();

        if (!TryGetComponent(out movement))
            movement = gameObject.AddComponent<PlayerMovimentComponent>();

    }

}