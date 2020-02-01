using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class AttackBehaviour : MonoBehaviour
{
    public int attackDamage = 1;
    public float attackSpeed = 0.3f;
    public Action onAttackHit;

    public abstract void Attack();
    public abstract void OnAttackHit();
}
