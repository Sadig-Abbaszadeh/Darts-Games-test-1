using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CastleController : MonoBehaviour
{
    [SerializeField]
    Castle _castle;
    [SerializeField]
    TMP_Text healthText;
    [SerializeField]
    bool showIndividualHealth;
    //

    public Castle castle => _castle;

    private void Start()
    {
        healthText.gameObject.SetActive(showIndividualHealth);

        ShowHealth();
    }

    public float MaxHealth { get; private set; }

    public float Health => castle.health;

    public bool TakeDamage(float damage)
    {
        bool isCastleDead = castle.TakeDamage(damage);

        ShowHealth();

        return isCastleDead;
    }

    private void ShowHealth()
    {
        healthText.text = (int)castle.health + "";
    }
}