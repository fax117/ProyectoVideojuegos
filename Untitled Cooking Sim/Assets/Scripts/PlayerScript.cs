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
        speed = 10f;
        sprintSpeed = 7f;
        ingredients = new List<GameObject>();
        doneIngredients = new List<GameObject>();
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
                        cookText.text = "Press E to make sauce";
                        if (Input.GetKeyDown(KeyCode.E))
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
                        cookText.text = "Press E to make dough";
                        if (Input.GetKeyDown(KeyCode.E))
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
                        cookText.text = "Press E to slice the cheese";
                        if (Input.GetKeyDown(KeyCode.E))
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
                        cookText.text = "Press E to grind meat";
                        if (Input.GetKeyDown(KeyCode.E))
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
