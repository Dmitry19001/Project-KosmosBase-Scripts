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

    private Vector3 _position;

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
        HealthSystem = new(maxHealth);
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

        ChangeEnergy(MaxEnergy);    
    }

    public int Energy
    {
        //Energy setting is going to be through ChangeEnergy method
        get => _energy;
        private set => _energy = value;
    }

    public int MaxEnergy
    {
        get => _maxEnergy;
        set => _maxEnergy = value > 0 ? value : 1;
    }

    public float MaxSpeed
    {
        get => _maxSpeed;
        set => _maxSpeed = value > 0f ? value : 1f;
    }

    public float Speed
    {
        get => _speed;
        set
        {
            if (value >= 0 && value <= MaxSpeed)
            {
                _speed = value;
            }
        }
    }
    public void SetSpeed(float speed)
    {
        Speed = speed;
    }

    public float EnginePower
    {
        get => _enginePower;
        set
        {
            if (value >= 0)
            {
                _enginePower = value;
            }
        }
    }

    public Vector3 Position
    {
        get => _position;
        set => _position = value;
    }

    public int InventorySize { get { return Inventory.Count; } }

    public void ComputeMass()
    {
        float mass = BaseMass;

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
        get => _inventory;
        //Unsafe to use, will overwrite everything in exsisting inventory
        set => _inventory = value;
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
        int newEnergy = energy + _energy;

        if (newEnergy < 0)
        {
            _energy = 0;
            return;
        }

        _energy = newEnergy >= _maxEnergy ? _maxEnergy : newEnergy;
    }
}
