using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityAtractor : MonoBehaviour
{

    public float gravity = -10f;

    public void Attract(Transform body)
    {
        Vector3 targetDir = (body.position - transform.position).normalized; //direction between the body and the center of the planet
        Vector3 bodyUp = body.up;

        body.rotation = Quaternion.FromToRotation(bodyUp, targetDir) * body.rotation;
        body.GetComponent<Rigidbody>().AddForce(targetDir * gravity); //Force to pull down
    }
}
