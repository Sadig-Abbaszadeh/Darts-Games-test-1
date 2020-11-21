using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LineDrawer))]
public class LineEditor : Editor
{
    private void OnSceneGUI()
    {
        LineDrawer line = target as LineDrawer;

        Vector3 start = line.StartingPoint;
        Vector3 end = line.EndPoint;
        Handles.color = line.SceneViewLineColor;

        Handles.DrawLine(start, end);
    }
}