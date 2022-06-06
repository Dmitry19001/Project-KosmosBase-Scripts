using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SpaceShip : PhysicalObject, IDamageable, IChargeable
{
    [Header("Params")]
    [SerializeField] private EnergySystem _energySystem;

    [SerializeField] private int _startEnergy = 200;

    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed = 100f;
    [SerializeField] private float _enginePower = 100f;

    [SerializeField] private int _inventory_size = 10;

    [Header("Other")]
    public bool IsPlayer;

    [SerializeField] private List<Item> _inventory;
    

    [SerializeField] private Vector3 _position;

    private void Start()
    {
        HealthSystem.Reset();

        _energySystem = new(_startEnergy);
        Inventory = new List<Item>(_inventory_size);

        ComputeMass();

        HealthSystem.OnDead += HpSystem_OnDead;
    }

    private void HpSystem_OnDead(object sender, EventArgs e)
    {
        DestroySelf();
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

    public int InventorySize => Inventory.Count;

    public void ComputeMass()
    {
        float mass = BaseMass;

        if (Inventory != null && Inventory.Count > 0)
        {
            for (int i = 0; i < Inventory.Count; i++)
            {
                Item item = Inventory[i];
                mass += item.Mass;
                //TODO Computing mass by items weight
                //After Item class implementation
            }

            Mass = mass;
        }
        else
            Mass = mass;
    }

    public List<Item> Inventory
    {
        get => _inventory;
        //Unsafe to use, will overwrite everything in exsisting inventory
        set => _inventory = value;
    }
    public EnergySystem EnergySystem { get => _energySystem; private set => _energySystem = value; }

    public void ChangeInventorySize(int changeTo)
    {
        List<Item> oldInv = _inventory;

        _inventory = new List<Item>(changeTo);

        //TODO: DROP
        //Should drop items that doesn't fit inside after size change

        _inventory.AddRange(oldInv);
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

        //TODO: inventory item dropping 
    }

    public override void DestroySelf()
    {
        Core.Explode(gameObject, 1f, null, IsPlayer);
    }

    public void Charge(int chargeAmount)
    {
        EnergySystem.Charge(chargeAmount);
    }

    public void Discharge(int chargeAmount)
    {
        EnergySystem.Discharge(chargeAmount);
    }
}
