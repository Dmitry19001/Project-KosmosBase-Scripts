using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : PhysicalObject, ICollectable
{
    //Item Type is constraint and shouldn't be changed during gameplay
    ItemType _itemType;
    float _inventoryWeight; //Means how many slots will be used in inventory 1,2,3 etc
    int _stackLimit;

    public Item(
        string name = "",
        string description = "Object that has hp and mass!",
        ItemType itemType = ItemType.Stone,
        int inventoryWeight = 1,
        int stackLimit = 1,
        float baseMass = 1,
        GameObject gameObject = null,
        int maxHealth = 0
        ) : base(name, description, maxHealth, baseMass, gameObject)
    {
        ItmType = itemType;
        InventoryWeight = inventoryWeight;
        StackLimit = stackLimit;
    }

    public float InventoryWeight { get => _inventoryWeight; set => _inventoryWeight = value; }
    public ItemType ItmType { get => _itemType; private set => _itemType = value; }
    public int StackLimit { get => _stackLimit; set => _stackLimit = value; }

    public override void DestroySelf()
    {
        Object.Destroy(GmObject);
    }
}
