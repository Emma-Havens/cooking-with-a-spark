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

    void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
        item_collider = GetComponent<BoxCollider>();
        item_renderer = GetComponent<MeshRenderer>();

        state = State.Raw;
        Debug.Log(state);
        Debug.Log(type);
    }

    // appliances call this to cook or chop food items
    public void Process_food()
    {
        state = State.Processed;
    }

    // appliances call this to burn or ruin food items
    public void Ruin_food()
    {
        state = State.Ruined;
    }
}
