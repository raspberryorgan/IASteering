using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }
    public void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Gameplay");
        }
    }
}
