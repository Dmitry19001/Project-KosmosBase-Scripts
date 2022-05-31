using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceBase : SpaceShip
{
    public SpaceBase(
        string name = "SBS-01",
        string description = "Your first space base and maybe only one!",
        int maxHealth = 300,
        int maxEnergy = 150,
        int maxSpeed = 100,
        int inventorySize = 10,
        float baseMass = 10,
        float enginePower = 100,
        GameObject model = null,
        List<Item> inventory = null
        ) : base(name, description, maxHealth, maxEnergy, maxSpeed, inventorySize, baseMass, enginePower, model, inventory)
    {

    }
}
