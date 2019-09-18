using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float xSpeed;
    private float zSpeed;
    private float speed;
    private float sprintSpeed;
    private List<GameObject> ingredients;

    private void Start()
    {
        speed = 10f;
        sprintSpeed = 7f;
        ingredients = new List<GameObject>();
    }

    // Update is called once per frame
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

    private void OnTriggerEnter(Collider ingredientCollider)
    {
        if (ingredientCollider.gameObject.CompareTag("Ingredient"))
        {
            var ingredientGo = ingredientCollider.gameObject;
            ingredients.Add(ingredientGo);
            ingredientCollider.gameObject.SetActive(false);
            //for (int i = 0; i < ingredients.Count; i++)
            //{
            //    Debug.Log(ingredients[i].name);
            //}
        }

        if (ingredients.Count > 0)
        {
            string ingredientName = ingredientCollider.gameObject.name;

            switch (ingredientName)
            {
                case "Tomato":
                    Debug.Log("Tomato");
                    break;

                case "Flour":
                    Debug.Log("Flour");
                    break;

                case "Cheese":
                    Debug.Log("Cheese");
                    break;

                case "Niku":
                    Debug.Log("Niku");
                    break;
            }
        }
    }
}
