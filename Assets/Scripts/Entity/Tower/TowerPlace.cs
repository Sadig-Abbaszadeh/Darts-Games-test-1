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

    [HideInInspector]
    public TowerController tower;

    private void OnMouseDown()
    {
        if (selectedPlace != null)
        {
            selectedPlace.TouchOn(this);
            selectedPlace = null;
        }
        else
        {
            TouchOn(this);
            selectedPlace = this;
        }
    }

    public void PurchaseTower(GameObject towerPrefab)
    {
        TowerController tc = towerPrefab.GetComponent<TowerController>();

        if(gameManager.CanSpendMoney(tc.tower.purchaseCost))
        {
            tower = Instantiate(towerPrefab, transform).GetComponent<TowerController>();
            tower.transform.localPosition = Vector3.zero;
            tower.Init(gameManager);
        }

        TouchOn(null);
        selectedPlace = null;
    }

    private void TouchOn(TowerPlace commandSender)
    {
        if(commandSender == this)
        {
            if(tower == null)
            {
                availableTowers.SetActive(!availableTowers.activeSelf);
            }
        }
        else
        {
            availableTowers.SetActive(false);
        }
    }
}