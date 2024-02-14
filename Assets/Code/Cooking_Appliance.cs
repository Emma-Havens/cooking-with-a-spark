using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public class Cooking_appliance : Interactable
{
    //whether the appliance is plugged in or not
    //when electricity is implemented, the default should be false
    bool is_powered = true;

    GameObject cooking_item = null;

    //public so you can watch in editor, and updating progress bar
    public int cook_progress = 0;

    //how much progress is made per fixed_update (50/s)
    public int cook_speed = 1;

    //when cook_progress reaches these numbers, the food's state changes
    public int processed = 1000;
    public int ruined = 1750;

    private Hand player_hand = null;

    public Appliance_Type type;

    public Progress_Bar prog_bar;

    private void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
    }

    private void FixedUpdate()
    {
        if (cooking_item && is_powered) 
        {
            increment_cook();
            prog_bar.SetProgress(cook_progress);

        }
    }

    public override void Interact()
    {
        Debug.Log("appliance use detected");

        if (cooking_item != null && player_hand.In_hand() == null)
        {
            Debug.Log("ending cooking");

            stop_cooking();

            prog_bar.Enable(false);
  
        }

        else if (cooking_item == null && player_hand.In_hand() != null)
        {
            GameObject new_obj = player_hand.In_hand();
            Food_Item new_item = new_obj.GetComponent<Food_Item>();
            if (new_item != null && type == new_item.get_compatible())
            {
                Debug.Log("beginning cooking");

                cooking_item = new_obj;
                player_hand.Use_item();
                start_cooking(cooking_item);

                prog_bar.Enable(true);

            }
            else
            {
                Debug.Log("incompatible food");
            }
        }
    }

    public void start_cooking(GameObject to_cook)
    {
        cooking_item = to_cook;

        cooking_item.transform.position = transform.position + new Vector3(0, 1.5f, 0);
        cooking_item.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }

    public void stop_cooking()
    {
        cook_progress = 0;

        GameObject temp_item = cooking_item;
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
            cooking_item.GetComponent<Food_Item>().Ruin_food();
        }
        else if (cook_progress >= processed)
        {
            Debug.Log("food cooked!");
            cooking_item.GetComponent<Food_Item>().Process_food();
        }
    }
}
