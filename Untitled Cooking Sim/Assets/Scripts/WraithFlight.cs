using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WraithFlight : MonoBehaviour
{
    public GameObject ship;
    float speedShip;
    private Vector3 targetPositionShip;
    public Transform firstShipTarget;
    public Transform SecondShipTarget;
    public Transform ThirdShipTarget;
    public Transform LastShipTarget;
    public Transform from;
    public Transform to;
    public Transform fromTwo;
    public Transform finalTo;

    // Start is called before the first frame update
    void Start()
    {
        targetPositionShip = firstShipTarget.position;
        speedShip = 20;
    }

    // Update is called once per frame
    void Update()
    {

        float distanceShip = Vector3.Distance(ship.transform.position, targetPositionShip);
        ship.transform.position = Vector3.Lerp(ship.transform.position, targetPositionShip, (Time.deltaTime * speedShip) / distanceShip);
        if (distanceShip < 0.1f)
        {
            if (targetPositionShip == firstShipTarget.position)
            {
                targetPositionShip = SecondShipTarget.position;
            }
            else if (targetPositionShip == SecondShipTarget.position)
            {
                targetPositionShip = ThirdShipTarget.position;
            }
            else if (targetPositionShip == ThirdShipTarget.position)
            {
                targetPositionShip = LastShipTarget.position;
            }
            else if (targetPositionShip == LastShipTarget.position)
            {
                targetPositionShip = firstShipTarget.position;
            }
        }

        Debug.Log(targetPositionShip);
        Debug.Log(ship.transform.rotation);
        if (targetPositionShip == SecondShipTarget.position)
        {
            ship.transform.rotation = Quaternion.Slerp(from.rotation, to.rotation, 5);
        }
        if (targetPositionShip == ThirdShipTarget.position)
        {
            ship.transform.rotation = Quaternion.Slerp(to.rotation, from.rotation, 5);
        }
        if (targetPositionShip == LastShipTarget.position)
        {
            ship.transform.rotation = Quaternion.Slerp(from.rotation, fromTwo.rotation, 5);
        }
        if (targetPositionShip == firstShipTarget.position)
        {
            ship.transform.rotation = Quaternion.Slerp(fromTwo.rotation, finalTo.rotation, 5);
        }

    }


}
