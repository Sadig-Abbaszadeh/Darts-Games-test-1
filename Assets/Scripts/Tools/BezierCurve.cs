using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [SerializeField]
    Vector3[] points = new Vector3[] { new Vector3(-1, -1), new Vector3(-1, 1), new Vector3(1, 1), new Vector3(1, -1) };
    [SerializeField]
    int smoothness = 10;

    public Vector3[] bezierCurvePoints { get; private set; }

#if UNITY_EDITOR
    public Color curveColor = Color.white;
    public float curveWidth = 1f;
    [SerializeField]
    bool drawGizmos = true;
#endif

    // transform between world and local space
    public Vector3[] Points
    {
        get {
            Vector3[] _p = new Vector3[points.Length];

            for(int i = 0; i < points.Length; i++)
            {
                _p[i] = transform.TransformPoint(points[i]);
            }

            return _p;
        }
        set {
            Vector3[] _p = new Vector3[value.Length];

            for(int i = 0; i < value.Length; i++)
            {
                _p[i] = transform.InverseTransformPoint(value[i]);
            }

            points = _p;
        }
    }

    private void Awake()
    {
        bezierCurvePoints = GetBezierPoints(Points);
    }

    private Vector3 BezierPoint(float t)
    {
        t = Mathf.Clamp01(t);
        float b = 1 - t;

        // func of bezier
        return points[0] * b * b * b + points[1] * 3 * t * b * b + points[2] * 3 * t * t * b + points[3] * t * t * t;
    }

    private void OnDrawGizmosSelected()
    {
        if (drawGizmos)
        {
            Vector3[] _points = Points;

            Gizmos.DrawLine(_points[0], _points[1]);
            Gizmos.DrawLine(_points[3], _points[2]);
        }
    }

    public Vector3[] GetBezierPoints(Vector3[] points)
    {
        Vector3[] bezierPoints = new Vector3[smoothness + 1];

        bezierPoints[0] = points[0];
        bezierPoints[bezierPoints.Length - 1] = points[points.Length - 1];

        for (int i = 1; i < smoothness; i++)
            bezierPoints[i] = BezierPoint((float)i / smoothness);

        return bezierPoints
    }
}