using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LineDrawer))]
public class LineEditor : PositionHandlerEditor
{
    private void OnSceneGUI()
    {
        LineDrawer line = target as LineDrawer;

        Vector3 start = line.StartingPoint;
        Vector3 end = line.EndPoint;
        Handles.color = line.SceneViewLineColor;

        if (base.CreatePositionHandle(ref start) || base.CreatePositionHandle(ref end))
        {
            Undo.RecordObject(line, "start point change");
            line.StartingPoint = start;
            line.EndPoint = end;
        }

        Handles.DrawLine(start, end);
    }
}