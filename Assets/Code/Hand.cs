using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class Hand : MonoBehaviour
{

    public GameObject item = null;     // player can only hold one thing at a time

    public Camera cam;

    void Update()
    {
        if (item != null)
        {
            //moving object in front of player, and rotating it with the player
            item.transform.position = cam.transform.position + cam.transform.forward + Vector3.Scale(- cam.transform.right - cam.transform.up, new Vector3(.5f, .5f, .5f));
            item.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles.x, cam.transform.rotation.eulerAngles.y, 0);
        }
    }

    // sets the hand item to incoming object if hand is not full
    public bool Pick_up_item(GameObject obj)
    {
        bool pick_up = false;
        if (item == null && obj.GetComponent<Hand_Item>() != null)
        {
            item = obj;
            item.GetComponent<Collider>().enabled = false;
            pick_up = true;
            Debug.Log("item has been picked up");
        } else
        {
            Debug.Log("item has not been picked up");
        }
        return pick_up;
    }

    // returns the current hand item which MAY BE NULL
    // the contents of the hand is 'used up'
    public GameObject Use_item()
    {
        GameObject obj = null;
        if (item != null)
        {
            item.GetComponent<Collider>().enabled = true;
            obj = item;
            item = null;
            Debug.Log("item has been used");
        } else
        {
            Debug.Log("no item currently in hand");
        }
        return obj;
    }

    // returns the current hand item WITHOUT using it up
    public GameObject In_hand()
    {
        return item;
    }

}
