using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeParent : MonoBehaviour
{
    public void FindNeightboursInChildren()
    {
        Node[] nodes = GetComponentsInChildren<Node>();
        foreach (var _n in nodes)
        {
            _n.neighbours.Clear();
            Collider[] nodez = Physics.OverlapBox(_n.transform.position, _n.transform.localScale * 3, Quaternion.identity, _n.layer);
            Collider nodeCollider = _n.GetComponent<Collider>();
            foreach (Collider collNode in nodez)
            {
                Debug.Log("colideah2");
                if (collNode != nodeCollider)
                {
                    _n.neighbours.Add(collNode.GetComponent<Node>());

                }
            }
        }
    }
}
