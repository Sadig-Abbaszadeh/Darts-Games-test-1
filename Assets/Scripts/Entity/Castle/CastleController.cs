using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleController : MonoBehaviour
{
    [SerializeField]
    Castle _castle;
    [SerializeField]
    HealthBarController healthBar;

    public Castle castle => _castle;

    public float MaxHealth { get; private set; }

    public float Health => castle.health;

    public bool TakeDamage(float damage)
    {
        bool isCastleDead = castle.TakeDamage(damage);

        healthBar.UpdateHealth();

        return isCastleDead;
    }
}