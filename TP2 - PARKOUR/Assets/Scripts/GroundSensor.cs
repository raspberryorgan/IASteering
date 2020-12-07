using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public LayerMask layer;
    public Vector3 area = Vector3.one * 0.1f;
    List<Vector3> debugpoints = new List<Vector3>();
    Vector3 debugvector;
    Vector3 thatpointposition;
    void Update()
    {
        Debug.Log(CheckIsLayer() + "floor");
    }
    public bool CheckIsLayer()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, area / 2, Quaternion.identity, layer);
        if (hitColliders.Length != 0)
        {
            return true;
        }
        return false;
    }
    public Vector3 GetNormal()
    {
        debugpoints = new List<Vector3>();
        Vector3 normal = Vector3.zero;
        thatpointposition = transform.position;
        Collider[] hitColliders = Physics.OverlapBox(transform.position, area / 2, Quaternion.identity, layer);
        foreach (var h in hitColliders)
        {
            //Vector3 closestPoint = collider.ClosestPoint(location);
            Vector3 point = h.ClosestPoint(transform.position);
            normal += transform.position - point;
            debugpoints.Add(point);
        }
        return normal.normalized;

    }
    void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, area);
    }
}
