using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpaceShip : PhysicalObject
{
    private int _energy;
    private int _maxEnergy;

    private float _speed;
    private float _maxSpeed;
    private float _enginePower;

    private List<Item> _inventory;

    public SpaceShip(
        string name = "SSHP-01",
        string description = "Your first ship and maybe only one!",
        int maxHealth = 300,
        int maxEnergy = 150,
        int maxSpeed = 100,
        int inventorySize = 10,
        float baseMass = 10f,
        float enginePower = 100f,
        GameObject shipModel = null,
        List<Item> inventory = null
        )
    {
        Name = name;
        Description = description;
        EnginePower = enginePower;
        MaxHealth = maxHealth;
        MaxEnergy = maxEnergy;
        MaxSpeed = maxSpeed;
        BaseMass = baseMass;
        Model = shipModel;

        Inventory = new List<Item>(inventorySize);
        if (inventory != null)
        {
            Inventory.AddRange(inventory);
        }

        ComputeMass();

        ChangeHealth(MaxHealth);
        ChangeEnergy(MaxEnergy);    
    }

    public int Energy
    {
        //Energy setting is going to be through ChangeEnergy method
        get { return _energy; }
        private set { _energy = value; }
    }

    public int MaxEnergy
    {
        get { return _maxEnergy; }
        set
        {
            if (value > 0)
                _maxEnergy = value;
            else
                _maxEnergy = 1;
        }
    }

    public float MaxSpeed 
    {
        get { return _maxSpeed; }
        set
        {
            if (value > 0f)
                _maxSpeed = value;
            else
                _maxSpeed = 1f;
        }
    }

    public float Speed
    {
        get { return _speed; }
        set 
        { 
            if (value >= 0 && value <= MaxSpeed)
                _speed = value; 
        }
    }
    public void SetSpeed(float speed)
    {
        Speed = speed;
    }

    public float EnginePower
    {
        get { return _enginePower; }
        set 
        { 
            if (value >= 0)
                _enginePower = value; 
        }
    }

    public int InventorySize { get { return Inventory.Count; } }

    public void ComputeMass()
    {
        var mass = base.BaseMass;

        if (Inventory != null && Inventory.Count > 0)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                var item = Inventory[i];
                //TODO Computing mass by items weight
                //After Item class implementation
            }

            base.Mass = mass;
        }
        else
            base.Mass = mass;
    }

    public List<Item> Inventory 
    {
        get { return _inventory; }
        //Unsafe to use, will overwrite everything in exsisting inventory
        set { _inventory = value; }
    }

    public void ChangeInventorySize(int changeTo)
    {

    }

    public void InventoryAdd(Item item)
    {
        //TODO: inventory item adding 
    }

    public void InventoryRemove(Item item = null, int index = -1)
    {
        //if Method called with wrong or empty params
        if (item != null && index < 0)
            return;

        //TODO: inventory item deleting 
    }

    public void ChangeEnergy(int energy)
    {
        var newEnergy = energy + _energy;

        if (newEnergy < 0)
        {
            _energy = 0;
            return;
        }

        if (newEnergy >= _maxEnergy)
        {
            _energy = _maxEnergy;
        }
        else
        {
            _energy = newEnergy;
        }
    }
}
