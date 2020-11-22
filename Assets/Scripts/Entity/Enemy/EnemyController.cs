using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    Enemy _enemy;

    BezierCurve path;

    public Enemy enemy => _enemy;
    public int PathwayIndex { get; private set; }

    public static event Action<EnemyController> OnEnemyReachedCastle;
    public static event Action<EnemyController> OnEnemyDied;
    public event Action<EnemyController> OnParticularEnemyDied;

    Vector3 target;
    int targetIndex = 0;

    public void Init(int pathwayIndex, BezierCurve path)
    {
        this.PathwayIndex = pathwayIndex;
        this.path = path;

        transform.position = path.bezierCurvePoints[0];
        TargetNext();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, enemy.moveSpeed * Time.deltaTime);

        if(transform.position == target)
        {
            if(targetIndex != path.bezierCurvePoints.Length - 1)
            {
                TargetNext();
            }
            else
            {
                ReachedCastle();
            }
        }
    }

    private void TargetNext()
    {
        targetIndex++;
        target = path.bezierCurvePoints[targetIndex];
        transform.right = target - transform.position;
    }

    private void ReachedCastle()
    {
        OnEnemyReachedCastle?.Invoke(this);

        Destroy(gameObject);
    }

    public void Damage(float damage)
    {
        if(enemy.TakeDamage(damage))
        {
            OnEnemyDied?.Invoke(this);
            OnParticularEnemyDied?.Invoke(this);

            Destroy(gameObject);
        }
    }
}
