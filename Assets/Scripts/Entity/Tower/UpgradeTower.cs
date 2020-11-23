using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    [SerializeField]
    TowerController tower;
    [SerializeField]
    TMP_Text upgradePromptText;

    static UpgradeTower activeTower;

    private void Start()
    {
        transform.SetParent(null);
    }

    private void OnMouseDown()
    {
        if (activeTower != null)
        {
            activeTower.TryUpgrade(this);
            activeTower = null;
        }
        else
        {
            TryUpgrade(this);
            activeTower = this;
        }
    }

    public void TryUpgrade(UpgradeTower tc)
    {
        if (tc == this)
        {
            if (upgradePromptText.gameObject.activeSelf)
            {
                Upgrade();
            }
            else
            {
                upgradePromptText.text = "Upgrade for " + tower.tower.upgradeCost + " ?";
            }

            upgradePromptText.gameObject.SetActive(!upgradePromptText.gameObject.activeSelf);
        }
        else
        {
            upgradePromptText.gameObject.SetActive(false);
        }
    }

    public bool Upgrade()
    {
        bool upgradable = tower.tower.Upgradable && tower.gameManager.CanSpendMoney(tower.tower.upgradeCost);

        if (upgradable)
            tower.tower.Upgrade();

        return upgradable;
    }
}