using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Order : MonoBehaviour
{
    // how long player has to complete order in float seconds
    float time_limit;

    // initialization time of order
    float start_time;

    // determines ingredients and time_limit
    public Recipe recipe;

    // specifies the ingredients the order requires
    // note that all ingredients should have state State.Processed
    // but that is not made explicit here
    public Food_type[] order_items;

    // reference to the assembly station this order is assigned to
    Assembly_Station assembly_station;

    // timer object visible on screen
    TMP_Text timer;

    // the timer of the order in scene above assembly station
    TMP_Text displayed_order_timer;

    // whether or not this order is the first order
    bool starter_order;

    // needs to happen BEFORE start is run
    private void Awake()
    {
        Initialize_order_items();
    }

    void Start()
    {
        if (!starter_order)
        {
            Initialize_timer();
            Set_timer();
        }
    }

    void Initialize_order_items()
    {
        switch (this.recipe)
        {
            case Recipe.BLT:
                time_limit = 160;
                starter_order = false;
                order_items = new Food_type[] { Food_type.Bun, Food_type.Bacon,
                                  Food_type.Lettuce, Food_type.Tomato };
                break;
            case Recipe.Breakfast:
                time_limit = 160;
                starter_order = false;
                order_items = new Food_type[] { Food_type.Bun, Food_type.Bacon,
                                  Food_type.Cheese, Food_type.Fries };
                break;
            case Recipe.Cheeseburger:
                time_limit = 220;
                starter_order = false;
                order_items = new Food_type[] { Food_type.Bun, Food_type.Burger,
                                  Food_type.Lettuce, Food_type.Tomato,
                                  Food_type.Fries, Food_type.Cheese };
                break;
            case Recipe.Double_deck:
                time_limit = 200;
                starter_order = false;
                order_items = new Food_type[] { Food_type.Bun, Food_type.Burger,
                                  Food_type.Burger, Food_type.Tomato, Food_type.Lettuce };
                break;
            case Recipe.Extra_fries:
                time_limit = 200;
                starter_order = false;
                order_items = new Food_type[] { Food_type.Bun, Food_type.Burger,
                                  Food_type.Lettuce, Food_type.Fries, Food_type.Fries };
                break;
            case Recipe.Kiddie_Meal:
                time_limit = 180;
                starter_order = false;
                order_items = new Food_type[] { Food_type.Bun, Food_type.Burger,
                                  Food_type.Lettuce, Food_type.Cheese, Food_type.Fries };
                break;
            case Recipe.Meatlovers:
                time_limit = 200;
                starter_order = false;
                order_items = new Food_type[] { Food_type.Bun, Food_type.Burger,
                                  Food_type.Bacon, Food_type.Cheese, Food_type.Fries };
                break;
            case Recipe.Starter:
                time_limit = 180;
                starter_order = false;
                order_items = new Food_type[] { Food_type.Bun, Food_type.Burger,
                                  Food_type.Lettuce, Food_type.Tomato,
                                  Food_type.Fries };
                break;
            case Recipe.Starter_start:
                starter_order = true;
                order_items = new Food_type[] { Food_type.Bun, Food_type.Burger,
                                  Food_type.Lettuce, Food_type.Tomato,
                                  Food_type.Fries };
                break;
            case Recipe.Veggie:
                time_limit = 200;
                starter_order = false;
                order_items = new Food_type[] { Food_type.Bun, Food_type.Cheese,
                                  Food_type.Lettuce, Food_type.Tomato,
                                  Food_type.Fries };
                break;
        }
    }

    // sets up timer and start_time
    void Initialize_timer()
    {
        TMP_Text[] list = GetComponentsInChildren<TMP_Text>();
        TMP_Text text1 = list[0];
        TMP_Text text2 = list[1];
        if (text1.gameObject.transform.position.y <
            text2.gameObject.transform.position.y)
        {
            timer = text1;
        }
        else
        {
            timer = text2;
        }
        start_time = Time.time;
    }

    // called by order_manager on order init
    public void Assign_assembly_station(Assembly_Station station)
    {
        assembly_station = station;
    }

    public void Set_displayed_timer(TMP_Text displayed)
    {
        displayed_order_timer = displayed;
    }

    void Update()
    {
        if (!starter_order)
        {
            Set_timer();
            if (Time.time > start_time + time_limit)
            {
                Order_timeout();
            }
        }
    }

    // runs timer countdown
    void Set_timer()
    {
        float cur_time = time_limit - (Time.time - start_time);
        float minutes = Mathf.FloorToInt(cur_time / 60);
        float seconds = Mathf.FloorToInt(cur_time % 60);
        string time = string.Format("{0:00}:{1:00}", minutes, seconds);
        timer.text = time;
        if (displayed_order_timer)
        {
            displayed_order_timer.text = time;
        }
    }

    // called when order timer runs out
    void Order_timeout()
    {
        // make loudspeaker man mad
        Debug.Log(assembly_station);
        if (assembly_station)
        {
            assembly_station.Meal_timeout();
        }
        Destroy(this.gameObject);
    }

    public void Order_fulfillment()
    {
        if (starter_order)
        {
            Order_Manager manager = FindAnyObjectByType<Order_Manager>();
            manager.Starter_order_done();
        }
        // something good
        Destroy(this.gameObject);
    }
}
