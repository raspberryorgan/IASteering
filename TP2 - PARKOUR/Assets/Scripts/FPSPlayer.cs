using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : MonoBehaviour
{
    public float speed;
    public float powerJump;
    [SerializeField] float gravity;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    public void Movement(Vector3 dir)
    {
        dir.y = 0;
        Vector3 newPos = this.transform.position;
        rb.MovePosition(newPos + dir.normalized * speed * Time.deltaTime);

    }
    public void Jump(Vector3 dir)
    {
        rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        rb.AddForce((Vector3.up + dir).normalized * powerJump, ForceMode.Force);
        Debug.Log("jumpp");
    }
    public void Gravity()
    {
        rb.AddForce(Vector3.down * gravity, ForceMode.VelocityChange);
    }
    public void Rotate(float x)
    {
        Vector3 rotateCameraVector3 = transform.rotation.eulerAngles;

        rotateCameraVector3.y += x;

        //rb.MoveRotation(Quaternion.Euler(rotateCameraVector3));
    }
}
