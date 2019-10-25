using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VideoPlayerController : MonoBehaviour
{   

    public Text continueText;

    void Start()
    {
        Invoke("ShowText", 5);   
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }    
    }

    void ShowText()
    {
        continueText.gameObject.SetActive(true);
    }


}
