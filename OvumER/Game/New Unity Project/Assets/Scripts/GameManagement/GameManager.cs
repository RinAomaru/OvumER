using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    
    public GameObject _grabbed; // global reference for currently held face component

    public int leftEyeID; //id for each of the randomly generated face components
    public int rightEyeID;
    public int noseID;
    public int mouthID;



    public int gatheredLeftEyeID = -1; //id for the player selected face components
    public int gatheredRightEyeID = -1;
    public int gatheredNoseID = -1;
    public int gatheredMouthID = -1;

    private static GameManager _instance; //protected access for the gamemanager, allows all scripts to access the gamemanager without replacing it
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

    public void Update()
    {

        if(gatheredLeftEyeID > -1 && gatheredRightEyeID > -1 && gatheredNoseID > -1 && gatheredMouthID > -1)
        {

            CompareEgg();

        }

    }

    public void CompareEgg() //checks if the player constructed egg is the same as the reference egg
    {

        if(gatheredLeftEyeID == leftEyeID && gatheredRightEyeID == rightEyeID && gatheredNoseID == noseID && gatheredMouthID == mouthID)
        {

            Debug.Log("Success!");
            

        }
        else
        {

            Debug.Log("NOPE.AVI");

        }
        gatheredLeftEyeID = -1;
        gatheredRightEyeID = -1;
        gatheredNoseID = -1;
        gatheredMouthID = -1;
        GameObject eggGen = GameObject.FindWithTag("eggGenerator");
        eggGen.GetComponent<BodypartGenerator>().GenerateFace();

    }

}
