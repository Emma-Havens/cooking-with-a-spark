using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Food_type
{
    Burger,
    Bun,
    Lettuce,
    Tomato,
    Fries
}

public enum Appliance_Type
{
    Stove,
    Toaster,
    Fryer
}

public class Kitchen_Types : MonoBehaviour
{
    public Dictionary<Appliance_Type, Food_type> Compatible_Food = new Dictionary<Appliance_Type, Food_type>
    {
        {Appliance_Type.Stove, Food_type.Burger},
        {Appliance_Type.Toaster, Food_type.Bun},
        {Appliance_Type.Fryer, Food_type.Fries},
    };
}
