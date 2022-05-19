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
            MaxHealth = maxHealth;
            BaseMass = baseMass;
            Model = model;
            Droppables = droppables;

            ChangeHealth(MaxHealth);
    }

    public Item[] Droppables
    {
        get { return _droppables; }
        private set { _droppables = value; }
    }

    public void Drop()
    {
        if (Droppables != null)
        {
            //TODO: DROP LOGIC
        }
    }
}
