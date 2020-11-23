using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class TowerController : MonoBehaviour
{
#if UNITY_EDITOR
    [SerializeField]
    bool drawGizmos = true;
#endif
    [SerializeField]
    Tower _tower;

    public GameManager gameManager { get; private set; }

    bool upgradePrompt = false;

    public List<EnemyController> targets = new List<EnemyController>();

    static TowerController activeTower;

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
        if(col.gameObject.layer == 8)
        {
            EnemyController enemyController = col.GetComponent<EnemyController>();

            enemyController.OnParticularEnemyDied += EnemyDied;

            if (target == null)
            {
                target = enemyController;
                StartCoroutine(TurnTowardsTarget());
                StartCoroutine(Fire());
            }
            else
                targets.Add(enemyController);
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == 8)
        {
            EnemyController enemyController = col.GetComponent<EnemyController>();

            enemyController.OnParticularEnemyDied -= EnemyDied;

            if (enemyController == target)
            {
                Debug.Log("this target left");
                target = GetNewTarget();
            }
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