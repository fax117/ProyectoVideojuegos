using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgMusicMenu : MonoBehaviour
{

    private static BgMusicMenu instance = null;

    public static BgMusicMenu Instance
    {
        get
        {
            return instance;
        }
    }

    void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
