using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attractor : MonoBehaviour
{
    public float G = 6674f;
    public Rigidbody rb;

    void Attract (Attractor objToAttract)
    {
        Rigidbody rbToAttract = objToAttract.rb;

        Vector3 direction = rb.position - rbToAttract.position;
        float distance = direction.magnitude;

        float forceMagnitude = G * (rb.mass * rbToAttract.mass) / Mathf.Pow(distance, 2);
        Vector3 force = direction.normalized * forceMagnitude;

        rbToAttract.AddForce(force);

    }

    private void FixedUpdate()
    {
        Attractor[] attractors = FindObjectsOfType<Attractor>();

        foreach (Attractor attractor in attractors)
        {
            if (attractor != this)
            {
                Attract(attractor);
            }

        }
    }

}
