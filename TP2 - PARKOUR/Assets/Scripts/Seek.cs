using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : ISteering
{
    Transform _target;
    Transform _from;
    public Seek(Transform from, Transform target)
    {
        _target = target;
        _from = from;
    }
    public Vector3 GetDir()
    {
        //A---->B
        //B-A
        Vector3 dir = (_target.position - _from.position).normalized;
        return dir;
    }
}
