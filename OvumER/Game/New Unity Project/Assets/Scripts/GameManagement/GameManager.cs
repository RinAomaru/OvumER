using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public GameObject _grabbed;

    public int leftEyeID;
    public int rightEyeID;
    public int noseID;
    public int mouthID;

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



}
