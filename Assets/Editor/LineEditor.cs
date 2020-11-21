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

        Vector3 start = line.transform.TransformPoint(line.startingPoint);
        Vector3 end = line.transform.TransformPoint(line.endPoint);
        Handles.color = line.SceneViewLineColor;

        EditorGUI.BeginChangeCheck();

        start = Handles.PositionHandle(start, Quaternion.identity);
        end = Handles.PositionHandle(end, Quaternion.identity);

        if(EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(line, "Line points edited");

            line.startingPoint = line.transform.InverseTransformPoint(start);
            line.endPoint = line.transform.InverseTransformPoint(end);
        }

        Handles.DrawLine(start, end);
    }
}