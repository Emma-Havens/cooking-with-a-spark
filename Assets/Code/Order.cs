using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Recipe
{
    Everything,
    Full_meal
}

public class Order : MonoBehaviour
{
    // how long player has to complete order
    float time_limit;

    // how long the order has been active
    float cur_timer = 0;

    // determines ingredients and time_limit
    public Recipe recipe;

    // specifies the ingredients the order requires
    // note that all ingredients should have state State.Processed
    // but that is not made explicit here
    public Food_type[] ingredients;
   
    void Start()
    {
        switch (this.recipe)
        {
            case Recipe.Everything:
                time_limit = 10000;
                ingredients = new Food_type[] { Food_type.Bun, Food_type.Burger,
                                  Food_type.Lettuce, Food_type.Tomato };
                break;
            case Recipe.Full_meal:
                time_limit = 11000;
                ingredients = new Food_type[] { Food_type.Bun, Food_type.Burger,
                                  Food_type.Lettuce, Food_type.Tomato,
                                  Food_type.Fries };
                break;
        }
        
    }

    private void FixedUpdate()
    {
        cur_timer++;
        if (cur_timer > time_limit)
        {
            // something bad ig?
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
