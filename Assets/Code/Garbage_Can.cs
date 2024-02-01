using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage_Can : MonoBehaviour
{
    protected Hand player_hand;

    private bool wating_for_garbage = false;

    void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
    }

    void Update()
    {
        if (wating_for_garbage == true)
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                Debug.Log("throw away detected");
                Potential_garbage();
            }
        }
    }

    // player is close enough to throw away an item
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("press \'t\' to trash an item");
        wating_for_garbage = true;
    }

    // player is no longer close enough to throw away an item
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("trash out of range");
        wating_for_garbage = false;
    }

    // ensures an item disappears from player hand if it is trashed
    void Potential_garbage()
    {
        Hand_Item item = player_hand.Use_item();
        if (item != null)
        {
            wating_for_garbage = false;
        }
    }
}
