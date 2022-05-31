using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : PhysicalObject
{
    private Item[] _droppables;
    public Asteroid(
        string name = "Asteroid",
        string description = "Unhabited and dead peace of stardust!",
        int maxHealth = 300,
        float baseMass = 100f,
        GameObject model = null,
        Item[] droppables = null
        )
    {
            Name = name;
            Description = description;
            HpSystem = new(maxHealth);
            BaseMass = baseMass;
            Model = model;
            Droppables = droppables;

        HpSystem.OnDead += HpSystem_OnDead;
    }

    private void HpSystem_OnDead(object sender, EventArgs e)
    {
        DestroySelf();
    }

    public Item[] Droppables
    {
        get { return _droppables; }
        private set { _droppables = value; }
    }

    public override void DestroySelf()
    {
        Debug.Log("[Asteroid] Should be destroyed");
        Core.Explode(GmObject, 2f);
    }

    public void Drop()
    {
        if (Droppables != null)
        {
            //TODO: DROP LOGIC
        }
    }
}
