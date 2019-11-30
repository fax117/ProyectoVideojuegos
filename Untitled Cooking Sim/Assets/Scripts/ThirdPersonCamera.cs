using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonCamera : MonoBehaviour
{

    public float rotationSpeed = 5f;
    public Transform target, player;
    float mouseX, mouseY;
    Quaternion startRotPos;

    private void Start() {
        /*Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;*/

        startRotPos = target.rotation;
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void LateUpdate() {
        CamControl();
    }

    void CamControl(){
        mouseX += Input.GetAxisRaw("Mouse X") * rotationSpeed;
        mouseY -= Input.GetAxisRaw("Mouse Y") * rotationSpeed;

        float dot = Quaternion.Dot(transform.rotation, Quaternion.identity);
        Vector3 cross = Vector3.Cross(transform.position, player.position);
        //Debug.Log(cross);

        //transform.LookAt(target);
        //transform.rotation = Quaternion.LookRotation( cros, -transform.up );
        target.rotation = Quaternion.Euler(mouseY, mouseX, 0);

        /*if(Input.GetMouseButtonDown(2)){
            target.rotation = Quaternion.Slerp(target.rotation, startRotPos, 1f);
        }*/

        //player.rotation = Quaternion.Euler(0, mouseX, 0);
    }
}