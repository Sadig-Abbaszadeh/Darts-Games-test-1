using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyTowers : MonoBehaviour
{
    [SerializeField]
    TowerPlace place;
    [SerializeField]
    GameObject towerPrefab;

    private void OnMouseDown()
    {
        place.PurchaseTower(towerPrefab);
    }
}