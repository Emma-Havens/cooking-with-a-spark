using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Item : Interactable
{

    protected Hand player_hand;
    protected BoxCollider item_collider;
    protected MeshRenderer item_renderer;

    void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
        item_collider = GetComponent<BoxCollider>();
        item_renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {

    }

    public override void Interact()
    {
        Debug.Log("pickup detected");
        Potential_pickup();

    }

    // ensures an item disappears from view after it enters player hand
    void Potential_pickup()
    {
        bool pick_up = player_hand.Pick_up_item(this);
        if (pick_up == true)
        {
            item_collider.enabled = false;
        }
    }

    public void Put_Down(Vector3 pos)
    {
        transform.position = pos;
        item_collider.enabled = true;
        item_renderer.enabled = true;
    }

}
