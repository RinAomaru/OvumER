using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    
    [Range(1f, 10f)]
    public float distanceFromCam = 5;

    public Material outline;
    public Material oldMaterial;
    GameObject grabbedObject;
    GameObject selectedObject;


    // Update is called once per frame
    void Update()
    {
        
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if(Physics.Raycast(Camera.main.gameObject.transform.position, ray.direction, out hit))
        {

            if(selectedObject == null)
            {

                hit.transform.gameObject.GetComponent<Renderer>().material.SetFloat("_LineIntensity", 0.1f);
                selectedObject = hit.transform.gameObject;

            }
            else
            {

                if(selectedObject != hit.transform.gameObject)
                {

                    selectedObject.GetComponent<Renderer>().material.SetFloat("_LineIntensity", 0f);
                    hit.transform.gameObject.GetComponent<Renderer>().material.SetFloat("_LineIntensity", 0.1f);
                    selectedObject = hit.transform.gameObject;

                }

            }
            

        }
        else
        {

            if(selectedObject != null && grabbedObject == null)
            {

                selectedObject.GetComponent<Renderer>().material.SetFloat("_LineIntensity", 0);
                selectedObject = null;

            }

        }

        if(selectedObject != null)
        {
            if(Input.GetMouseButtonDown(0))
            {

                grabbedObject = selectedObject;
                GameManager.instance._grabbed = grabbedObject;

            }
            if(grabbedObject != null)
            {

                Vector3 movepos = Input.mousePosition;
                movepos = Camera.main.ScreenToWorldPoint(new Vector3(movepos.x, movepos.y, distanceFromCam));
                grabbedObject.transform.position = movepos;

            }
            if(Input.GetMouseButtonUp(0))
            {

                grabbedObject = null;
                GameManager.instance._grabbed = null;

            }
        }


    }




}
