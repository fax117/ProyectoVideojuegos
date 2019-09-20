using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour
{

    GravityAtractor planet;
    Rigidbody rigidbody;

    void Awake()
    {
        planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAtractor>();
        rigidbody = GetComponent<Rigidbody>();

        rigidbody.useGravity = false;
        //rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        planet.Attract(transform);
    }
}
