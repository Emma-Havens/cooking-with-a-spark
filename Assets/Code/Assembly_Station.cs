using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Timeline.Actions.MenuPriority;

public class Assembly_Station : MonoBehaviour
{

    private bool within_range = false;
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
        if (within_range == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
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
        }

    }

    private void AddIngredient(Food_Item item)
    {
        /*
        if (!meal)
        {
            meal = gameObject.AddComponent<Meal>();
        }
        */

        meal.Add_Item(item);
        player_hand.Use_item();
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("press \'e\' to use assembly station");
        within_range = true;
    }

    // player is no longer close enough to use appliance
    private void OnTriggerExit(Collider other)
    {
        Debug.Log("assembly station out of range");
        within_range = false;
    }
}
