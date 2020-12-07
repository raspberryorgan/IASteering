using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class WaypointHandler
{
    List<Node> nodes;
    float minDistance;
    int index;
    bool isBackwards = false;
    Action EndCallback;
    public WaypointHandler(List<Node> _n, float _nodeDistance)
    {
        nodes = _n;
        minDistance = _nodeDistance;
        index = 1;
    }
    public Vector3 WaypDirection(Vector3 position)
    {
        Vector3 dir = nodes[index].transform.position - position;
        if (dir.sqrMagnitude < minDistance)
        {
            if (index == 0 || index == nodes.Count - 1)
            {
                EndCallback();
            }
            Recalculate();
        }
        return dir.normalized;
    }
    public void SubscribeEndCallback(Action callback)
    {
        EndCallback = callback;
    }
    void Recalculate()
    {
        if (isBackwards == false)
        {
            index++;
            if (index >= nodes.Count - 1)
            {
                isBackwards = true;
            }

        }
        else
        {
            index--;
            if (index <= 0)
            {
                isBackwards = false;
            }
        }

    }

}
