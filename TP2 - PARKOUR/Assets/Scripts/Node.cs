using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Node : MonoBehaviour
{
    public List<Node> neighbours = new List<Node>();
    public LayerMask layer;
    public void OnDrawGizmos()
    {
        bool selected = UnityEditor.Selection.Contains(this.gameObject);
        if (selected == true)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(transform.position, transform.localScale * 5);
        }
        Gizmos.DrawSphere(transform.position, 0.25f);
        foreach (Node n in neighbours)
        {
            Gizmos.DrawLine(transform.position, n.transform.position);
        }
    }
}

