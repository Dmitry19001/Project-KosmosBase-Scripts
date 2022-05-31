using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicalObject : IDamageable
{
    private HealthSystem _healthSystem;

    private float _mass;
    private float _baseMass;

    private string _name;
    private string _description;

    private GameObject _model;

    public PhysicalObject(
        string name = "Physical Object",
        string description = "Object that has hp and mass!",
        int maxHealth = 300,
        float baseMass = 10f,
        GameObject model = null
        )
    {
        Name = name;
        Description = description;
        HealthSystem = new(maxHealth);
        BaseMass = baseMass;
        Model = model;
    }

    public string Name
    {
        get => _name;
        set
        {
            if (value != null && value.Trim().Length > 0)
                _name = value;
            else
                _name = "Physical Basic Object";
        }
    }

    public string Description
    {
        get => _description;
        set
        {
            if (value != null && value.Trim().Length > 0)
                _description = value;
            else
                _description = "Abstract class that is used in other classes!";
        }
    }

    public int Health
    {
        //Health setting is going to be through ChangeHeath method
        get =>  HealthSystem.Health;
    }

    public int MaxHealth
    {
        get => HealthSystem.HealthMax;
    }

    public float Mass
    {
        get => _mass;
        set => _mass = value;
    }

    public float BaseMass
    {
        get => _baseMass;
        set => _baseMass = value > 0f ? value : 10f;
    }

    public GameObject Model
    {
        get => _model;
        set => _model = value;
    }
    public HealthSystem HealthSystem { get => _healthSystem; set => _healthSystem = value; }

    public void Damage(int damageAmount)
    {
        HealthSystem.ChangeHealth(damageAmount);
    }
}
