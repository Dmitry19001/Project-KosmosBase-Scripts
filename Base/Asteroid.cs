using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(HealthSystem))]
public class Asteroid : PhysicalObject, IDamageable
{
    [SerializeField] private GameObject[] _droppables;
    private HealthSystem _healthSystem;
    //private ItemType _priorityResource;
    private bool _isBig;


    private void Awake()
    {
        HealthSystem = GetComponent<HealthSystem>();
    }

    private void Start()
    {
        HealthSystem.Reset();
        HealthSystem.OnDead += HpSystem_OnDead;
    }

    private void HpSystem_OnDead(object sender, EventArgs e)
    {
        DestroySelf();
    }

    public GameObject[] Droppables
    {
        get { return _droppables; }
        set { _droppables = value; }
    }

    public bool IsBig { get => _isBig; }
    public HealthSystem HealthSystem { get => _healthSystem; private set => _healthSystem = value; }

    public override void DestroySelf()
    {
        //Debug.Log("[Asteroid] Should be destroyed");
        Drop();
        Core.Explode(gameObject, 2f);
    }

    public void Drop()
    {
        if (Droppables != null)
        {
            for (int i = 0; i < Droppables.Length; i++)
            {
                var drop = Droppables[i];
                drop.transform.SetParent(null);
            }
        }
    }

    public void Damage(int damageAmount)
    {
        HealthSystem.Damage(damageAmount);
    }
}
