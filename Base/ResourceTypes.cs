using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Linq;

public class ResourceTypes : MonoBehaviour
{
    public static ResourceTypes ResourceTypesGlobal;
    public bool AutoUpdate = true;

    public ResourceType[] ItemTypes = new ResourceType[] {
        new ResourceType("Iron", true, 0.5f, 1f, Color.grey),
        new ResourceType("Gold", true, 0.5f, 1f, Color.yellow),
        new ResourceType("Uranium", true, 0.5f, 1f, Color.green),        
        
        new ResourceType("Stone", false, 0f, 1f, Color.blue),
        new ResourceType("Scrap", false, 0f, 1f,  Color.blue),
        new ResourceType("Consumable", false, 0f, 1f, Color.blue),
        new ResourceType("Upgrade", false, 0f, 1f, Color.blue)
    };

    public ResourceType[] Ores; 


    public void FilterOres()
    {
        Ores = ( from ResourceType resourceType 
                in ItemTypes 
                where resourceType.IsOre == true 
                select resourceType )
                .ToArray();
    }


    private void Awake()
    {
        ResourceTypesGlobal = this;
        FilterOres();
    }

}

[Serializable]
public struct ResourceType
{
    public string Name;
    public bool IsOre;
    public float SpawnChance;
    public float BaseMass;
    public Color Color;

    public ResourceType(string name, bool isOre, float spawnChance, float baseMass, Color color)
    {
        Name = name;
        IsOre = isOre;
        SpawnChance = spawnChance;
        BaseMass = baseMass;
        Color = color;
    }

    public override bool Equals(object obj)
    {
        return obj is ResourceType type &&
               Name == type.Name;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, IsOre, SpawnChance, BaseMass, Color);
    }
}