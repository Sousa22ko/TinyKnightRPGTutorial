using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerHealthComponent : GenericHealthComponent
{

    //overrides
    [SerializeField] public float customHP = 100f;
    protected override float maxHealth => customHP;

    [SerializeField] public float customAutoheal = 0.5f;
    protected override float autoHeal => customAutoheal;
}