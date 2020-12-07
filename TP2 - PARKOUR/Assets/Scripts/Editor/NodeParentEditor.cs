using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(NodeParent))]
public class NodeParentEditor : Editor
{
    public override void OnInspectorGUI()
    {

        base.OnInspectorGUI();
        NodeParent _parent = (NodeParent)target;
        if (GUILayout.Button("Find Neigbours In Children"))
        {
            _parent.FindNeightboursInChildren();
        }
    }
}