using UnityEngine;
using System.Collections.Generic;
using UnityEditor;

[ExecuteInEditMode]
public class MinimizeInpector : MonoBehaviour
{
    public void CollapseItems(bool s)
    {
        Debug.Log(s);
        if (s == true)
        {
            EditorWindow.focusedWindow.SendEvent(Event.KeyboardEvent("left"));
        }
        if (s == false)
        {
            EditorWindow.focusedWindow.SendEvent(Event.KeyboardEvent("right"));
        }
    }
}
