using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance
{
    Transform _from;
    float _radius;
    LayerMask _mask;
    Transform _target;
    float _avoidWeight;
    public ObstacleAvoidance(Transform from, Transform target, float radius, float avoidWeight, LayerMask mask)
    {
        _avoidWeight = avoidWeight;
        _target = target;
        _radius = radius;
        _mask = mask;
        _from = from;
    }
    public Vector3 GetDir()
    {
        Vector3 dir = Vector3.zero;
        Collider[] obstacles = Physics.OverlapSphere(_from.position, _radius, _mask);
        if (obstacles.Length > 0)
        {
            Collider closer = null;
            float dist = 99999;
            foreach (var item in obstacles)
            {
                var currDistance = Vector3.Distance(item.transform.position, _from.position);
                if (currDistance < dist)
                {
                    dist = currDistance;
                    closer = item;
                }
            }
            //Vector3 dirFromObs = (_target.position - closer.transform.position).normalized * ((_radius - dist) / _radius) * _avoidWeight;
            Vector3 dirFromObs = (_from.transform.position - closer.ClosestPoint(_from.position)).normalized * _avoidWeight;
            dir += dirFromObs;
        }


        return dir.normalized;
    }
}
