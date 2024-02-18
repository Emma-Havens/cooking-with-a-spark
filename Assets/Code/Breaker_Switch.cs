using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker_Switch : Interactable
{

    //has to be manually set for each switch in the inspector
    public Cooking_appliance appliance;
    bool on = false;

    Breaker breaker;
    Renderer ren;

    private void Start()
    {
        breaker = FindObjectOfType<Breaker>().GetComponent<Breaker>();

        ren = GetComponent<Renderer>();
        ren.material.color = Color.red;
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
                AnimateOn();
            }
        }

        else
        {
            breaker.remove_load();
            appliance.is_powered = false;

            AnimateOff();
            on = false;
        }
    }

    private void AnimateOn()
    {
        ren.material.color = Color.green;
        transform.rotation = Quaternion.Euler(-10, 0, 0);
    }

    private void AnimateOff()
    {
        ren.material.color = Color.red;
        transform.rotation = Quaternion.Euler(10, 0, 0);
    }
}
