using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
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