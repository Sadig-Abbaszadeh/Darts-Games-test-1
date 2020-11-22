using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Enemy : DamageTakers
{
    public int goldBonus;
    public float moveSpeed, damage;

    public override void Die()
    {
        base.Die();
    }
}