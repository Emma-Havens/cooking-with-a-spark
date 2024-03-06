using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Progress_Bar : MonoBehaviour
{
    public GameObject bar;
    public GameObject full_bar;
    public TextMeshPro text;
    Cooking_appliance appliance;
    private int processed;
    private int ruined;
    // Start is called before the first frame update
    void Start()
    {
        appliance = GetComponentInParent<Cooking_appliance>();
        processed = appliance.processed;
        ruined = appliance.ruined;
        bar.transform.localScale = new Vector3(0, 0, 0);
        Enable(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Enable(bool e)
    {
       bar.transform.localScale = new Vector3(0, 0, 0);
       bar.SetActive(e);
       full_bar.SetActive(e);
       text.text = "";

    }

    public void SetProgress(float prog)
    {
        float val = prog / processed;
        if (val > 1) val = 1;

        bar.transform.localScale = new Vector3(val , 1, 1);
        bar.transform.localPosition = new Vector3 ((1.0f-val) * 5.0f, 0.01f, 0.0f);

        if (prog < processed)
        {
            bar.GetComponent<MeshRenderer>().material.color = new Color(1, val, 0);
            text.text = (Mathf.RoundToInt(val * 100)).ToString() + "%";
        }
        else if (prog < ruined)
        {
            bar.GetComponent<MeshRenderer>().material.color = Color.green;
            text.text = "TAKE OUT";
        }
        else
        {
            bar.GetComponent<MeshRenderer>().material.color = Color.red;
            text.text = "RUINED";
        }

        
    }
}
