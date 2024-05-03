using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanePhysics : MonoBehaviour
{
    private Rigidbody planeRigidBody;
    
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        planeRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        planeRigidBody.AddForce(transform.forward * speed); // ForceMode.VelocityChange()
        Debug.Log(planeRigidBody.velocity.ToString());
    }
}
