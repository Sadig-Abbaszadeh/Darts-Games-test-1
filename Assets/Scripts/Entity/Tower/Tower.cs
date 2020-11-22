using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "Entity/Tower")]
public class Tower : UpgradableObject
{
    public int purchaseCost;

    public float damage, fireRate, range;

    public void Upgrade()
    {
        level += 1;
        upgradeCost += upgradeCostIncrease;

        float a = upgradePercent / 100;

        damage *= (1 + a);
        fireRate *= (1 - a);
        health *= (1 + a);
    }
}