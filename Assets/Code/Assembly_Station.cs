using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Assembly_Station : Counter
{

    private Hand player_hand;
    public Meal meal;
    private Vector3 mealPos;

    // Start is called before the first frame update
    void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
        meal = gameObject.AddComponent<Meal>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public override void Interact()
    {
        Debug.Log("assembly station use detected");

        if (player_hand.In_hand() != null)
        {
            Food_Item item;
            if (player_hand.In_hand().TryGetComponent<Food_Item>(out item))
                AddIngredient(item);
        }
        else
        {
            meal.Finish();
        }
    }

    private void AddIngredient(Food_Item item)
    {

        meal.Add_Item(item);
        player_hand.Use_item();
    }

}
