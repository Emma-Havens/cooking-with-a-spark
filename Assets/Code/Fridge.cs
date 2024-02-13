using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Fridge : Interactable
{
    //whether the appliance is plugged in or not
    //when electricity is implemented, the default should be false
    bool is_powered = true;

    public GameObject foodprefab;

    private Hand player_hand = null;



    // Start is called before the first frame update
    void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Overriding Interact() from Interactable parent class
    public override void Interact()
    {
        if (is_powered)
        {
            Debug.Log("fridge use detected");

            if (player_hand.In_hand() == null)
            {
                GameObject new_food = Instantiate(foodprefab, new Vector3(0, 0, 0), Quaternion.identity);
                player_hand.Pick_up_item(new_food);
            }

        }

    }

}
