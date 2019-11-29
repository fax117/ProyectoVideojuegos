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
using UnityEngine.UI;

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
        Color cheeseIcon = unit.cheeseIcon.color;
        Color meatIcon = unit.meatIcon.color;
        Color tomatoIcon = unit.tomatoIcon.color;
        Color flourIcon = unit.flourIcon.color;
        SaveObject saveObject = new SaveObject
        {
            ingredients = ingredients,
            playerPosition = playerPosition,
            doneIngredients = doneIngredients,
            pizzas = pizzas,
            cheeseIcon = cheeseIcon,
            meatIcon = meatIcon,
            tomatoIcon = tomatoIcon,
            flourIcon = flourIcon
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
            unit.cheeseIcon.color = saveObject.cheeseIcon;
            unit.meatIcon.color = saveObject.meatIcon;
            unit.tomatoIcon.color = saveObject.tomatoIcon;
            unit.flourIcon.color = saveObject.flourIcon;
        } else {
        }
    }


    private class SaveObject
    {
        public Vector3 playerPosition;
        public List<GameObject> ingredients;
        public List<GameObject> doneIngredients;
        public List<GameObject> pizzas;
        public Color cheeseIcon;
        public Color meatIcon;
        public Color tomatoIcon;
        public Color flourIcon;
    }
}