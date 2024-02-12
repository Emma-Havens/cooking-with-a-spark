using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand : MonoBehaviour
{

    public Hand_Item item;     // player can only hold one thing at a time

    void Start()
    {
        item = null;
    }

    void Update()
    {
        if (item != null)
        {
            //moving object in front of player, and rotating it with the player
            item.transform.position = transform.position + transform.forward + transform.right;
            item.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }

    // sets the hand item to incoming object if hand is not full
    public bool Pick_up_item(Hand_Item obj)
    {
        bool pick_up = false;
        if (item == null)
        {
            item = obj;
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
    public Hand_Item Use_item()
    {
        Hand_Item obj = null;
        if (item != null)
        {
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
    public Hand_Item In_hand()
    {
        return item;
    }

}
