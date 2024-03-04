using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class Game_Over : MonoBehaviour
{
    private int wrong_orders = 0;
    private RawImage lose_screen;
    private GameObject losescoretext;
    private TextMeshProUGUI outt = null;


    // Start is called before the first frame update
    void Start()
    {
        wrong_orders = 0;
        TryGetComponent<RawImage>(out lose_screen);
        lose_screen.enabled = false;


        losescoretext = GameObject.Find("LoseScore");
        losescoretext.TryGetComponent<TextMeshProUGUI>(out outt);
        if (outt)
        {
            outt.enabled = false;
        }
        //losescoretext.SetActive(false);

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
        lose_screen.enabled = true;
        outt.enabled = true;

    }

    public void Increase()
    {
        wrong_orders += 1;
    }
}
