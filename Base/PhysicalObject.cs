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
    private GameObject _gameObject;

    public PhysicalObject(
        string name = "Physical Object",
        string description = "Object that has hp and mass!",
        int maxHealth = 300,
        float baseMass = 10f,
        GameObject gameObject = null,
        GameObject model = null
        )
    {
        Name = name;
        Description = description;
        HpSystem = new(maxHealth);
        BaseMass = baseMass;
        Model = model;
        GmObject = gameObject;

        //HpSystem.OnDead += HpSystem_OnDead;
    }

    //private void HpSystem_OnDead(object sender, EventArgs e)
    //{
    //    Debug.Log("[Abstract] Should be destroyed");
    //    DestroySelf();
    //}

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
        get => HpSystem.Health;
    }

    public int MaxHealth
    {
        get => HpSystem.HealthMax;
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
    public HealthSystem HpSystem { get => _healthSystem; set => _healthSystem = value; }
    public GameObject GmObject { get => _gameObject; set => _gameObject = value; }

    public void Damage(int damageAmount)
    {
        HpSystem.Damage(damageAmount);
    }

    public void Heal(int healAmount)
    {
        HpSystem.Heal(healAmount);
    }

    public abstract void DestroySelf();

}
