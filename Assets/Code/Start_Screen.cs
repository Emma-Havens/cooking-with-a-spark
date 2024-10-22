using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Start_Screen : MonoBehaviour
{
    bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0.0f;

    }

    // Update is called once per frame
    void Update()
    {

        if (!start)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                RawImage start_screen;
                start = true;
                Time.timeScale = 1.0f;
                AudioListener.pause = false;
                TryGetComponent<RawImage>(out start_screen);
                start_screen.enabled = false;
            }   

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.P))
            {
                RawImage start_screen;
                start = false;
                Time.timeScale = 0.0f;
                AudioListener.pause = true;
                TryGetComponent<RawImage>(out start_screen);
                start_screen.enabled = true;
            }
        }
    }
}
