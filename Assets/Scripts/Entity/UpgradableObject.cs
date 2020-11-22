using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableObject : Entity
{
    public int upgradeCost, upgradeCostIncrease, maxUpgrades;

    [Tooltip("15 means 15% increase for all parameters for each level")]
    public float upgradePercent;

    public int level { get; protected set; } = 1;

    public virtual bool Upgrade() => level < maxUpgrades;
}
