using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(HealthSystem))]
[RequireComponent(typeof(InventorySystem))]
public class SpaceShip : PhysicalObject, IDamageable, IChargeable
{
    [Header("Params")]
    private EnergySystem _energySystem;
    private HealthSystem _healthSystem;
    private InventorySystem _inventorySystem;

    [SerializeField] private int _startEnergy = 200;

    [SerializeField] private float _speed;
    [SerializeField] private float _maxSpeed = 100f;
    [SerializeField] private float _enginePower = 100f;

    [Header("Other")]
    public bool IsPlayer;

    [SerializeField] private Vector3 _position;

    private void Awake()
    {
        HealthSystem = GetComponent<HealthSystem>();
        InventorySystem = GetComponent<InventorySystem>();

        HealthSystem.OnDead += HealthSystem_OnDead;
        InventorySystem.OnAddToInventory += InventorySystem_OnAddToInventory;
    }

    private void InventorySystem_OnAddToInventory(object sender, EventArgs e)
    {
        Debug.Log("SpaceShip received item to inventory");
    }

    private void Start()
    {
        HealthSystem.Reset();

        _energySystem = new(_startEnergy);

        ComputeMass();
        
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
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

    public void ComputeMass()
    {
        float mass = BaseMass;

        if (InventorySystem.Inventory != null && InventorySystem.InventorySize > 0)
        {
            for (int i = 0; i < InventorySystem.InventorySize; i++)
            {
                GameObject itemGm = InventorySystem.Inventory[i];

                if (itemGm != null)
                {
                    Item item = itemGm.GetComponent<Item>();
                    mass += item.Mass;
                }

                //TODO Computing mass by items weight
                //After Item class implementation
            }

            Mass = mass;
        }
        else
            Mass = mass;
    }

    public EnergySystem EnergySystem { get => _energySystem; private set => _energySystem = value; }
    public HealthSystem HealthSystem { get => _healthSystem; private set => _healthSystem = value; }
    public InventorySystem InventorySystem { get => _inventorySystem; private set => _inventorySystem = value; }


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

    public void Damage(int damageAmount)
    {
        HealthSystem.Damage(damageAmount);
    }
}
