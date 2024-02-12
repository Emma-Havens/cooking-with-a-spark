using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Meal : MonoBehaviour
{
    // array of all food_items that the order dictates must be in the meal
    Food_type[] order_items;

    // bool array of whether or not the food_item of the same index in the
    // order_items array is already supplied for this meal
    bool[] order_items_bool;

    private bool finished = false;

    // Must be called by Assembly_Station on creation to set order_items
    public void Set_order(GameObject order)
    {
        order_items = order.GetComponent<Order>().order_items;
        order_items_bool = new bool[order_items.Length];
        Array.Fill(order_items_bool, false);
    }

    // checks whether a supplied ingredient is in the order for the meal
    public bool Try_add_item(Food_Item ingredient)
    {
        // DO THIS WHEN APPLIANCES ARE FULLY IMPLEMENTED
        //if (ingredient.state != State.Processed)
        //{
        //    return false;
        //}

        for (int i = 0; i < order_items.Length; i++)
        {
            if (order_items[i] == ingredient.type &&
                order_items_bool[i] != true)
            {
                Debug.Log("adding " + order_items[i]);
                ingredient.Put_Down(Food_Pos(ingredient.type));
                order_items_bool[i] = true;

                // if all order_items have been supplied
                if (order_items_bool.All(x => x))
                {
                    Finish();
                }

                return true;
            }
        }
        return false;
    }

    

    private Vector3 Food_Pos(Food_type f) 
    {
        Vector3 pos;

        switch (f)
        {
            case Food_type.Bun:
                pos = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);    
                break;
            case Food_type.Burger:
                pos = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
                break;
            case Food_type.Lettuce:
                pos = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z);
                break;
            case Food_type.Tomato:
                pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                break;
            case Food_type.Fries:
                pos = new Vector3(transform.position.x + 0.6f, transform.position.y + 1.2f, transform.position.z);
                break;
            default:
                pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                break;
        }

        return pos;
    }

    // automatically finishes meal when all order_items are in
    public void Finish()
    {
        //if (tomato != null && fries != null && !finished)
        //{
        //    Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.6f, transform.position.z);
        //    Instantiate(bun, pos, Quaternion.identity);
        //    finished = true;
        //}
        finished = true;
        Debug.Log("Finished");
    }

    //public void Add_Item(Food_Item f)
    //{
    //    switch (f.type)
    //    {
    //        case Food_type.Bun:
    //            Debug.Log("adding bun");
    //            if (!bun)
    //            {
    //                bun = f;
    //                bun.Put_Down(Food_Pos(Food_type.Bun));
    //            }
    //            break;
    //        case Food_type.Burger:
    //            Debug.Log("adding burger");
    //            if (!burger && bun != null)
    //            {
    //                burger = f;
    //                burger.Put_Down(Food_Pos(Food_type.Burger));
    //            }
    //            break;
    //        case Food_type.Lettuce:
    //            Debug.Log("adding lettuce");
    //            if (!lettuce && burger != null)
    //            {
    //                lettuce = f;
    //                lettuce.Put_Down(Food_Pos(Food_type.Lettuce));
    //            }
    //            break;
    //        case Food_type.Tomato:
    //            Debug.Log("adding tomato");
    //            if (!tomato && lettuce != null)
    //            {
    //                tomato = f;
    //                tomato.Put_Down(Food_Pos(Food_type.Tomato));
    //            }
    //            break;
    //        case Food_type.Fries:
    //            Debug.Log("adding fries");
    //            if (!fries)
    //            {
    //                fries = f;
    //                fries.Put_Down(Food_Pos(Food_type.Fries));
    //            }
    //            break;
    //        default:
    //            break;
    //    }

    //}
}




