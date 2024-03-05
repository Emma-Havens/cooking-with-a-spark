using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class LoadKeeper : MonoBehaviour
{

    private int load = 0;

    private GameObject scoretext;

    void Start()
    {
        scoretext = transform.GetChild(1).gameObject;
    }

    public void Increase()
    {
        load += 1;
        scoretext.GetComponent<TextMeshPro>().text = load.ToString();
    }

    public void Decrease()
    {
        load -= 1;
        scoretext.GetComponent<TextMeshPro>().text = load.ToString();
    }
}