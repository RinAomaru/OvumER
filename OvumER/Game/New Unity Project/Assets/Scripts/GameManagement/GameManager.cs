using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public float demoVal = 10f;

    private static GameManager _instance;
    public static GameManager instance
    {

        get
        {

            return _instance;

        }

    }

    void Awake()
    {

        if(_instance == null)
        {

            _instance = this;
            DontDestroyOnLoad(this.gameObject);

        }
        else
        {

            if(instance != this)
            Destroy(this.gameObject);

        }

    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
