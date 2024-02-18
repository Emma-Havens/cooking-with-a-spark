using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker_Switch : Interactable
{

    //has to be manually set for each switch in the inspector
    public Cooking_appliance appliance;
    bool on = false;

    Breaker breaker;

    private void Start()
    {
        breaker = FindObjectOfType<Breaker>();
    }

    public override void Interact()
    {
        if (!on)
        {
            //checks if can add load, and updates current load if yes
            if (breaker.add_load())
            {
                appliance.is_powered = true;

                on = true;
            }
        }

        else
        {
            breaker.remove_load();
            appliance.is_powered = false;

            on = false;
        }
    }
}
