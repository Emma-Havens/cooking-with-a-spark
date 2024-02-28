using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Item : Interactable
{

    protected Hand player_hand;
    protected BoxCollider item_collider;
    protected MeshRenderer item_renderer;
    private Counter counter = null;

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
        Assembly_Station a;

        if (counter != null)
        {
            if (counter.TryGetComponent<Assembly_Station>(out a))
                a.Interact();
            else
                Potential_pickup();
        }
        else
        {
            Debug.Log("pickup detected");
            Potential_pickup();

        }
           

    }

    // ensures an item disappears from view after it enters player hand
    public void Potential_pickup()
    {
        if (player_hand.Pick_up_item(gameObject))
        {

            if (counter != null)
            {
                counter.TakeFood();
                counter = null;
                
            }
                
        }
    }

    public void Put_Down(Vector3 pos, Counter c)
    {
        counter = c;
        transform.position = pos;
        transform.rotation = Quaternion.identity;
        item_collider.enabled = true;
        item_renderer.enabled = true;
    }

}
