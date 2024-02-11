using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Cooking_appliance : Interactable
{
    //whether the appliance is plugged in or not
    //when electricity is implemented, the default should be false
    bool is_powered = true;

    Food_Item cooking_item = null;

    //public so you can watch in editor, and updating progress bar
    public int cook_progress = 0;

    //how much progress is made per fixed_update (50/s)
    public int cook_speed = 1;

    //when cook_progress reaches these numbers, the food's state changes
    public int processed = 1000;
    public int ruined = 1250;

    private Hand player_hand = null;

    public Appliance_Type type;
    Food_type compatible_food_type;
    Dictionary<Appliance_Type, Food_type> dict;

    private void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
        
        //HOW TO GET ACCESS TO Kitchen_Types.cs DICT?
        //dict = Kitchen_Types.Compatible_Food;
        compatible_food_type = dict[type];
    }

    private void FixedUpdate()
    {
        if (cooking_item && is_powered) 
        {
            increment_cook();
        }
    }

    public override void Interact()
    {
        Debug.Log("appliance use detected");

        if (cooking_item == null && player_hand.In_hand() != null)
        {
            Food_Item new_item = player_hand.In_hand() as Food_Item;
            if (new_item.type == compatible_food_type)
            {
                Debug.Log("beginning cooking");

                cooking_item = new_item;
                player_hand.Use_item();
                start_cooking(cooking_item);
            }
            else
            {
                Debug.Log("incompatible food");
            }
            
        }
        else
        {
            Debug.Log("ending cooking");

            stop_cooking();
        }
    }

    public void start_cooking(Food_Item to_cook)
    {
        cooking_item = to_cook;
        //TODO move the object into position above the stove / in the toaster
    }

    public void stop_cooking()
    {
        cook_progress = 0;

        Food_Item temp_item = cooking_item;
        cooking_item = null;

        //return temp_item;
        player_hand.item = temp_item;
    }

    //increments cook_progress by cook_speed
    //checks progress and changes cooking_item.state
    private void increment_cook()
    {
        cook_progress += cook_speed;

        if (cook_progress >= ruined)
        {
            Debug.Log("food ruined!");
            cooking_item.Ruin_food();
        }
        else if (cook_progress >= processed)
        {
            Debug.Log("food cooked!");
            cooking_item.Process_food();
        }
    }
}
