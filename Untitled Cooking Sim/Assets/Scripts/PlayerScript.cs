using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float xSpeed;
    private float zSpeed;

    // Update is called once per frame
    void Update()
    {
        xSpeed = Input.GetAxis("Horizontal");
        zSpeed = Input.GetAxisRaw("Vertical");
        gameObject.transform.Translate(new Vector3(xSpeed,0f,zSpeed));
    }
}
