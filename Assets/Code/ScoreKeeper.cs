using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    private int score = 0;

    private GameObject scoretext;


    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoretext = transform.GetChild(1).gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Increase()
    {
        score += 1;
        if (score < 10)
        {
            scoretext.GetComponent<TextMeshPro>().text = "0" + score.ToString();
        }
        else
        {
            scoretext.GetComponent<TextMeshPro>().text = score.ToString();
        }
    }

    public int GetScore()
    {
        return score;
    }


}
