using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField]
    Vector3 startingPoint = Vector3.left, endPoint = Vector3.right;
    [SerializeField]
    Color sceneViewLineColor = Color.white;

    public Color SceneViewLineColor => sceneViewLineColor;

    public Vector3 StartingPoint
    {
        get {
            return transform.TransformPoint(startingPoint);
        }
        set {
            startingPoint = transform.InverseTransformPoint(value);
        }
    }

    public Vector3 EndPoint
    {
        get
        {
            return transform.TransformPoint(endPoint);
        }
        set
        {
            endPoint = transform.InverseTransformPoint(value);
        }
    }
}
