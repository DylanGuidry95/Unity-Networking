using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[CustomEditor(typeof(MinimizeInpector))]
class InpectorBehavior : Editor
{
    List<Component> AllTheComponents = new List<Component>();
    List<bool> States = new List<bool>();
    bool GetInfo = true;

    MinimizeInpector Script;

    void OnEnable()
    {
        Script = (MinimizeInpector)target;

        foreach (Component c in Script.GetComponents<Component>())
        {
            if (c.GetType() != typeof(MinimizeInpector) && !AllTheComponents.Contains(c))
            {
                AllTheComponents.Add(c);
                States.Add(false);
            }
        }


    }


    public override void OnInspectorGUI()
    {
        for (int i = 0; i < States.Count; i++)
        {
            States[i] = GUILayout.Toggle(States[i], AllTheComponents[i].GetType().ToString());
        }

        foreach (bool b in States)
        {
            if (b == true)
                AllTheComponents[States.IndexOf(b)].hideFlags = HideFlags.HideInInspector;
            if (b == false)
                AllTheComponents[States.IndexOf(b)].hideFlags = HideFlags.None;
        }
    }
}
