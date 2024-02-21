using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Timeline.Actions.MenuPriority;
using TMPro;

public class Assembly_Station : Counter
{

    public GameObject Meal_prefab;
    public GameObject Order_prefab;

    public Material assembly_active;
    public Material assembly_inactive;

    MeshRenderer rend;

    // meals active in assembly station
    public Meal meal;

    // the order card visible above the assembly station
    GameObject displayed_order;

    // called before start
    private void Awake()
    {
        rend = GetComponent<MeshRenderer>();
        rend.material = assembly_inactive;
    }

    // Start is called before the first frame update
    void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
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
    public bool Add_meal(GameObject order)
    {
        if (meal == null)
        {
            meal = Instantiate(Meal_prefab, this.transform, false).GetComponent<Meal>();
            meal.Set_order_at_station(order, this);
            Order_prefab = order;
            displayed_order = Instantiate(Order_prefab, this.transform, false);
            transform_order();
            rend.material = assembly_active;
            Debug.Log(rend.material);
            return true;
        }
        return false;
    }

    void transform_order()
    {
        Sprite order_sprite = displayed_order.GetComponent<Image>().sprite;
        Destroy(displayed_order.GetComponent<Image>());
        Destroy(displayed_order.GetComponent<CanvasRenderer>());
        //TMP_Text[] children = GetComponentsInChildren<TMP_Text>();
        //foreach (TMP_Text child in children) {
        //    child.gameObject.AddComponent<MeshRenderer>();
        //    Destroy(child.gameObject.GetComponent<CanvasRenderer>());
        //    // child.gameObject.GetComponent<Transform>().localPosition
        //}
        SpriteRenderer rend = displayed_order.AddComponent<SpriteRenderer>();
        rend.sprite = order_sprite;
        RectTransform trans = displayed_order.GetComponent<RectTransform>();
        Vector3 scale = trans.localScale;
        trans.localScale = new Vector3(scale.x / 10, scale.y / 10, scale.z);
        displayed_order.transform.localPosition = new Vector3(0f, 1.3f, 0.5f);
    }

    // adds the food item to the meal if that move is valid
    private void AddIngredient(GameObject ingredient)
    {
        bool item_used = false;
        if (meal != null && meal.Try_add_item(ingredient) == true)
        {
            player_hand.Use_item();
            item_used = true;
        }
        if (item_used == false)
        {
            Debug.Log("Item was not used");
            // Loudspeaker yells at you
        }
    }

    public void Meal_fulfillment()
    {
        meal = null;
        rend.material = assembly_inactive;
        Destroy(displayed_order);
    }

    public void Meal_timeout()
    {
        if (meal)
        {
            Destroy(meal);
        }
        rend.material = assembly_inactive;
        Destroy(displayed_order);
    }
}
