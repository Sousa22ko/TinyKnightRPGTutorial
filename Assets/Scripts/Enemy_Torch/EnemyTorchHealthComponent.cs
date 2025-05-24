using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyTorchHealthComponent : GenericHealthComponent
{

    //overrides
    [SerializeField] public float customHealth = 50;
    protected override float maxHealth => customHealth;

}