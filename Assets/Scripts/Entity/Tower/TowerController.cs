using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerController : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    bool drawGizmos = true;
#endif
    [SerializeField]
    Tower _tower;
    [SerializeField]
    TMP_Text upgradePrompText;

    GameManager gameManager;

    bool upgradePrompt = false;

    List<EnemyController> targets = new List<EnemyController>();

    EnemyController target = null;

    public Tower tower => _tower;

    private void Start()
    {
        GetComponent<CircleCollider2D>().radius = tower.range;
    }

    public void Init(GameManager gameManager)
    {
        this.gameManager = gameManager;
    }

    private void EnemyDied(EnemyController _enemy)
    {
        if (_enemy == target)
        {
            target = GetNewTarget();
        }
    }

    public void TryUpgrade(bool upgrade)
    {
        if(upgrade)
        {
            if(upgradePrompText.gameObject.activeSelf)
            {
                UpgradeTower();
            }
            else
            {
                upgradePrompText.text = "Upgrade for " + tower.upgradeCost + " ?";
            }

            upgradePrompText.gameObject.SetActive(!upgradePrompText.gameObject.activeSelf);
        }
        else
        {
            upgradePrompText.gameObject.SetActive(false);
        }
    }

    public bool UpgradeTower()
    {
        bool upgradable = tower.Upgradable && gameManager.CanSpendMoney(tower.upgradeCost);

        if (upgradable)
            tower.Upgrade();

        return upgradable;
    }

    private EnemyController GetNewTarget()
    {
        EnemyController e = null;

        if(targets.Count > 0)
        {
            e = targets[0];
            targets.RemoveAt(0);
        }

        return e;
    }

    private void OnDrawGizmos()
    {
        if(drawGizmos)
        {
            Gizmos.DrawSphere(transform.position, tower.range);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 6)
        {
            EnemyController enemyController = col.GetComponent<EnemyController>();

            enemyController.OnParticularEnemyDied += EnemyDied;

            if (target == null)
                target = enemyController;
            else
                targets.Add(enemyController);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 6)
        {
            EnemyController enemyController = col.GetComponent<EnemyController>();

            enemyController.OnParticularEnemyDied -= EnemyDied;

            if (enemyController == target)
                target = GetNewTarget();
            else
                targets.Remove(enemyController);
        }
    }

    private IEnumerator TurnTowardsTarget()
    {
        while(target != null)
        {
            transform.right = target.transform.position - transform.position;
            yield return null;
        }
    }

    private IEnumerator Fire()
    {
        while(target != null)
        {
            target.Damage(tower.damage);
            yield return new WaitForSeconds(tower.fireRate);
        }
    }
}