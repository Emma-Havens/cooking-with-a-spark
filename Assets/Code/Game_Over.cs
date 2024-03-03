using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Game_Over : MonoBehaviour
{
    private int wrong_orders = 0;
    private RawImage start_screen;

    // Start is called before the first frame update
    void Start()
    {
        wrong_orders = 0;
        TryGetComponent<RawImage>(out start_screen);
        start_screen.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (wrong_orders == 3)
        {
            endGame();
        }
    }

    public void endGame()
    {

        AudioListener.pause = true;
        Time.timeScale = 0.0f;
        start_screen.enabled = true;
    }

    public void Increase()
    {
        wrong_orders += 1;
    }
}
