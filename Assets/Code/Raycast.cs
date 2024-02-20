using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEditor.Progress;

public class Raycast : MonoBehaviour
{
    public float maxDistance = 5.0f;
    GameObject currObj;

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

            if (currObj != null)
            {
                if (currObj.TryGetComponent<Interactable>(out item))
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
            GameObject obj = hit.transform.gameObject;
            Interactable T;
           

            // if hit an interactable:
            if (obj.TryGetComponent<Interactable>(out T))
            {
                // if no previous object: highlight obj, set currObj to obj
                if (!currObj)
                {
                    AddHighlight(obj);
                    currObj = obj;

                }
                // else if previous object is not the same object: turn off previous highlight, turn on current highlight, set currObj to obj
                else if (!ReferenceEquals(obj, currObj))
                {

                    RemoveHighlight(currObj);
                    AddHighlight(obj);
                    currObj = obj;

                }
                // else, currObj is same as obj: do nothing

            }
            // else if parent is interactable: highlight parent, set currObj to parent
            else if (obj.transform.parent != null)
            {
                if (obj.transform.parent.gameObject.TryGetComponent<Interactable>(out T))
                {
                    AddHighlight(obj.transform.parent.gameObject);
                    currObj = obj.transform.parent.gameObject;
                }

            }
            // else, hit but no interactable: turn off currObj highlight, set currObj to null
            else
            {
                if (currObj)
                {
                    RemoveHighlight(currObj);
                    currObj = null;
                }
            }
           
        }
        // else, no hit: turn off currObj highlight, set currObj to null
        else
        {
            if (currObj)
            {
                RemoveHighlight(currObj);
                currObj = null;
            }
        }
    }

    void AddHighlight(GameObject obj)
    {
        MeshRenderer currMR;
        List<Material> materials;


        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject child = obj.transform.GetChild(i).gameObject;
            if (child.tag != "no_highlight")
                AddHighlight(child);
        }

        if (obj.TryGetComponent<MeshRenderer>(out currMR))
        {
            materials = currMR.materials.ToList();
            foreach (Material m in materials)
            {
                m.SetFloat("_Highlight", 0.5f);
            }
        }

    }

    void RemoveHighlight(GameObject obj)
    {
        MeshRenderer currMR;
        List<Material> materials;

        for (int i = 0; i < obj.transform.childCount; i++)
        {
            GameObject child = obj.transform.GetChild(i).gameObject;
            RemoveHighlight(child);
        }

        if (obj.TryGetComponent<MeshRenderer>(out currMR))
        {
            materials = currMR.materials.ToList();
            foreach (Material m in materials)
            {
                m.SetFloat("_Highlight", 0.0f);
            }
        }

    }
}
