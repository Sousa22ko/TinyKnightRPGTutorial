using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyTorchEntity : Entity
{

    protected override void Awake()
    {
        base.Awake();

        if (!TryGetComponent(out health))
            health = gameObject.AddComponent<EnemyTorchHealthComponent>();

        if (!TryGetComponent(out combat))
            combat = gameObject.AddComponent<EnemyTorchCombatComponent>();

        if (!TryGetComponent(out movement))
            movement = gameObject.AddComponent<EnemyTorchMovimentComponent>();

    }

}