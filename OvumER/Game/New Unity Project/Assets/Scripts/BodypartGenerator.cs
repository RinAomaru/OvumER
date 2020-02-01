using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodypartGenerator : MonoBehaviour
{
    

    public GameObject[] availableEyesLeft;
    public GameObject[] availableEyesRight;
    public GameObject[] availableNoses;
    public GameObject[] availableMouths;


    public Bodypart eyeLeft;
    public Bodypart eyeRight;
    public Bodypart nose;
    public Bodypart mouth;
  

    void Awake()
    {


    }

    public void GenerateFace()
    {

        GameObject[] oldFace = GameObject.FindGameObjectsWithTag("faceComponent");
        for(int i = 0; i < oldFace.Length; i++)
        {

            Destroy(oldFace[i]);

        }
        GameObject[] oldParts = GameObject.FindGameObjectsWithTag("grabbable");
        for(int i = 0; i < oldFace.Length; i++)
        {

            Destroy(oldParts[i]);

        }

        int randVal = Random.Range(0, availableEyesLeft.Length);
        Instantiate(availableEyesLeft[randVal], eyeLeft.transform.position, Quaternion.identity);
        GameManager.instance.leftEyeID = randVal;

        randVal = Random.Range(0, availableEyesRight.Length);
        Instantiate(availableEyesRight[randVal], eyeRight.transform.position, Quaternion.identity);
        GameManager.instance.rightEyeID = randVal;

        randVal = Random.Range(0, availableNoses.Length);
        Instantiate(availableNoses[randVal], nose.transform.position, Quaternion.identity);
        GameManager.instance.noseID = randVal;

        randVal = Random.Range(0, availableMouths.Length);
        Instantiate(availableMouths[randVal], mouth.transform.position, Quaternion.identity);
        GameManager.instance.mouthID = randVal;

    }

}
