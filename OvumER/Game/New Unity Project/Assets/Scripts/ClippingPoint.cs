using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClippingPoint : MonoBehaviour
{
    
    public GameObject attachedObj;

    public ObjectSnapping.partType connectionType;


    private void Update()
    {

        

        if(attachedObj != null && !Input.GetMouseButton(0))
        {

            attachedObj.transform.position = transform.position;

        }

    }

    private void OnTriggerEnter(Collider other)
    {


        if(other.tag == "grabbable" && GameManager.instance._grabbed != null)
        {

            if(other.gameObject.GetComponent<ObjectSnapping>().part == connectionType )
            {

                if(other.gameObject == GameManager.instance._grabbed)
                {

                    attachedObj = other.gameObject;

                    if(connectionType == ObjectSnapping.partType.leftEye)
                    {

                        GameManager.instance.gatheredLeftEyeID = other.gameObject.GetComponent<ObjectSnapping>().idRef;

                    }
                    if(connectionType == ObjectSnapping.partType.rightEye)
                    {

                        GameManager.instance.gatheredRightEyeID = other.gameObject.GetComponent<ObjectSnapping>().idRef;

                    }
                    if(connectionType == ObjectSnapping.partType.nose)
                    {

                        GameManager.instance.gatheredNoseID = other.gameObject.GetComponent<ObjectSnapping>().idRef;

                    }
                    if(connectionType == ObjectSnapping.partType.mouth)
                    {

                        GameManager.instance.gatheredMouthID = other.gameObject.GetComponent<ObjectSnapping>().idRef;

                    }

                }


            }


        }

    }

    private void OnTriggerExit(Collider other)
    {

        if(other.tag == "grabbable")
        {

            if(other.gameObject == attachedObj)
            {

                attachedObj = null;

                if(connectionType == ObjectSnapping.partType.leftEye)
                    {

                        GameManager.instance.gatheredLeftEyeID = -1;

                    }
                    if(connectionType == ObjectSnapping.partType.rightEye)
                    {

                        GameManager.instance.gatheredRightEyeID = -1;

                    }
                    if(connectionType == ObjectSnapping.partType.nose)
                    {

                        GameManager.instance.gatheredNoseID = -1;

                    }
                    if(connectionType == ObjectSnapping.partType.mouth)
                    {

                        GameManager.instance.gatheredMouthID = -1;

                    }

            }

        }

    }

}
