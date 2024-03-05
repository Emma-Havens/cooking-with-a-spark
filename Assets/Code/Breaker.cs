using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : Interactable
{
    public AudioClip flip_breaker;
    public AudioClip breaker_error;

    AudioSource audio_s;

    public int max_load = 3;
    int current_load = 0;

    LoadKeeper loadKeeper;

    private void Start()
    {
        audio_s = GetComponent<AudioSource>();
        GetComponent<Renderer>().material.color = Color.red;

        loadKeeper = FindObjectOfType<LoadKeeper>().GetComponent<LoadKeeper>();
    }

    //called by switches
    //checks to see if more load can be added
    //if yes, increment load and return true
    //if not, return false
    public bool add_load()
    {
        if (current_load + 1 > max_load)
        {
            Debug.Log("Load could not be increased");
            audio_s.PlayOneShot(breaker_error);
            return false;
        }
        else
        {
            Debug.Log("Load increased");
            current_load++;
            audio_s.PlayOneShot(flip_breaker);

            loadKeeper.Increase();
            return true;
        }
    }

    public override void Interact()
    {
        foreach (var breaker_switch in FindObjectsOfType<Breaker_Switch>())
        {
            if (breaker_switch.on)
            {
                breaker_switch.TurnOff();
            }
        }
    }

    public void remove_load()
    {
        Debug.Log("Load decreased");
        current_load--;
        audio_s.PlayOneShot(flip_breaker);

        loadKeeper.Decrease();
    }

    public int get_current_load()
    {
        return current_load;
    }

    public int get_max_load()
    {
        return max_load;
    }
}
