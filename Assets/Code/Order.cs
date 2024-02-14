using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum Recipe
{
    Everything,
    Full_meal
}

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

    // timer object visible on screen
    TMP_Text timer;

    // needs to happen BEFORE start is run
    private void Awake()
    {
        Initialize_order_items();
    }

    void Start()
    {
        Initialize_timer();
        Set_timer();
    }

    void Initialize_order_items()
    {
        switch (this.recipe)
        {
            case Recipe.Everything:
                time_limit = 60;
                order_items = new Food_type[] { Food_type.Bun, Food_type.Burger,
                                  Food_type.Lettuce, Food_type.Tomato };
                break;
            case Recipe.Full_meal:
                time_limit = 65;
                order_items = new Food_type[] { Food_type.Bun, Food_type.Burger,
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

    void Update()
    {
        Set_timer();
        if (Time.time > start_time + time_limit)
        {
            Order_timeout();
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
    }

    // called when order timer runs out
    void Order_timeout()
    {
        // make loudspeaker man mad
        // destory meal
        Destroy(this.gameObject);
    }

    public void Order_fulfillment()
    {
        // something good
        Destroy(this.gameObject);
    }
}
