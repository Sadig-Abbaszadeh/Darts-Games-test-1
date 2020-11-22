using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewTower", menuName = "Entity/Tower")]
public class Tower : UpgradableObject
{
    public float damage, fireRate;

    public override bool Upgrade()
    {
        bool upgraded = base.Upgrade();
        if(upgraded)
        {
            level += 1;
            upgradeCost += upgradeCostIncrease;

            float a = upgradePercent / 100;

            damage *= a;
            fireRate *= a;
            health *= a;
        }

        return upgraded;
    }
}