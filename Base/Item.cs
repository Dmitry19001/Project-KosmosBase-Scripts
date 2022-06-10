using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : PhysicalObject, ICollectable
{
    //Item Type is constraint and shouldn't be changed during gameplay
    ResourceType _itemType;
    float _inventoryWeight = 1; //Means how many slots will be used in inventory 1,2,3 etc
    int _stackLimit = 1;

    //public Item(
    //    string name = "",
    //    string description = "Object that has hp and mass!",
    //    ItemType itemType = ItemType.Stone,
    //    int inventoryWeight = 1,
    //    int stackLimit = 1,
    //    float baseMass = 1,
    //    GameObject gameObject = null,
    //    int maxHealth = 0
    //    ) : base(name, description, maxHealth, baseMass, gameObject)
    //{
    //    ItmType = itemType;
    //    InventoryWeight = inventoryWeight;
    //    StackLimit = stackLimit;
    //}

    public float InventoryWeight { get => _inventoryWeight; set => _inventoryWeight = value; }
    public ResourceType ItemType { get => _itemType; set => _itemType = value; }
    public int StackLimit { get => _stackLimit; set => _stackLimit = value; }

    public override void DestroySelf()
    {
        Destroy(gameObject);
    }
}
