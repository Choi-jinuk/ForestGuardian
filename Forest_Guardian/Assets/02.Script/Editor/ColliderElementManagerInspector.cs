using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ColliderElementManager))]
public class ColliderElementManagerInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        ColliderElementManager manager = target as ColliderElementManager;

        if(GUILayout.Button("Copy Element"))
        {
            manager.CopyElement();
        }

        if (GUILayout.Button("Add Element"))
        {
            manager.AddElement();
        }
    }

}
