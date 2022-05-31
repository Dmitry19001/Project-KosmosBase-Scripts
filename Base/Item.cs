using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    string _name;
    float _weight;
    float _inventoryWeight; //Means how many slots will be used in inventory 1,2,3 etc



    string Name
    {
        get
        {
            return _name;
        }
        set
        {
            if (value != null && value.Trim().Length > 0)
            {
                _name = value;
            }
            else
            {
                _name = "Unknown Item";
            }
        }
    }
}
