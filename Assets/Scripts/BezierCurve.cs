using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour
{
    [SerializeField]
    Vector3[] points = new Vector3[] { new Vector3(-1, -1), new Vector3(-1, 1), new Vector3(1, 1), new Vector3(1, -1) };
    [SerializeField]
    Color sceneViewCurveColor = Color.white;
    [SerializeField]
    int smoothness = 10;

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

    public int Smoothness => smoothness;
    public Color CurveColor => sceneViewCurveColor;

    public Vector3 Bezier(float t)
    {
        t = Mathf.Clamp01(t);
        float b = 1 - t;

        // func of bezier
        return points[0] * b * b * b + points[1] * 3 * t * b * b + points[2] * 3 * t * t * b + points[3] * t * t * t;
    }
}
