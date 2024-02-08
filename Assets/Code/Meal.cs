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

    private bool finished = false;
   

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Add_Item(Food_Item f)
    {
        switch(f.type)
        {
            case Food_type.Bun:
                Debug.Log("adding bun");
                if (!bun)
                {
                    bun = f;
                    bun.Put_Down(Food_Pos(Food_type.Bun));
                }   
                break;
            case Food_type.Burger:
                Debug.Log("adding burger");
                if (!burger && bun != null)
                {
                    burger = f;
                    burger.Put_Down(Food_Pos(Food_type.Burger));
                }  
                break;
            case Food_type.Lettuce:
                Debug.Log("adding lettuce");
                if (!lettuce && burger != null)
                {
                    lettuce = f;
                    lettuce.Put_Down(Food_Pos(Food_type.Lettuce));
                }
                break;
            case Food_type.Tomato:
                Debug.Log("adding tomato");
                if (!tomato && lettuce != null)
                {
                    tomato = f;
                    tomato.Put_Down(Food_Pos(Food_type.Tomato));
                }
                break;
            case Food_type.Fries:
                Debug.Log("adding fries");
                if (!fries)
                {
                    fries = f;
                    fries.Put_Down(Food_Pos(Food_type.Fries));
                }  
                break; 
            default:
                break;
        }

    }

    private Vector3 Food_Pos(Food_type f) 
    {
        Vector3 pos;

        switch (f)
        {
            case Food_type.Bun:
                pos = new Vector3(transform.position.x, transform.position.y + 1.2f, transform.position.z);    
                break;
            case Food_type.Burger:
                pos = new Vector3(transform.position.x, transform.position.y + 1.3f, transform.position.z);
                break;
            case Food_type.Lettuce:
                pos = new Vector3(transform.position.x, transform.position.y + 1.4f, transform.position.z);
                break;
            case Food_type.Tomato:
                pos = new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z);
                break;
            case Food_type.Fries:
                pos = new Vector3(transform.position.x + 0.6f, transform.position.y + 1.2f, transform.position.z);
                break;
            default:
                pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                break;
        }

        return pos;
    }

    public void Finish()
    {
        if (tomato != null && fries != null && !finished)
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 1.6f, transform.position.z);
            Instantiate(bun, pos, Quaternion.identity);
            finished = true;
        }
    }
}
