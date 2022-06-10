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
    [SerializeField] private float _mass;
    [SerializeField] private float _baseMass;
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

    public abstract void DestroySelf();

}
