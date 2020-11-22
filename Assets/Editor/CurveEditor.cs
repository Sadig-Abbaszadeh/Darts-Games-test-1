using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class CurveEditor : PositionHandlerEditor
{
    float debugCurveWidth;
    Color debugCurveColor;

    private void OnSceneGUI()
    {
        BezierCurve bezierCurve = target as BezierCurve;

        debugCurveWidth = bezierCurve.curveWidth;
        debugCurveColor = bezierCurve.curveColor;

        Vector3[] points = CreateCurveHandles(bezierCurve.Points, bezierCurve);

        Handles.DrawBezier(points[0], points[3], points[1], points[2], debugCurveColor, null, debugCurveWidth);
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        BezierCurve curve = target as BezierCurve;

        GUILayout.BeginHorizontal();

        if(GUILayout.Button("Draw the line"))
        {
            curve.DrawAndRemoveLineInWorld(true);
        }
        if(GUILayout.Button("Remove the line"))
        {
            curve.DrawAndRemoveLineInWorld(false);
        }

        GUILayout.EndHorizontal();
    }

    private Vector3[] CreateCurveHandles(Vector3[] points, BezierCurve curve)
    {
        Vector3 point;
        bool anyChangeOccured = false;

        for (int i = 0; i < points.Length; i++)
        {
            point = points[i];

            if (base.CreatePositionHandle(ref point))
            {
                anyChangeOccured = true;
                Undo.RecordObject(curve, "curve edited");
                points[i] = point;
            }
        }

        if (anyChangeOccured)
            curve.Points = points;

        return points;
    }
}
