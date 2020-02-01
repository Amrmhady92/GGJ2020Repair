using System;
using System.Collections.Generic;
using UnityEngine;


public abstract class AttackBehaviour : MonoBehaviour
{
    public int attackDamage = 1;
    public float attackSpeed = 0.3f;
    public Action onAttackHit;

    public float attackCoolDown = 1;


    public abstract void Attack();
    public virtual void OnAttackHit()
    {
        if (onAttackHit != null) onAttackHit?.Invoke();
    } 
}
