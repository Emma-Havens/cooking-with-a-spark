using System.Collections;
using System.Collections.Generic;
//using System.Numerics;
using UnityEngine;

public class Order_Manager : MonoBehaviour
{
    public GameObject Everything_burger_prefab;
    public GameObject Full_meal_prefab;
    public GameObject Starter_prefab;

    // the maximum number of orders a player can be given at a time
    public int max_orders = 4;

    // holds the position to place each order card in the order box
    // slot closest to the edge of the screen where new orders are inserted
    // corresponds to slot_array[max_orders - 1]
    Vector3[] slot_array;

    // holds all of the active orders. New orders are always inserted at [0], older
    // orders are moved up
    public GameObject[] order_array;

    // true if the order array is full
    bool orders_full = false;

    // true if order waiting coroutine is running
    bool waiting = true;

    // base time to wait between orders
    float wait_time = 120;

    // transform of the order box (area where orders are displayed)
    RectTransform box_transform;

    // assembly station references. There SHOULD be as many stations as max_orders
    Assembly_Station[] assembly_stations;

    void Start()
    {
        box_transform = GetComponent<RectTransform>();
        assembly_stations = FindObjectsByType<Assembly_Station>(FindObjectsSortMode.None);
        order_array = new GameObject[max_orders];
        slot_array = new Vector3[max_orders];
        Fill_slot_array();
        Get_starter_order();
    }

    // used in initialization. Calculates positions on canvas for orders
    void Fill_slot_array()
    {
        float y_val = 0;

        // gameobject was set to have width cell_width * 4
        // everything will be sized properly if max_orders = 4
        float width = box_transform.rect.width;
        float cell_width = width / max_orders;
        float x_val = -width / 2 + cell_width / 2;

        for (int i = 0; i < max_orders; i++)
        {
            slot_array[i] = new Vector3(x_val, y_val, 1);
            x_val += cell_width;
        }
    }

    void Get_starter_order()
    {
        Generate_order(Starter_prefab);
    }

    public void Starter_order_done()
    {
        waiting = false;
    }

    private void Update()
    {
        if (waiting == false)
        {
            StartCoroutine(Waiting_to_make_order());
        }
    }

    // populates new orders after waiting time
    IEnumerator Waiting_to_make_order()
    {
        waiting = true;
        if (order_array[0] == null)
        {
            orders_full = false;
        }
        Generate_order(Everything_burger_prefab);
        yield return new WaitForSecondsRealtime(wait_time);
        waiting = false;
    }

    // returns a order gameobject. May eventually return a random order
    void Generate_order(GameObject food_prefab)
    {
        if (orders_full == false)
        {
            for (int i = 0; i < 4; i++)
            {
                if (order_array[i] != null)
                {
                    order_array[i].transform.localPosition = slot_array[i - 1];
                    order_array[i - 1] = order_array[i];
                    order_array[i] = null;
                    
                }
            }
            if (order_array[0] != null)
            {
                orders_full = true;
            }
            GameObject order = Instantiate(food_prefab,
                                           box_transform,
                                           false);
            order.transform.localPosition = slot_array[max_orders - 1];
            order_array[max_orders - 1] = order;

            int j = 0;
            while (assembly_stations[j].Add_meal(order) == false)
            {
                j++;
            }
            order.GetComponent<Order>().Assign_assembly_station(assembly_stations[j]);
        }
    }
}

// implement boss?
// add variation to wait_time

