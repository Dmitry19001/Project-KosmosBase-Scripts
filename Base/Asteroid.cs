using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : PhysicalObject
{
    [SerializeField] private GameObject[] _droppables;
    //private ItemType _priorityResource;
    private bool _isBig;
    //public Asteroid(
    //    string name = "Asteroid",
    //    string description = "Unhabited and dead peace of stardust!",
    //    int maxHealth = 300,
    //    float baseMass = 100f,
    //    GameObject gameObject = null,
    //    Item[] droppables = null
    //    ) : base(name, description, maxHealth, baseMass, gameObject)
    //{

    //    Droppables = droppables;

    //    
    //}

    private void Start()
    {
        //for (int i = 0; i < Enum.GetValues(typeof(ItemType)).Length; i++)
        //{
            
        //}

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
            //TODO: DROP LOGIC
        }
    }
}
