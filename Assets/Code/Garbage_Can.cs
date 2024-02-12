using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage_Can : Interactable
{
    protected Hand player_hand;


    void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
    }

    void Update()
    {

    }

    public override void Interact()
    {
        Debug.Log("throw away detected");
        Hand_Item item = player_hand.Use_item();
        if (item != null )
        {
            item.GetComponent<MeshRenderer>().enabled = false;
        }
        
    }

}
