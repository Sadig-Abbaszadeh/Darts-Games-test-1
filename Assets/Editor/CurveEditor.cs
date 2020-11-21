using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BezierCurve))]
public class CurveEditor : PositionHandlerEditor
{
    int smoothness;

    private void OnSceneGUI()
    {
        BezierCurve bezierCurve = target as BezierCurve;

        smoothness = bezierCurve.Smoothness;
        Handles.color = bezierCurve.CurveColor;

        Vector3[] points = CreateCurveHandles(bezierCurve.Points, bezierCurve);

        DrawCurve(points, bezierCurve);        
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

    private void DrawCurve(Vector3[] points, BezierCurve curve)
    {
        Vector3 currentPoint = points[0], nextPoint;

        for(int i = 1; i < smoothness; i++)
        {
            nextPoint = curve.Bezier((float)i / smoothness);
            Handles.DrawLine(currentPoint, nextPoint);
            currentPoint = nextPoint;
        }
    }
}
