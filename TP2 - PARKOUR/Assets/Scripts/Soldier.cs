using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Soldier : MonoBehaviour
{
    public Transform character;
    public float speed;
    public float analyzeTime;
    public List<Node> nodes;
    public float minNodeDistance;
    public float avoidDistance = 1;
    public float avoidWeight = 1;
    public float movSmoothness = 1;
    public float timePrediction;
    public Light viewLight;
    Pursuit pursuit;
    WaypointHandler wp;
    ObstacleAvoidance obstAvoid;
    Rigidbody rb;
    float bulletTimer;
    float analyzeTimer;
    bool analyzing;

    Vector3 dir;
    public void StartGuard(LayerMask layerMask)
    {
        rb = GetComponent<Rigidbody>();
        pursuit = new Pursuit(this.transform, character, character.GetComponent<Rigidbody>(), timePrediction);
        obstAvoid = new ObstacleAvoidance(transform, character, avoidDistance, avoidWeight, layerMask);
        wp = new WaypointHandler(nodes, minNodeDistance);
    }
    public void MoveSeekPlayer()
    {
        Debug.Log("pursuuuuit");
        Vector3 oldDir = Vector3.zero + dir;
        dir = Vector3.Slerp(oldDir, pursuit.GetDir() + obstAvoid.GetDir(), movSmoothness);
        Debug.Log(dir);
        dir.y = 0f;
        rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
        transform.forward = Vector3.Slerp(transform.forward, dir, 0.5f);
        viewLight.color = Color.red;
    }
    public void MoveWaypointRoute()
    {
        //Ver de meter el seek aca 
        Vector3 oldDir = Vector3.zero + dir;
        dir = Vector3.Slerp(oldDir, wp.WaypDirection(transform.position) + obstAvoid.GetDir(), movSmoothness);
        dir.y = 0f;
        rb.MovePosition(transform.position + dir * speed * Time.deltaTime);
        transform.forward = Vector3.Slerp(transform.forward, dir, 0.5f);
        viewLight.color = Color.white;
    }
    public void StartAnalyze()
    {
        analyzing = true;
        analyzeTimer = 0f;
    }
    public void Analyze()
    {
        analyzeTimer += Time.deltaTime;
        if (analyzeTimer > analyzeTime)
        {
            analyzing = false;
        }
        //analyzebehaviour
    }
    public bool IsAnalyzing()
    {
        return analyzing;
    }


    public void SetPatrolCallback(Action callback)
    {
        wp.SubscribeEndCallback(callback);
    }
    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
        }
    }
}
