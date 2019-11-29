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
    private float mouseX;
    private float rotationSpeed;
    private float timeRun;
    private float minsLeft, secsLeft, secs, mins;

    //Galaxy style movement 
    public float jumpHeight;
    //private float gravity;
    public bool onGround;
    private float distanceToGround;
    public GameObject groundCheck;
    private Vector3 groundNormal;
    public LayerMask layer;
    private Rigidbody rb;
    public GameObject planet;
    public GameObject playerPlaceholder;

    //Cooking
    public List<GameObject> ingredients;
    private GameObject ingredientToCook;

    public List<GameObject> doneIngredients;
    public GameObject sauce;
    public GameObject dough;
    public GameObject shreddedCheese;
    public GameObject meatCubes;
    public GameObject rawPizza;
    public GameObject pizza;

    public List<GameObject> pizzas;

    public Text cookText;
    public Text timerText;
    public RawImage winText;
    public RawImage cheeseIcon;
    public RawImage flourIcon;
    public RawImage meatIcon;
    public RawImage tomatoIcon;
    public RawImage gameOver;
    public Button restartButton;

    public GameObject recipe;

    public AudioSource audioPlayer;
    public AudioClip doughSound;
    public AudioClip graterSound;
    public AudioClip chopSound;
    public AudioClip boilingSound;
    public AudioClip ovenDone;
    public AudioClip pizzSplat;

    private void Start()
    {
        speed = 20f;    //tutorial = 4f;
        sprintSpeed = 5f;
        timeRun = 180;

        rotationSpeed = 10f;
        
        jumpHeight = 1.5f;
        //gravity = 20;
        onGround = true;
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        ingredients = new List<GameObject>();
        doneIngredients = new List<GameObject>();
        pizzas = new List<GameObject>();
        audioPlayer = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {
        Movement();
        GroudnControl();
        ShowRecipie();
        Jump();
        Sprint();
        Timer();
    }

    private void ChangePlanet(Collider collision){
        if(collision.transform != planet.transform){
            var pt = collision.gameObject.GetComponent<GravityAtractor>();
            planet = collision.transform.gameObject;

            transform.GetComponent<GravityBody>().planet = pt;

            playerPlaceholder.GetComponent<PlayerPlaceholder>().NewPlanet(planet);
        }
    }

    private void Movement(){
        //Movement
        xSpeed = Input.GetAxisRaw("Horizontal");
        zSpeed = Input.GetAxisRaw("Vertical");
        mouseX = Input.GetAxisRaw("Mouse X") * rotationSpeed;

        Quaternion rotActual = transform.rotation; 

        Vector3 playerMovement = new Vector3(xSpeed, 0f, zSpeed).normalized * speed * Time.deltaTime;

        transform.Translate(playerMovement, Space.Self);

        if(Input.GetAxis("Mouse X") != 0){
            transform.Rotate(rotActual.y, mouseX , rotActual.z);
        }
    }

    private void GroudnControl(){
        //Ground Control
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(groundCheck.transform.position, -groundCheck.transform.up, out hit, 10, layer) )
        {

            Debug.DrawRay(groundCheck.transform.position, groundCheck.transform.TransformDirection(-Vector3.up) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");

            distanceToGround = hit.distance;
            groundNormal = hit.normal;

            if(distanceToGround <= 0.3f )//&& hit.collider != <>()
            {
                onGround = true;
            }
            else
            {
                onGround = false;
            }

            
        }

        if (Input.GetKey(KeyCode.Tab))
        {
            recipe.SetActive(true);
        }
        else
        {
            recipe.SetActive(false);
        }

        var ingredientsToCheck = GameObject.FindGameObjectsWithTag("Ingredient");
        foreach (GameObject ingredientToCheck in ingredientsToCheck)
        {
            if (ingredients.Contains(ingredientToCheck))
            {
                ingredientToCheck.GetComponent<MeshRenderer>().enabled = false;
                ingredientToCheck.GetComponent<Collider>().enabled = false;
            }else{
                ingredientToCheck.GetComponent<MeshRenderer>().enabled = true;
                ingredientToCheck.GetComponent<Collider>().enabled = true;
            }
        }

        //Gavity and Rotation
        /* Vector3 gravDirection = (transform.position - planet.transform.position).normalized;

         if (onGround == false)
         {
             rb.AddForce(gravDirection * -gravity);
         }

         Quaternion toRotation = Quaternion.FromToRotation(transform.up, gravDirection) * transform.rotation;
         transform.rotation = toRotation;
         groundCheck.transform.rotation = toRotation;*/

        Jump();
        Sprint();
        Timer();
    }

    private void ChangePlanet(Collider collision){
        if(collision.transform != planet.transform){
            var pt = collision.gameObject.GetComponent<GravityAtractor>();
            planet = collision.transform.gameObject;

            transform.GetComponent<GravityBody>().planet = pt;

            playerPlaceholder.GetComponent<PlayerPlaceholder>().NewPlanet(planet);

        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // && groundCheck
        {
            //rb.AddForce(transform.up * 20f * jumpHeight, ForceMode.Impulse);
            rb.AddForce(transform.up * 30000 * jumpHeight * Time.deltaTime);
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
            other.gameObject.GetComponent<Collider>().enabled = false;
        }

        var ingredientName = other.name;
        Color tempAlpha;

        switch (ingredientName)
        {
            case "Cheese":
                var otherIngredient = GameObject.Find("/Ingredients/Cheese");
                if(ingredients.Contains(otherIngredient)){
                    tempAlpha = cheeseIcon.color;
                    tempAlpha.a = 1f;
                    cheeseIcon.color = tempAlpha;
                }else{
                    tempAlpha = cheeseIcon.color;
                    tempAlpha.a = 0.2f;
                    cheeseIcon.color = tempAlpha;
                }
                break;

            case "Flour":
                otherIngredient = GameObject.Find("/Ingredients/Flour");
                if(ingredients.Contains(otherIngredient)){
                    tempAlpha = flourIcon.color;
                    tempAlpha.a = 1f;
                    flourIcon.color = tempAlpha;
                }else{
                    tempAlpha = cheeseIcon.color;
                    tempAlpha.a = 0.2f;
                    cheeseIcon.color = tempAlpha;
                }
                break;

            case "Meat":
                otherIngredient = GameObject.Find("/Ingredients/Meat");
                if(ingredients.Contains(otherIngredient)){
                    tempAlpha = meatIcon.color;
                    tempAlpha.a = 1f;
                    meatIcon.color = tempAlpha;
                }else{
                    tempAlpha = cheeseIcon.color;
                    tempAlpha.a = 0.2f;
                    cheeseIcon.color = tempAlpha;
                }
                break;

            case "Tomato":
                otherIngredient = GameObject.Find("/Ingredients/Tomato");
                if(ingredients.Contains(otherIngredient)){
                    tempAlpha = cheeseIcon.color;
                    tempAlpha.a = 1f;
                    tomatoIcon.color = tempAlpha;
                }else{
                    tempAlpha = cheeseIcon.color;
                    tempAlpha.a = 0.2f;
                    cheeseIcon.color = tempAlpha;
                }
                break;
        }

        if (other.gameObject.CompareTag("DoneIngredient"))
        {
            var doneIngredientGo = other.gameObject;
            doneIngredients.Add(doneIngredientGo);
            other.gameObject.GetComponent<Renderer>().enabled = false;
            other.gameObject.GetComponent<Collider>().enabled = false;
        }

        if (other.gameObject.CompareTag("Pan"))
        {
            other.gameObject.SetActive(false);
        }
    }

    private IEnumerator OnTriggerStay(Collider other)
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
                        if (audioPlayer.isPlaying)
                        {
                            cookText.text = "Preparing...";
                        }
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            audioPlayer.clip = boilingSound;
                            audioPlayer.Play();
                            yield return new WaitForSeconds(3);
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
                        if (audioPlayer.isPlaying)
                        {
                            cookText.text = "Preparing...";
                        }
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            audioPlayer.clip = doughSound;
                            audioPlayer.Play();
                            yield return new WaitForSeconds(3);
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
                        if (audioPlayer.isPlaying)
                        {
                            cookText.text = "Preparing...";
                        }
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            audioPlayer.clip = graterSound;
                            audioPlayer.Play();
                            yield return new WaitForSeconds(3);
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
                        if (audioPlayer.isPlaying)
                        {
                            cookText.text = "Preparing...";
                        }
                        if (Input.GetKeyDown(KeyCode.F))
                        {
                            audioPlayer.clip = chopSound;
                            audioPlayer.Play();
                            yield return new WaitForSeconds(3);
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

        if (other.gameObject.CompareTag("Tray"))
        {
            if (doneIngredients.Count >= 4)
            {
                cookText.gameObject.SetActive(true);
                cookText.text = "F to prepare pizza";
                if (audioPlayer.isPlaying)
                {
                    cookText.text = "Preparing...";
                }
                if (Input.GetKeyDown(KeyCode.F))
                {
                    cookText.gameObject.SetActive(true);
                    audioPlayer.clip = pizzSplat;
                    audioPlayer.Play();
                    yield return new WaitForSeconds(0.5f);
                    rawPizza.gameObject.SetActive(true);
                }
            }
            else
            {
                cookText.gameObject.SetActive(true);
                cookText.text = "Gather all the ingredients and cook them!";
            }

            if (rawPizza.activeSelf == true)
            {
                cookText.text = "X to pick up pizza";
                if (Input.GetKeyDown(KeyCode.X))
                {
                    rawPizza.gameObject.SetActive(false);
                    pizzas.Add(rawPizza);
                }
            }

            if (pizzas.Count > 0)
            {
                cookText.text = "Cook your pizza";
            }
        }

        if (other.gameObject.CompareTag("Oven"))
        {
            if (pizzas.Count > 0)
            {
                cookText.gameObject.SetActive(true);
                cookText.text = "F to cook pizza";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    audioPlayer.clip = ovenDone;
                    audioPlayer.Play();
                    yield return new WaitForSeconds(3);
                    pizza.gameObject.SetActive(true);
                    pizzas.Remove(rawPizza);
                }

            }
            else
            {
                cookText.gameObject.SetActive(true);
                cookText.text = "Gather all the ingredients and cook them!";
            }
        }

        if (other.gameObject == pizza)
        {
            cookText.gameObject.SetActive(true);
            cookText.text = "Press F to pick up and deliver";
            if (Input.GetKeyDown(KeyCode.F))
            {
                pizzas.Add(pizza);
                pizza.gameObject.SetActive(false);
                cookText.gameObject.SetActive(false);
            }
        }

        if (other.gameObject.CompareTag("Deliver"))
        {
            cookText.gameObject.SetActive(true);
            cookText.text = "Cook first";
            if (pizzas.Contains(pizza))
            {
                
                cookText.text = "F to deliver";
                //winText.text = "Well done!";
                if (Input.GetKeyDown(KeyCode.F))
                {
                    cookText.gameObject.SetActive(false);
                    winText.gameObject.SetActive(true);
                    restartButton.gameObject.SetActive(true);
                    timerText.gameObject.SetActive(false);
                    pizzas.Clear();
                }
            }

            if (winText.gameObject.activeSelf == true)
            {
                cookText.gameObject.SetActive(false);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        cookText.gameObject.SetActive(false);
    }

    private void ShowIngredient(GameObject ingredientReady)
    {
        ingredientReady.gameObject.SetActive(true);

        if (ingredientReady.gameObject.activeSelf == true)
        {
            cookText.text = "Done!";
        }
    }

    private void Timer()
    {
        if (timeRun > 60)
        {
            minsLeft = timeRun / 60;
            secsLeft = timeRun % 60;
            mins = Mathf.RoundToInt(timeRun -= Time.deltaTime) / 60;
            secs = Mathf.Round(secsLeft -= Time.deltaTime) % 60;
        }
        else
        {
            mins = 00;
            secs = Mathf.Round(timeRun -= Time.deltaTime) % 60;
        }

        timerText.text = "Timer: " + mins + ":" + secs;

        if (secs < 10)
        {
            timerText.text = "Timer: " + mins + ":0" + secs;
        }

        if (timeRun <= 0)
        {
            Time.timeScale = 0;
            gameOver.gameObject.SetActive(true);
            timerText.gameObject.SetActive(false);
            restartButton.gameObject.SetActive(true);
        }

    }

    private void ShowRecipie(){
        if (Input.GetKey(KeyCode.Tab)){
            recipe.SetActive(true);
        }
        else{
            recipe.SetActive(false);
        }
    }
}
