using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlace : MonoBehaviour
{
    [SerializeField]
    GameManager gameManager;
    [SerializeField]
    GameObject availableTowers;

    static TowerPlace selectedPlace = null;

    public TowerController tower;

    private void OnMouseDown()
    {
        if(selectedPlace != null)
        {
            selectedPlace.TouchOn(this);
        }

        TouchOn(this);
        selectedPlace = this;
    }

    public void PurchaseTower(GameObject towerPrefab)
    {
        TowerController tc = towerPrefab.GetComponent<TowerController>();

        if(gameManager.CanSpendMoney(tc.tower.purchaseCost))
        {
            Instantiate(towerPrefab, transform);
            tc.Init(gameManager);
        }

        TouchOn(null);
    }

    private void TouchOn(TowerPlace commandSender)
    {
        if(commandSender == this)
        {
            if(tower != null)
            {
                tower.TryUpgrade(true);
            }
            else
            {
                availableTowers.SetActive(true);
            }
        }
        else
        {
            if (tower != null)
                tower.TryUpgrade(false);

            availableTowers.SetActive(false);
        }
    }
}