using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineofSight
{
    float range;
    float angle;
    LayerMask mask;
    Transform transform;
    //Implementar
    public LineofSight(Transform _transform, float _range, float _angle, LayerMask _mask)
    {
        transform = _transform;
        range = _range;
        angle = _angle;
        mask = _mask;
    }
    public bool IsInSight(Transform target)
    {
        Vector3 diff = (target.position - transform.position);
        //A--->B
        //B-A
        float distance = diff.magnitude;
        if (distance > range) return false;
        if (Vector3.Angle(transform.forward, diff) > angle / 2) return false;
        if (Physics.Raycast(transform.position, diff.normalized, distance, mask)) return false;
        return true;
    }

}
