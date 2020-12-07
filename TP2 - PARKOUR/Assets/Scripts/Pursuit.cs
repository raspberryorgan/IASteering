using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pursuit : ISteering
{
    Transform _target;
    Transform _from;
    float _timePrediction;
    Rigidbody _rb;
    public Pursuit(Transform from, Transform target, Rigidbody rb, float timePrediction)
    {
        _rb = rb;
        _timePrediction = timePrediction;
        _target = target;
        _from = from;
    }
    public Vector3 GetDir()
    {
        Vector3 predictionPoint = _target.position + _target.forward * _rb.velocity.magnitude * _timePrediction;
        //A=From
        //B= PredictionPoint
        return (predictionPoint - _from.position).normalized;
    }
}
