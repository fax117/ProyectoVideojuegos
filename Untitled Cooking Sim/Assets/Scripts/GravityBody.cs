using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent (typeof (Rigidbody))]
public class GravityBody : MonoBehaviour
{

    public GravityAtractor planet;
    Rigidbody planetRigidbody;

    void Awake()
    {
        //planet = GameObject.FindGameObjectWithTag("Planet").GetComponent<GravityAtractor>();
        planetRigidbody = GetComponent<Rigidbody>();

        planetRigidbody.useGravity = false;
        //rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void FixedUpdate()
    {
        planet.Attract(transform);
    }
}
