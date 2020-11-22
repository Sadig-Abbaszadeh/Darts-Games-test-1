using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyTowers : MonoBehaviour
{
    [SerializeField]
    TowerPlace place;
    [SerializeField]
    GameObject towerPrefab;
    [SerializeField]
    TMP_Text costText;

    private void Start()
    {
        costText.text = towerPrefab.GetComponent<TowerController>().tower.purchaseCost + "";
    }

    private void OnMouseDown()
    {
        place.PurchaseTower(towerPrefab);
    }
}