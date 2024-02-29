using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Food_type
{
    Bun,
    Bacon,
    Burger,
    Lettuce,
    Tomato,
    Cheese,
    Fries
}

public enum Appliance_Type
{
    Stove,
    Toaster,
    Fryer,
    Chopper
}

public class Kitchen_Types : MonoBehaviour
{
    public Dictionary<Food_type, Appliance_Type> Compatible_Food = new Dictionary<Food_type, Appliance_Type>
    {
        {Food_type.Burger, Appliance_Type.Stove},
        {Food_type.Bun, Appliance_Type.Toaster},
        {Food_type.Fries, Appliance_Type.Fryer},
        {Food_type.Lettuce, Appliance_Type.Chopper},
        {Food_type.Tomato, Appliance_Type.Chopper}
    };
}

public enum Recipe
{
    BLT,
    Breakfast,
    Cheeseburger,
    Double_deck,
    Extra_fries,
    Kiddie_Meal,
    Meatlovers,
    Starter,
    Starter_start,
    Veggie
}
