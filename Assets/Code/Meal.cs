using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Numerics;
using UnityEngine;


public class Meal : MonoBehaviour
{
    // the order object this meal fulfills
    Order order;

    // array of all foods that the order dictates must be in the meal
    Food_type[] order_items;

    // references to the food gameobjects in the assembly station
    GameObject[] ingredients;

    // bool array of whether or not the food_item of the same index in the
    // order_items array is already supplied for this meal
    bool[] order_items_bool;

    // current height of the meal (used for placing next item)
    float meal_height;
    float initial_meal_height;
    float fry_depth;

    // used in Finish to top off burger
    public GameObject bun_prefab;

    // used to place food_item objects on
    private Assembly_Station station;

    void Start()
    {
        initial_meal_height = transform.position.y + 1.1f;
        meal_height = initial_meal_height + 0.1f;
        fry_depth = transform.position.z;
    }

    // Must be called by Assembly_Station on creation to set order_items
    public void Set_order_at_station(GameObject assigned_order, Assembly_Station assembly_station)
    {
        station = assembly_station;
        order = assigned_order.GetComponent<Order>();
        order_items = order.order_items;
        order_items_bool = new bool[order_items.Length];
        ingredients = new GameObject[order_items.Length];
        Array.Fill(order_items_bool, false);
    }

    // checks whether a supplied ingredient is in the order for the meal
    // gameobject has already been checked to have a food item component
    public bool Try_add_item(GameObject ingredient)
    {
        Food_Item ing = ingredient.GetComponent<Food_Item>();

        if (ing.state != State.Processed)
        {
            return false;
        }

        for (int i = 0; i < order_items.Length; i++)
        {
            if (order_items[i] == ing.type &&
                order_items_bool[i] != true)
            {
                Debug.Log("adding " + order_items[i]);
                ing.Put_Down(Food_Pos(ing.type), station);
                order_items_bool[i] = true;
                ingredients[i] = ingredient;

                // if all order_items have been supplied
                if (order_items_bool.All(x => x))
                {
                    StartCoroutine(Finish());
                }
                return true;
            }
        }
        return false;
    }

    // returns the position the food_item should appear at
    private Vector3 Food_Pos(Food_type f)
    {
        Vector3 pos;

        switch (order.recipe)
        {
            case Recipe.Extra_fries:
                switch (f)
                {
                    case Food_type.Bun:
                        pos = new Vector3(transform.position.x, initial_meal_height, transform.position.z);
                        break;
                    case Food_type.Fries:
                        pos = new Vector3(transform.position.x + 0.6f, initial_meal_height, fry_depth);
                        fry_depth -= 0.6f;
                        break;
                    default:
                        pos = new Vector3(transform.position.x, meal_height, transform.position.z);
                        meal_height += 0.1f;
                        break;
                }
                break;
            default:
                switch (f)
                {
                    case Food_type.Bun:
                        pos = new Vector3(transform.position.x, initial_meal_height, transform.position.z);
                        break;
                    case Food_type.Fries:
                        pos = new Vector3(transform.position.x + 0.6f, initial_meal_height, transform.position.z);
                        break;
                    default:
                        pos = new Vector3(transform.position.x, meal_height, transform.position.z);
                        meal_height += 0.1f;
                        break;
                }
                break;
        }
        

        return pos;
    }

    // automatically finishes meal when all order_items are in
    public IEnumerator Finish()
    {

        Vector3 pos = new Vector3(transform.position.x, meal_height, transform.position.z);
        //GameObject top_bun = Instantiate(bun_prefab, pos, Quaternion.identity);

        order.Order_fulfillment();
        Debug.Log("Finished");

        yield return new WaitForSecondsRealtime(1);

        station.Meal_fulfillment();
        //Destroy(top_bun);
        Destroy(this.gameObject);
    }

    private void OnDestroy()
    {
        for (int i = 0; i < ingredients.Length; i++)
        {
            if (ingredients[i] != null)
            {
                Destroy(ingredients[i]);
            }
        }
    }
}




