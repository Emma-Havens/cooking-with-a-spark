using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycast : MonoBehaviour
{
    public float maxDistance = 5.0f;
    Interactable currObj;
    MeshRenderer currMR;
    // Start is called before the first frame update
    void Start()
    {

        Cursor.visible = false;

    }

    // Update is called once per frame
    void Update()
    {
        CastRay();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Interactable item;

            if (currObj != null && currObj.TryGetComponent<Interactable>(out item))
            {
                item.Interact();
            }
        }
    }

    void CastRay()
    {
        RaycastHit hit;

        // if we hit something:
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            Interactable obj;

            // if hit an interactable:
            if (hit.transform.gameObject.TryGetComponent<Interactable>(out obj))
            {
                // if no previous object: get material, turn on highlight, set curr to obj
                if (!currObj)
                {
                    
                    obj.TryGetComponent<MeshRenderer>(out currMR);
                    currMR.material.SetFloat("_Highlight", 1.0f);
                    currObj = obj;

                }
                // else if previous object is not the same object: turn off previous highlight, turn on current highlight, set curr to obj
                else if (!ReferenceEquals(obj, currObj))
                {
                    currMR.material.SetFloat("_Highlight", 0.0f);
                    
                    obj.TryGetComponent<MeshRenderer>(out currMR);
                    currMR.material.SetFloat("_Highlight", 1.0f);
                    currObj = obj;

                }

            }
            // else, hit but no interactable: reset previous object highlight, set currObj to null
            else
            {
                if (currObj)
                {
                    currMR.material.SetFloat("_Highlight", 0.0f);
                    currObj = null;
                }
            }
           
        }
        // else, no hit: reset previous object highlight, set currObj to null
        else
        {
            if (currObj)
            {
                currMR.material.SetFloat("_Highlight", 0.0f);
                currObj = null;
            }
        }
    }
}
