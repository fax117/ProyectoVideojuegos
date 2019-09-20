﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private float xSpeed;
    private float zSpeed;
    private float speed;
    private float sprintSpeed;
    
    //Galaxy style movement 
    public float jumpHeight;
    public float gravity;
    public bool onGround;
    private float distanceToGround;
    public GameObject groundCheck;
    private Vector3 groundNormal;
    public LayerMask layer;
    private Rigidbody rb;
    public GameObject planet;
    public GameObject playerPlaceholder;

    //Cooking
    private List<GameObject> ingredients;
    private GameObject ingredientToCook;

    private List<GameObject> doneIngredients;
    public GameObject sauce;
    public GameObject dough;
    public GameObject shreddedCheese;
    public GameObject meatCubes;

    public Text cookText;
    public RawImage cheeseIcon;
    public RawImage flourIcon;
    public RawImage meatIcon;
    public RawImage tomatoIcon;

    private void Start()
    {
        speed = 10f;    //tutorial = 4f;
        sprintSpeed = 7f;

        jumpHeight = 1.5f;
        gravity = 20;
        onGround = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        ingredients = new List<GameObject>();
        doneIngredients = new List<GameObject>();
    }

    void Update()
    {
        //Movement
        xSpeed = Input.GetAxisRaw("Horizontal") * Time.deltaTime * speed;
        zSpeed = Input.GetAxisRaw("Vertical") * Time.deltaTime * speed;

        transform.Translate(xSpeed, 0f, zSpeed);

        //Local Rotation
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, 150 * Time.deltaTime, 0);
        }
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -150 * Time.deltaTime, 0);
        }

        //Ground Control
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(groundCheck.transform.position, -groundCheck.transform.up, out hit, 10, layer) )
        {
            distanceToGround = hit.distance;
            groundNormal = hit.normal;

            if(distanceToGround <= 0.2f)
            {
                onGround = true;
            }
            else
            {
                onGround = false;
            }
        }


        //Gavity and Rotation
        Vector3 gravDirection = (transform.position - planet.transform.position).normalized;

        if (onGround == false)
        {
            rb.AddForce(gravDirection * -gravity);
        }

        Quaternion toRotation = Quaternion.FromToRotation(transform.up, groundNormal) * transform.rotation;
        transform.rotation = toRotation;

        Jump();
        Sprint();
    }

    private void ChangePlanet(Collider collision){
        if(collision.transform != planet.transform){
            planet = collision.transform.gameObject;

            Vector3 gravDirection = (transform.position - planet.transform.position).normalized;
            Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravDirection) * transform.rotation;
            transform.rotation = toRotation;
            
            rb.velocity = Vector3.zero;
            rb.AddForce(gravDirection * gravity);

            playerPlaceholder.GetComponent<PlayerPlaceholder>().NewPlanet(planet);

        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(transform.up * 40000 * jumpHeight * Time.deltaTime);
        }
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Planet"))
            ChangePlanet(other);
            
        if (other.gameObject.CompareTag("Ingredient"))
        {
            var ingredientGo = other.gameObject;
            ingredients.Add(ingredientGo);
            other.gameObject.GetComponent<Renderer>().enabled = false;
        }

        var ingredientName = other.name;
        Color tempAlpha;

        switch (ingredientName)
        {
            case "Cheese":
                tempAlpha = cheeseIcon.color;
                tempAlpha.a = 1f;
                cheeseIcon.color = tempAlpha;
                break;

            case "Flour":
                tempAlpha = flourIcon.color;
                tempAlpha.a = 1f;
                flourIcon.color = tempAlpha;
                break;

            case "Meat":
                tempAlpha = meatIcon.color;
                tempAlpha.a = 1f;
                meatIcon.color = tempAlpha;
                break;

            case "Tomato":
                tempAlpha = cheeseIcon.color;
                tempAlpha.a = 1f;
                tomatoIcon.color = tempAlpha;
                break;
        }
       
    }

    private void OnTriggerStay(Collider other)
    {
        string cookWareName = other.gameObject.name;

        switch (cookWareName)
        {
            case "Pot":
                ingredientToCook = GameObject.Find("/Ingredients/Tomato");
                if (ingredients.Contains(ingredientToCook) == false)
                {
                    cookText.gameObject.SetActive(true);
                    cookText.text = "Missing tomatoes";
                }
                break;

            case "Mixer":
                ingredientToCook = GameObject.Find("/Ingredients/Flour");
                if (ingredients.Contains(ingredientToCook) == false)
                {
                    cookText.gameObject.SetActive(true);
                    cookText.text = "Missing flour";
                }
                break;

            case "CuttingBoard":
                ingredientToCook = GameObject.Find("/Ingredients/Cheese");
                if (ingredients.Contains(ingredientToCook) == false)
                {
                    cookText.gameObject.SetActive(true);
                    cookText.text = "Missing cheese";
                }
                break;

            case "Grinder":
                ingredientToCook = GameObject.Find("/Ingredients/Meat");
                if (ingredients.Contains(ingredientToCook) == false)
                {
                    cookText.gameObject.SetActive(true);
                    cookText.text = "Missing meat";
                }
                break;
        }

        if (ingredients.Count > 0 && other.gameObject.tag != "Ingredient")
        {

            switch (cookWareName)
            {
                case "Pot":
                    ingredientToCook = GameObject.Find("/Ingredients/Tomato");
                    if (ingredients.Contains(ingredientToCook))
                    {
                        cookText.gameObject.SetActive(true);
                        cookText.text = "Press F to make sauce";
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            //Invoke("ShowIngredient(sauce)", 1f);
                            sauce.gameObject.SetActive(true);
                        }
                        if (sauce.gameObject.activeSelf == true)
                        {
                            cookText.text = "Sauce done";
                        }
                    }
                    break;

                case "Mixer":
                    ingredientToCook = GameObject.Find("/Ingredients/Flour");
                    if (ingredients.Contains(ingredientToCook))
                    {
                        cookText.gameObject.SetActive(true);
                        cookText.text = "Press F to make dough";
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            dough.gameObject.SetActive(true);
                        }
                        if (dough.gameObject.activeSelf == true)
                        {
                            cookText.text = "Dough ready";
                        }
                    }
                    break;

                case "CuttingBoard":
                    ingredientToCook = GameObject.Find("/Ingredients/Cheese");
                    if (ingredients.Contains(ingredientToCook))
                    {
                        cookText.gameObject.SetActive(true);
                        cookText.text = "Press F to slice the cheese";
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            shreddedCheese.gameObject.SetActive(true);
                        }
                        if (shreddedCheese.gameObject.activeSelf == true)
                        {
                            cookText.text = "Cheese shredded";
                        }
                    }
                    break;

                case "Grinder":
                    ingredientToCook = GameObject.Find("/Ingredients/Meat");
                    if (ingredients.Contains(ingredientToCook))
                    {
                        cookText.gameObject.SetActive(true);
                        cookText.text = "Press F to grind meat";
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            meatCubes.gameObject.SetActive(true);
                        }
                        if (meatCubes.gameObject.activeSelf == true)
                        {
                            cookText.text = "Meat grinded";
                        }
                    }
                    break;
            }
        }

        if (other.gameObject.CompareTag("DoneIngredient"))
        {
            var doneIngredientGo = other.gameObject;
            doneIngredients.Add(doneIngredientGo);
            other.gameObject.GetComponent<Renderer>().enabled = false;
        }

        if (other.gameObject.CompareTag("Pan"))
        {
            other.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit()
    {
        cookText.gameObject.SetActive(false);
    }

    //private void ShowIngredient(GameObject ingredientReady)
    //{
    //    ingredientReady.gameObject.SetActive(true);

    //    if(ingredientReady.gameObject.activeSelf == true)
    //    {
    //        cookText.text = "Done!";
    //    }
    //}
}
