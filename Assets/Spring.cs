using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public Rigidbody RB;
    public float k;

 
    void Start()
    {
        Debug.Log(RB.mass);
    }
    void FixedUpdate()
    {
        RB.AddForce(-k * transform.position.x, 0, 0);
    }
}
