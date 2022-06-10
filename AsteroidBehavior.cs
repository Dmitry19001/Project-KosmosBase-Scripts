using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Asteroid))]

public class AsteroidBehavior : MonoBehaviour
{
    public Asteroid Asteroid;

    [SerializeField] private GameObject _orePrefab;

    [SerializeField] private ResourceType _priorityResource;

    private GameObject[] _oreSpawnPoints;
    // Start is called before the first frame update
    void Start()
    {
        if (Asteroid == null)
        {
            Asteroid = GetComponent<Asteroid>();
        }

        _priorityResource = ResourceTypes.ResourceTypesGlobal.Ores[Random.Range(0, ResourceTypes.ResourceTypesGlobal.Ores.Length)];

        _oreSpawnPoints = new GameObject[transform.Find("Ores").childCount];
        //print($"Ore child count is: {transform.Find("Ores").childCount}");

        GenerateOres();
    }

    private void GenerateOres()
    {
        if (_orePrefab != null && _oreSpawnPoints?.Length > 0)
        {
            Transform ores_parent = transform.Find("Ores");

            ResourceType[] ores = ResourceTypes.ResourceTypesGlobal.Ores;
            int[] oresSpawned = new int[ores.Length];

            List<GameObject> droppables = new List<GameObject>();

            for (int i = 0; i < ResourceTypes.ResourceTypesGlobal.Ores.Length; i++)
            {
                oresSpawned[i] = 0;
            }

            for (int i = 0; i < _oreSpawnPoints.Length; i++)
            {
                _oreSpawnPoints[i] = ores_parent.GetChild(i).gameObject;

                for (int x = 0; x < oresSpawned.Length; x++)
                {
                    float priorityModifier = (ores[x].Equals(_priorityResource) ? 0.20f : 0); //Priority modifier is 20% spawn chance
                    float spawnChance = ores[x].SpawnChance + priorityModifier - 0.1f * oresSpawned[x];

                    if (spawnChance > 0f)
                    {
                        bool result = Random.Range(0f, 1f) <= spawnChance;
                        if (result)
                        {
                            GameObject loot = Instantiate(_orePrefab, _oreSpawnPoints[i].transform.position, Quaternion.identity, ores_parent);

                            loot.name = ores[x].Name + "Ore";
                            loot.GetComponent<Renderer>().material.SetColor("MainColor", ores[x].Color);

                            var itm = loot.GetComponent<Item>();
                            itm.Name = ores[x].Name;
                            itm.BaseMass = ores[x].BaseMass;

                            droppables.Add(loot);

                            oresSpawned[x]++;

                            break;
                        }
                    }
                }
            }

            Asteroid.Droppables = droppables.ToArray();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
       
    }
}
