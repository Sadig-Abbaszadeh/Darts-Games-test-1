using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineDrawer : MonoBehaviour
{
    public Vector3 startingPoint = Vector3.left, endPoint = Vector3.right;

    [SerializeField]
    Color sceneViewLineColr = Color.white;

    public Color SceneViewLineColor => sceneViewLineColr;
}
