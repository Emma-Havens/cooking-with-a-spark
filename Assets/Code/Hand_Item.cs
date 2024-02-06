using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hand_Item : MonoBehaviour
{

    protected Hand player_hand;
    protected BoxCollider item_collider;
    protected MeshRenderer item_renderer;

    private bool waiting_for_pickup = false;

    void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
        item_collider = GetComponent<BoxCollider>();
        item_renderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (waiting_for_pickup == true)
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                Debug.Log("pickup detected");
                Potential_pickup();
            }
        }
    }

    // player is close enough to pick up item
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("press \'p\' to pick up item");
        waiting_for_pickup = true;
    }

    // player is no longer close enough to pick up item
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("pickup out of range");
        waiting_for_pickup = false;
    }

    // ensures an item disappears from view after it enters player hand
    void Potential_pickup()
    {
        bool pick_up = player_hand.Pick_up_item(this);
        if (pick_up == true)
        {
            item_collider.enabled = false;
            item_renderer.enabled = false;
            waiting_for_pickup = false;
        }
    }

    public void Put_Down(Vector3 pos)
    {
        transform.position = pos;
        item_collider.enabled = true;
        item_renderer.enabled = true;
    }

}
