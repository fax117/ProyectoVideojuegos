using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlaceholder : MonoBehaviour
{
    public GameObject player;
    public GameObject planet;

    // Update is called once per frame
    void Update()
    {
        //S M O O T H N E S S  :*

        //Postion
        transform.position = Vector3.Lerp(transform.position, player.transform.position, 0.1f);

        Vector3 gravDirection = (transform.position - planet.transform.position).normalized;
        
        //Rotation
        Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravDirection) * transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, 0.5f);
    }

    public void NewPlanet(GameObject newPlanet){
        planet = newPlanet;
    }
}
