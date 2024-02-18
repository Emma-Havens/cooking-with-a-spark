using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breaker : MonoBehaviour
{
    public int max_load = 3;
    int current_load = 0;

    //called by switches
    //checks to see if more load can be added
    //if yes, increment load and return true
    //if not, return false
    public bool add_load()
    {
        if (current_load++ >= max_load)
        {
            Debug.Log("Load could not be increased");
            return false;
        }
        else
        {
            Debug.Log("Load increased");
            current_load++;
            return true;
        }
    }

    public void remove_load()
    {
        Debug.Log("Load decreased");
        current_load--;
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
