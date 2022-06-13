using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour 
{
    public event EventHandler OnInventoryChanged;
    public event EventHandler OnInventorySizeChanged;
    public event EventHandler OnInventorySizeIncreased;
    public event EventHandler OnInventorySizeDecreased;
    public event EventHandler OnItemDrop;
    public event EventHandler OnRemoveFromInventory;
    public event EventHandler OnAddToInventory;

    [SerializeField] private int _startingSlots;

    [SerializeField] private GameObject[] _inventory;
    
    public GameObject[] Inventory { get => _inventory; private set => _inventory = value; }
    public int InventorySize { get => Inventory.Length; }

    private void Awake()
    {
        Inventory = new GameObject[_startingSlots];
    }

    private void Start()
    {
        OnInventoryChanged?.Invoke(this, EventArgs.Empty);
    }

    public bool AddItem(GameObject item, bool silent = false)
    {
        for (int i = 0; i < Inventory.Length; i++)
        {
            if (Inventory[i] == null)
            {
                Inventory[i] = item;
                if (!silent) 
                { 
                    OnAddToInventory?.Invoke(this, EventArgs.Empty);
                    OnInventoryChanged?.Invoke(this, EventArgs.Empty);
                }

                return true; //Success
            }
        }

        //Well fuck
        return false;
    }

    public void RemoveItem(int id)
    {
        if (id > InventorySize - 1 && Inventory[id] != null)
        {
            return; //Wrong ID
        }

        Inventory[id] = null;
        OnRemoveFromInventory?.Invoke(this, EventArgs.Empty);
        OnInventoryChanged?.Invoke(this, EventArgs.Empty);
    }

    public void DropItem(int id, bool silent = false)
    {
        var item = Inventory[id];

        item.GetComponent<Item>().DropSelf();

        Inventory[id] = null;

        if (!silent)
        {
            OnItemDrop?.Invoke(this, EventArgs.Empty);
            OnInventoryChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public void DropAll(bool silent = false)
    {
        for (int i = 0; i < Inventory.Length;i++)
        {
            var item = Inventory[i];
            if (item != null)
            {
                DropItem(i, silent);
                Inventory[i] = null;
            }
        }
    }

    public void TransferItem( GameObject item, InventorySystem inventoryReciever )
    {
        //Transfer TODO;
    }

    public void IncreaseSize(int size)
    {
        if (InventorySize + size >= 0)
        {
            var temp = Inventory;

            Inventory = new GameObject[InventorySize + size];

            for (int i = 0; i < temp.Length; i++)
            {
                if(!AddItem(temp[i], true))
                {
                    temp[i].GetComponent<Item>().DropSelf(); ;
                }
            }

            OnInventorySizeChanged?.Invoke(this, EventArgs.Empty);
            OnInventorySizeIncreased?.Invoke(this, EventArgs.Empty);
        }
    }

    public void DecreaseSize(int size)
    {
        if (InventorySize - size >= 0)
        {
            var temp = Inventory;

            Inventory = new GameObject[InventorySize - size];

            for (int i = 0; i < temp.Length; i++)
            {
                AddItem(temp[i], true);
            }

            OnInventorySizeChanged?.Invoke(this, EventArgs.Empty);
            OnInventorySizeDecreased?.Invoke(this, EventArgs.Empty);
        }
    }
}
