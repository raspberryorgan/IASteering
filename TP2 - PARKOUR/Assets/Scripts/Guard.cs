using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody))]
public class Guard : MonoBehaviour
{
    public Transform character;
    public float speed;
    public float shootDelay;
    public float analyzeTime;
    public List<Node> nodes;
    public float minNodeDistance = 1;
    public float avoidDistance = 1;
    public float avoidWeight = 1;
    public GameObject bulletPrefab;
    public Light viewLight;
    public float movSmoothness = 1;
    Seek seek;
    WaypointHandler wp;
    ObstacleAvoidance obstAvoid;
    Rigidbody rb;
    float bulletTimer;
    float analyzeTimer;
    bool analyzing;
    Action[] owotest;
    Vector3 dir;
    Roulette roulette;

    Dictionary<Action, int> _shootprob = new Dictionary<Action, int>();
    public void StartGuard(LayerMask layerMask)
    {
        roulette = new Roulette();
        _shootprob.Add(() => ShootingBullet(Vector3.zero), 60);
        _shootprob.Add(() => ShootingBullet(Vector3.right * 1), 35);
        _shootprob.Add(() => ShootingBullet(Vector3.right * 1.5f + Vector3.up * 3), 5);
        seek = new Seek(this.transform, character);
        obstAvoid = new ObstacleAvoidance(transform, character, avoidDistance, avoidWeight, layerMask);
        rb = GetComponent<Rigidbody>();

        wp = new WaypointHandler(nodes, minNodeDistance);
    }
    public void MoveSeekPlayer()
    {
        Debug.Log("seeeeeeek");
        Vector3 oldDir = Vector3.zero + dir;
        dir = Vector3.Slerp(oldDir, seek.GetDir() + obstAvoid.GetDir(), movSmoothness);
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
    public void Shoot()
    {
        viewLight.color = Color.red;
        roulette.Execute(_shootprob).Invoke();

    }
    public void ShootingBullet(Vector3 dir)
    {
        bulletTimer += Time.deltaTime;
        if (bulletTimer > shootDelay)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.transform.forward = (character.position + dir) - transform.position;
            bulletTimer = 0f;
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, avoidDistance);
    }
}