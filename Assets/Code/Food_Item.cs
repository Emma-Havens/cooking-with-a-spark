using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// all food items have one of the following states, according to how
// it has been affected by appliances
public enum State
{
    Raw,
    Processed,
    Ruined
}

public class Food_Item : Hand_Item
{
    
    public State state;      // either Raw, Processed, or Ruined
    public Food_type type;   // either Burger, Bun, Lettuce, Tomato, Fries

    public GameObject nextStage;

    void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
        item_collider = GetComponent<BoxCollider>();
        item_renderer = GetComponent<MeshRenderer>();

    }

    public Appliance_Type get_compatible()
    {
        var kitchen_types = FindObjectOfType<Kitchen_Types>().GetComponent<Kitchen_Types>();
        return kitchen_types.Compatible_Food[type];
    }

}
