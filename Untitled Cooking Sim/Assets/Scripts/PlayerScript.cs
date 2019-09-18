using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private float xSpeed;
    private float zSpeed;
    private float speed;
    private float sprintSpeed;
    private List<GameObject> ingredients;
    private GameObject ingredientToCook;
    public Text cookText;

    private void Start()
    {
        speed = 10f;
        sprintSpeed = 7f;
        ingredients = new List<GameObject>();
    }

    void Update()
    {
        xSpeed = Input.GetAxisRaw("Horizontal");
        zSpeed = Input.GetAxisRaw("Vertical");
        gameObject.transform.Translate(new Vector3(xSpeed * Time.deltaTime * speed, 0f, zSpeed * Time.deltaTime * speed));
        Sprint();
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
        if (other.gameObject.CompareTag("Ingredient"))
        {
            var ingredientGo = other.gameObject;
            ingredients.Add(ingredientGo);
            other.gameObject.GetComponent<Renderer>().enabled = false;
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
                ingredientToCook = GameObject.Find("/Ingredients/Niku");
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
                        cookText.text = "Press E to make sauce";
                    }
                    break;

                case "Mixer":
                    ingredientToCook = GameObject.Find("/Ingredients/Flour");
                    if (ingredients.Contains(ingredientToCook))
                    {

                        cookText.gameObject.SetActive(true);
                        cookText.text = "Press E to make pasta";
                    }
                    break;

                case "CuttingBoard":
                    ingredientToCook = GameObject.Find("/Ingredients/Cheese");
                    if (ingredients.Contains(ingredientToCook))
                    {

                        cookText.gameObject.SetActive(true);
                        cookText.text = "Press E to slice the cheese";
                    }
                    break;

                case "Grinder":
                    ingredientToCook = GameObject.Find("/Ingredients/Niku");
                    if (ingredients.Contains(ingredientToCook))
                    {

                        cookText.gameObject.SetActive(true);
                        cookText.text = "Press E to grind meat";
                    }
                    break;
            }
        }
    }

    private void OnTriggerExit()
    {
        cookText.gameObject.SetActive(false);
    }
}
