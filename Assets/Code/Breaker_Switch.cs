using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker_Switch : Interactable
{

    Cooking_appliance appliance;
    Breaker breaker;
    
    Renderer ren;

    public bool on = false;

    private void Start()
    {
        breaker = FindObjectOfType<Breaker>().GetComponent<Breaker>();
        appliance = transform.parent.GetComponent<Cooking_appliance>();

        ren = GetComponent<Renderer>();
        ren.material.color = Color.red;

        transform.Rotate(20, 0, 0);
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
            TurnOff();
        }
    }

    public void TurnOff()
    {
        breaker.remove_load();
        appliance.is_powered = false;

        AnimateOff();
        on = false;
    }


    private void AnimateOn()
    {
        ren.material.color = Color.green;
        transform.Rotate(Vector3.left * 50);
    }

    private void AnimateOff()
    {
        ren.material.color = Color.red;
        transform.Rotate(Vector3.right * 50);
    }
}
