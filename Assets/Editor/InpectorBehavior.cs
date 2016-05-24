using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(MinimizeInpector))]
public class InpectorBehavior : Editor
{
    MinimizeInpector Script;

    bool s = true;

    void OnEnable()
    {
        Script = target as MinimizeInpector;
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Collapse"))
        {
            Script.CollapseItems(s);
            s = !s;
        }
    }  
}
