using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Assembly_Station : Counter
{

    public GameObject Meal_prefab;

    // maximum number of meals being built
    // same as max_orders in Order_Manager
    int max_meals;

    // meals active in assembly station
    public Meal[] meal_array;

    // Start is called before the first frame update
    void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
        max_meals = FindAnyObjectByType<Order_Manager>().max_orders;
        meal_array = new Meal[max_meals];
    }

    public override void Interact()
    {
        Debug.Log("assembly station use detected");

        if (player_hand.In_hand() != null)
        {
            GameObject hand_item = player_hand.In_hand();
            if (hand_item.GetComponent<Food_Item>() != null)
            {
                AddIngredient(hand_item);
            }
        }
    }

    // automatically adds a potential meal when an order comes in
    public void Add_meal(GameObject order)
    {
        int i = 0;
        while (meal_array[i] != null)
        {
            i++;
            Debug.Log(meal_array[i]);
        }
        meal_array[i] = Instantiate(Meal_prefab, this.transform, false).GetComponent<Meal>();
        meal_array[i].Set_order(order);
        Debug.Log("meal set at " + i);
        Debug.Log(meal_array[i]);
    }

    // preferentially adds the food item to the oldest order
    // DOESN'T DO THAT YET
    private void AddIngredient(GameObject ingredient)
    {
        bool item_used = false;
        int i = 0;
        while (item_used == false && i < meal_array.Length)
        {
            if (meal_array[i] != null
               && meal_array[i].Try_add_item(ingredient) == true)
            {
                player_hand.Use_item();
                item_used = true;
            }
            i++;
        }
        if (item_used == false)
        {
            Debug.Log("Item was not used");
            // Loudspeaker yells at you
        }
    }
}
