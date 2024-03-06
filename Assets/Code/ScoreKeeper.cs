using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{

    private int score = 0;

    private GameObject scoretext;

    private GameObject losescoretext;
    private TextMeshProUGUI outt = null;

    private GameObject x1;
    private GameObject x2;

    private Game_Over lose;
    private int losses = 0;

    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        scoretext = transform.GetChild(1).gameObject;
        losescoretext = GameObject.Find("LoseScore");
        if (losescoretext == null )
        {
            Debug.Log("hiiiiiiiiiiiiiiiiii");
        }

        x1 = transform.GetChild(3).gameObject;
        x1.SetActive(false);
        x2 = transform.GetChild(4).gameObject;
        x2.SetActive(false);

        lose = FindObjectOfType<Game_Over>();


        //losescoretext.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        losses = lose.wrong_orders;
        if (losses == 1)
        {
            x1.SetActive(true);
        }
        if (losses == 2)
        {
            x2.SetActive(true);
        }

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

        losescoretext.TryGetComponent<TextMeshProUGUI>(out outt);
        if (outt)
        {
            outt.text = "Score: " + score.ToString();
        }
    }

        public int GetScore()
    {
        return score;
    }


}
