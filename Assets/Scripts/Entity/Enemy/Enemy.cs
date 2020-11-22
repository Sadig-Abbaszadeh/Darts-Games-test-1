using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEnemy", menuName = "Entity/Enemy")]
public class Enemy : DamageTakers
{
    public int goldBonus, damage;
    public float moveSpeed;

    public override void Die()
    {
        base.Die();
    }
}