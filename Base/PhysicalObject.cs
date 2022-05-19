using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicalObject
{
    private int _health;
    private int _maxHealth;

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
        MaxHealth = maxHealth;
        BaseMass = baseMass;
        Model = model;

        ChangeHealth(MaxHealth);
    }

    public string Name
    {
        get { return _name; }
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
        get { return _description; }
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
        get { return _health; }
        private set { _health = value; }
    }

    public int MaxHealth
    {
        get { return _maxHealth; }
        set
        {
            if (value > 0)
                _maxHealth = value;
            else
                _maxHealth = 1;
        }
    }

    public float Mass
    {
        get { return _mass; }
        set { _mass = value; }
    }

    public float BaseMass
    {
        get { return _baseMass; }
        set
        {
            if (value > 0f)
                _baseMass = value;
            else
                _baseMass = 10f;
        }
    }

    public GameObject Model
    {
        get { return _model; }
        set { _model = value; }
    }

    public void ChangeHealth(int health)
    {
        var newHealth = health + this.Health;

        if (newHealth < 0)
        {
            Health = 0;
            return;
        }

        if (newHealth >= MaxHealth)
        {
            Health = MaxHealth;
        }
        else
        {
            Health = newHealth;
        }
    }
}
