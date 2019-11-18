using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundInteraction : MonoBehaviour
{

    public GameObject rocket;
    public Transform finalPosition;
    public Vector3 targetPosition;
    float speed;
    bool rocketShip;
    // Start is called before the first frame update
    void Start()
    {
        targetPosition = rocket.transform.position;
        speed = 6;
        rocketShip = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (rocketShip)
        {
            float distance = Vector3.Distance(rocket.transform.position, targetPosition);
            rocket.transform.position = Vector3.Lerp(rocket.transform.position, targetPosition, (Time.deltaTime * speed) / distance);
            if (distance < 0.1f)
            {
                if (targetPosition == rocket.transform.position)
                {
                    targetPosition = finalPosition.position;
                }
            }
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && gameObject.CompareTag("Rocket"))
        {
            rocketShip = true;
            rocket.GetComponentInChildren<ParticleSystem>().Play();
        }
        if (other.gameObject.CompareTag("DestroyRocket"))
        {
            Destroy(rocket);
        }
    }
}
