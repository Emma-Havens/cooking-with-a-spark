using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Progress_Bar : MonoBehaviour
{
    public GameObject bar;
    public GameObject full_bar;
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

    }

    public void SetProgress(float prog)
    {
        float val = prog / (processed + 200);

        bar.transform.localScale = new Vector3(val , 1, 1);
        bar.transform.localPosition = new Vector3 ((1.0f-val) * 5.0f, 0.01f, 0.0f);
;
        if (prog < 3.0f * processed/4 | prog > ruined)
        {
            bar.GetComponent<MeshRenderer>().material.color = new Color(1, 0, 0);   
        }
        else if (prog < processed)
        {
            bar.GetComponent<MeshRenderer>().material.color = new Color(1 - (4 * prog / processed - 3), (4 * prog / processed) - 3, 0);
        }
        else
        {
            bar.GetComponent<MeshRenderer>().material.color = new Color(0, 1, 0);
        }
    }
}
