using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Collectable : MonoBehaviour
{
    //La idea es que cuando inicie todos los coll esten bloqueados y cada vez 
    //que agarran uno, se desbloquea el siguiente
    public MeshRenderer baseMesh;
    public MeshRenderer topMesh;
    public GameObject effects;
    bool isLocked;
    public bool isFinal;
    Action onGrab;
    Action onEnd;
    public void Unlock()
    {
        Debug.Log("unlock");
        baseMesh.material.color = Color.green;
        topMesh.material.color = new Color(0, 1, 0, 0.2f);
        effects.SetActive(true);
        isLocked = false;

    }
    public void Lock()
    {
        Debug.Log("lock");
        isLocked = true;
        baseMesh.material.color = Color.white;
        topMesh.material.color = new Color(1, 1, 1, 0.2f);
    }
    public void SubscribeGrab(Action _a)
    {
        onGrab = _a;
    }
    public void SubscribeEnd(Action _a)
    {
        onEnd = _a;
    }
    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player") && !isLocked)
        {
            if (isFinal)
            {
                onEnd();
            }
            else
            {
                onGrab();

            }
            Destroy(this.gameObject);
        }
    }

}
