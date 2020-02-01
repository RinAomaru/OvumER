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

            }

        }

    }

}
