using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    [SerializeField]
    Transform startingPoint, endPoint;
    [SerializeField]
    Color sceneViewLineColor;

    public Vector3 StartingPoint => startingPoint == null ? Vector3.zero : startingPoint.position;
    public Vector3 EndPoint => endPoint == null ? Vector3.zero : endPoint.position;
    public Color SceneViewLineColor => sceneViewLineColor == null ? Color.white : sceneViewLineColor;
}
