using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundInteraction : MonoBehaviour
{

    public GameObject rocket;
    public Transform finalPosition;
    private Vector3 targetPosition;
    float speed;
    bool rocketMoving;

    public Transform spawner;
    public GameObject meteor;
    bool spawn;


    // Start is called before the first frame update
    void Start()
    {
        targetPosition = rocket.transform.position;
        speed = 6;
        rocketMoving = false;
        spawn = true;
        meteor.GetComponent<Rigidbody>().velocity = new Vector3(2000, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (spawn)
        {
            StartCoroutine(MeteorShower());
            spawn = false;
        }
        if (rocketMoving)
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
            rocketMoving = true;
            gameObject.GetComponent<AudioSource>().Play();
        }
        if (other.gameObject.CompareTag("DestroyRocket"))
        {
            Destroy(rocket);
        }
    }

    IEnumerator MeteorShower()
    {
        meteor.GetComponent<Rigidbody>().AddForce(transform.forward * 3000);
        var recentGO = Instantiate(meteor, spawner.transform.position, Quaternion.identity);
        recentGO.GetComponent<Rigidbody>().AddForce(transform.forward * 3000);
        yield return new WaitForSeconds(Random.Range(3, 5));
        StartCoroutine(DestroyGO());
        spawn = true;
    }

    IEnumerator DestroyGO()
    {
        yield return new WaitForSeconds(2);
        var go = GameObject.Find("Meteor(Clone)");
        Destroy(go);
    }

}
