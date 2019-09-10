using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float xSpeed;
    private float zSpeed;
    public float speed;
    private float sprintSpeed;

    private void Start()
    {
        sprintSpeed = 7f;
    }

    // Update is called once per frame
    void Update()
    {
        xSpeed = Input.GetAxisRaw("Horizontal");
        zSpeed = Input.GetAxisRaw("Vertical");
        Sprint();
        gameObject.transform.Translate(new Vector3(xSpeed * Time.deltaTime * speed, 0f, zSpeed * Time.deltaTime * speed));
    }

    private void Sprint()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed += sprintSpeed; 
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed -= sprintSpeed;
        }
    }
}
