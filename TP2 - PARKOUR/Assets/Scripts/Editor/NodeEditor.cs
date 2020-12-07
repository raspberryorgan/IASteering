using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[CustomEditor(typeof(Node))]
public class NodeEditor : Editor
{
    public override void OnInspectorGUI(){
       
        base.OnInspectorGUI();
        Node _node = (Node)target;
        if(GUILayout.Button("Find Neigbours")){
            _node.neighbours.Clear();
            Debug.Log(_node.transform.localScale);
                Debug.Log("colideah1");
            Collider[] nodez = Physics.OverlapBox(_node.transform.position,_node.transform.localScale * 3, Quaternion.identity, _node.layer);
            Collider nodeCollider = _node.GetComponent<Collider>();
            Debug.Log(nodez.Length);
            foreach (Collider n in nodez){
                Debug.Log("colideah2");
                if(n != nodeCollider){
                _node.neighbours.Add(n.GetComponent<Node>());

                }
            }
    }
   
}
}