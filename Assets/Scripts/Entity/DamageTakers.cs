using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageTakers :  Entity
{
    public virtual bool TakeDamage(float damage)
    {
        health -= damage;
        
        if(health <= 0)
        {
            Die();
            return true;
        }

        return false;
    }

    public virtual void Die()
    {
        
    }
}