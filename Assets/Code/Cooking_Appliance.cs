using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;


public class Cooking_appliance : Interactable
{
    public AudioClip food_processed;
    public AudioClip food_ruined;
    public AudioClip food_cooking;
    public AudioClip food_incompatible;


    //whether the appliance is plugged in or not
    //when electricity is implemented, the default should be false
    public bool is_powered = false;

    GameObject cooking_item = null;

    //public so you can watch in editor, and updating progress bar
    public int cook_progress = 0;

    //how much progress is made per fixed_update (50/s)
    public int cook_speed = 1;

    //when cook_progress reaches these numbers, the food's state changes
    public int processed = 1000;
    public int ruined = 1750;

    private Hand player_hand = null;

    AudioSource audio_s;
    bool already_played_processed = false;
    bool playing_ruined = false;

    public Appliance_Type type;

    public Progress_Bar prog_bar;

    private void Start()
    {
        player_hand = FindObjectOfType<Hand>().GetComponent<Hand>();
        audio_s = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (cooking_item && is_powered) 
        {
            IncrementCook();
            prog_bar.SetProgress(cook_progress);

        }
    }

    public override void Interact()
    {
        Debug.Log("appliance use detected");

        if (cooking_item != null && player_hand.In_hand() == null)
        {
            Debug.Log("ending cooking");

            StopCooking();

            prog_bar.Enable(false);
  
        }

        else if (cooking_item == null && player_hand.In_hand() != null)
        {
            GameObject new_obj = player_hand.In_hand();
            Food_Item new_item = new_obj.GetComponent<Food_Item>();
            if (new_item != null && type == new_item.get_compatible())
            {
                Debug.Log("beginning cooking");

                cooking_item = new_obj;
                player_hand.Use_item();
                StartCooking(cooking_item);

                prog_bar.Enable(true);

            }
            else
            {
                Debug.Log("incompatible food");
                audio_s.PlayOneShot(food_incompatible, .1f);
            }
        }
        else
        {
            audio_s.PlayOneShot(food_incompatible, .1f);
        }
    }

    public void StartCooking(GameObject to_cook)
    {
        cooking_item = to_cook;

        cooking_item.transform.position = transform.position + new Vector3(0, 2, 0);
        cooking_item.transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        cooking_item.GetComponent<BoxCollider>().enabled = false;

        //audio_s.clip = food_cooking;
    }

    public void StopCooking()
    {
        cook_progress = 0;

        GameObject temp_item = cooking_item;
        cooking_item = null;

        //return temp_item;
        player_hand.Pick_up_item(temp_item);

        if (audio_s.isPlaying)
        {
            audio_s.Stop();
        }
        already_played_processed = false;
        playing_ruined = false;
    }

    //increments cook_progress by cook_speed
    //checks progress and changes cooking_item.state
    private void IncrementCook()
    {
        cook_progress += cook_speed;
        if (!already_played_processed && !playing_ruined && !audio_s.isPlaying)
        {
            audio_s.PlayOneShot(food_cooking, .07f);
        }

        if (cook_progress >= ruined)
        {
            Debug.Log("food ruined!");
            playing_ruined = true;
            if (!audio_s.isPlaying)
            {
                //audio_s.clip = food_ruined;
                audio_s.PlayOneShot(food_ruined);
            }
            cooking_item.GetComponent<Food_Item>().Ruin_food();
        }
        else if (cook_progress >= processed)
        {
            Debug.Log("food cooked!");
            if (!already_played_processed)
            {
                //audio_s.clip = food_processed;
                audio_s.Stop();
                audio_s.PlayOneShot(food_processed, .7f);
                already_played_processed = true;
            }
            cooking_item.GetComponent<Food_Item>().Process_food();
        }
    }
}
