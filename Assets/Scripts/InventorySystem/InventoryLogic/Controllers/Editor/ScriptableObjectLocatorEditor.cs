
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
    using UnityEditor;
#endif
using UnityEngine;

[CustomEditor(typeof(ScriptableObjectLocator))]
public class ScriptableObjectLocatorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        ScriptableObjectLocator script = (ScriptableObjectLocator)target;
        if (GUILayout.Button("Find InventoryItems"))
        {
            script.AddAllScriptableObjectsContainingUIDs<InventorySystem.InventoryItem>();
            Repaint();
        }

        if (GUILayout.Button("Find Ingredients"))
        {
            //was <Ingredient> previously
            script.AddAllScriptableObjectsContainingUIDs<ScriptableObject>();
            Repaint();
        }

        if (GUILayout.Button("Clear All Scriptable Objects"))
        {
            script.ClearAllScriptableObjects();
        }
    }
}