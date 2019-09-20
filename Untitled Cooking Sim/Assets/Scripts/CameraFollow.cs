using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public float xSpeed = 3.5f;
    private float sensitivity = 17f;

    float minX;
    float maxX;
    float minY;
    float maxY;

    float minFov = 35f;
    float maxFox = 100f;

    void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            transform.RotateAround(target.transform.position, transform.up, Input.GetAxis("Mouse X") * xSpeed );
            //transform.RotateAround(target.transform.position, transform.forward, Input.GetAxis("Mouse X") * xSpeed );
            transform.RotateAround(target.transform.position, transform.right, -Input.GetAxis("Mouse Y") * xSpeed );
        }

        //Zoom
        float fov = Camera.main.fieldOfView;
        fov += Input.mouseScrollDelta.y * -sensitivity;
        fov = Mathf.Clamp(fov, minFov, maxFox);
        Camera.main.fieldOfView = fov;
    }


    /*public Transform target;
    [Range(0.0f, 1.0f)]
    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    public float cameraSpeed = 1.0f;
    public float cameraRotationSpeed = 2.0f;

    //public bool LookAtPlayer = false;

    void Start()
    {
        offset = target.position - transform.position;
    }

    void LateUpdate()
    {
        Vector3 desiredPosition;
        Quaternion desiredRotation;

        desiredPosition = target.position;

        desiredRotation = Quaternion.LookRotation(target.forward, target.up);
        desiredPosition -= (transform.rotation * offset);

        transform.position += (desiredPosition - transform.position) * cameraSpeed * Time.deltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, desiredRotation, cameraRotationSpeed * Time.deltaTime);

        //var yAxis = target.rotation.y;

        //transform.Rotate(0f, yAxis * Time.deltaTime * cameraRotationSpeed, 0f);

        /*Vector3 desiredPosition = target.position - offset; 
        //Vector3 smoothedPosition = Vector3.Slerp(transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        transform.position = desiredPosition;*/


        //transform.rotation = Quaternion.LookRotation(target.forward, target.up);
        //transform.position -= (transform.rotation * offset);*/


}