/* 
    ------------------- Code Monkey -------------------

    Thank you for downloading this package
    I hope you find it useful in your projects
    If you have any questions let me know
    Cheers!

               unitycodemonkey.com
    --------------------------------------------------
 */

using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataHandler : MonoBehaviour
{

    [SerializeField] private GameObject unitGameObject;
    private PlayerScript unit;

    private void Awake()
    {
        unit = unitGameObject.GetComponent<PlayerScript>();
        SaveSystem.Init();
    }

    private void Update()
    {

    }

    public void Save()
    {
        // Save
        Vector3 playerPosition = unit.transform.localPosition;
        List<GameObject> ingredients = unit.ingredients;
        List<GameObject> doneIngredients = unit.doneIngredients;
        List<GameObject> pizzas = unit.pizzas;

        SaveObject saveObject = new SaveObject
        {
            ingredients = ingredients,
            playerPosition = playerPosition,
            doneIngredients = doneIngredients,
            pizzas = pizzas
        };
        string json = JsonUtility.ToJson(saveObject);
        SaveSystem.Save(json);

    }

    public void Load()
    {
        // Load
        string saveString = SaveSystem.Load();
        if (saveString != null)
        {

            SaveObject saveObject = JsonUtility.FromJson<SaveObject>(saveString);

            unit.transform.position = saveObject.playerPosition;
            unit.ingredients = saveObject.ingredients;
            unit.doneIngredients = saveObject.doneIngredients;
            unit.pizzas = saveObject.pizzas;
        } else {
        }
    }


    private class SaveObject
    {
        public Vector3 playerPosition;
        public List<GameObject> ingredients;
        public List<GameObject> doneIngredients;
        public List<GameObject> pizzas;
    }
}