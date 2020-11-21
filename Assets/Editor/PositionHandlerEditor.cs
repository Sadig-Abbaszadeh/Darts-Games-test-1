using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class PositionHandlerEditor : Editor
{
    public bool CreatePositionHandle(ref Vector3 position)
    {
        EditorGUI.BeginChangeCheck();

        position = Handles.PositionHandle(position, Quaternion.identity);

        return EditorGUI.EndChangeCheck();
    }
}
