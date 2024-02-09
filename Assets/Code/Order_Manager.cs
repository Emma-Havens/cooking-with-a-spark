using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Order_Manager : MonoBehaviour
{
    public GameObject Everything_burger_prefab;

    // the maximum number of orders a player can be given at a time
    int max_orders = 4;

    // holds the position to place each order card in the order box
    Vector3[] slot_array = new Vector3[4];

    GameObject[] order_array = new GameObject[4];

    // transform of the order box (area where orders are displayed)
    RectTransform box_transform;

    

    void Start()
    {
        box_transform = GetComponent<RectTransform>();
        Fill_slot_array();
        Generate_Order();
    }

    // used in initialization. Calculates positions on canvas for orders
    void Fill_slot_array()
    {
        Vector2 ll_order_box = box_transform.anchorMin;
        print(ll_order_box);

        float height = box_transform.rect.height;
        float y_val = ll_order_box.y + height / 2;

        float width = box_transform.rect.width;
        float cell_width = width / max_orders;
        float x_val = ll_order_box.x + cell_width / 2;

        for (int i = 0; i < max_orders; i++) {
            slot_array[i] = new Vector3(x_val, y_val, 1);
            x_val += cell_width;
        }
    }

    // returns a order gameobject. May eventually return a random order
    GameObject Generate_Order()
    {
        int i = 0;
        while (order_array[i] != null)
        {
            i++;
        }
        GameObject order = Instantiate(Everything_burger_prefab,
                                       box_transform,
                                       false);
        order.transform.position = slot_array[i];
        order_array[i] = order;
        print(slot_array[i]);
        return order;
    }
}
