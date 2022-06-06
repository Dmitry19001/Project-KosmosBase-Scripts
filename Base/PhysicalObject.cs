using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PhysicalObject : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private string _name = "Physical Object";
    [SerializeField] private string _description = "Object that has hp and mass!";

    [Header("Params [BASE]")]
    [SerializeField] private HealthSystem _healthSystem;

    [SerializeField] private float _mass;
    [SerializeField] private float _baseMass;

    //[Header("Other [BASE]")]
    //[SerializeField] private GameObject _gameObject;

    //public PhysicalObject(
    //    string name = "Physical Object",
    //    string description = "Object that has hp and mass!",
    //    int maxHealth = 300,
    //    float baseMass = 10f,
    //    GameObject gameObject = null
    //    )
    //{
    //    Name = name;
    //    Description = description;
    //    HpSystem = new(maxHealth);
    //    BaseMass = baseMass;
    //    GmObject = gameObject;

    //    //HpSystem.OnDead += HpSystem_OnDead;
    //}

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
        get => HealthSystem.Health;
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

    public HealthSystem HealthSystem { get => _healthSystem; private set => _healthSystem = value; }
    //public GameObject GmObject { get => _gameObject; set => _gameObject = value; }

    public void Damage(int damageAmount)
    {
        HealthSystem.Damage(damageAmount);
    }

    public void Heal(int healAmount)
    {
        HealthSystem.Heal(healAmount);
    }

    public abstract void DestroySelf();

}
