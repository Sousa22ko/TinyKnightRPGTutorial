using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyTorchHealthComponent : GenericHealthComponent
{

    //overrides
    [SerializeField] public float customHealth = 50;
    protected override float maxHealth => customHealth;

    [SerializeField] public float customAutoHeal = 0.5f;
    protected override float autoHeal => customAutoHeal;

    [SerializeField] public float customCD = 0f;
    protected override float continuousDamage => customCD;

}