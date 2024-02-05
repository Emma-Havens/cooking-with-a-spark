using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meal : MonoBehaviour
{
    private Food_Item burger;
    private Food_Item bun;
    private Food_Item lettuce;
    private Food_Item tomato;
    private Food_Item fries;

    public enum State
    {
        Unfinished,
        Finished
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void addItem(Food_Item f)
    {
        switch(f.GetComponent<Food_type>())
        {
            case Food_type.Burger:
                if (!burger)
                    burger = f;
                break;
            case Food_type.Bun:
                if (!bun)
                    bun = f;
                break;
            case Food_type.Lettuce:
                if (!lettuce)
                    lettuce = f;
                break;
            case Food_type.Tomato:
                if (!tomato)
                    tomato = f;
                break;
            case Food_type.Fries:
                if (!fries)
                    fries = f;
                break; 
            default:
                break;
        }

    }
}
