using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public GameObject ingredient;
    public Collider ingredientCollider;
    // Start is called before the first frame update
    void Start()
    {
        ingredientCollider = ingredient.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
