using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : PhysicalObject, ICollectable
{
    //Item Type is constraint and shouldn't be changed during gameplay
    ResourceType _itemType;
    float _inventoryWeight = 1; //Means how many slots will be used in inventory 1,2,3 etc
    int _stackLimit = 1;

    public float InventoryWeight { get => _inventoryWeight; set => _inventoryWeight = value; }
    public ResourceType ItemType { get => _itemType; set => _itemType = value; }
    public int StackLimit { get => _stackLimit; set => _stackLimit = value; }

    public override void DestroySelf()
    {
        Destroy(gameObject);
    }

    public void DropSelf()
    {
        gameObject.SetActive(true);
        transform.position = Vector3.zero + new Vector3(0, -3, 0); //Should appear at the bottom of the spaceship
        transform.SetParent(null);
    }

    public void Pickup(GameObject reciever)
    {
        var inventory = reciever.GetComponent<InventorySystem>();

        gameObject.SetActive(false);
        transform.SetParent(reciever.transform);

        inventory.AddItem(gameObject);
    }
}
