using System;
using UnityEngine;


public class Core : MonoBehaviour
{
    public static bool GamePaused = false;

    //public static AsteroidPrefab AsteroidsBig;
    //public static AsteroidPrefab AsteroidsSmall; 
    //public static ResourceBase[] Resources;

    public static Vector3 DockPosition;

    public static ParticleSystem defaultExplosion;

    public static void RespawnPlayer(SpaceShip spaceShip, Vector3 SpawnPosition)
    {
        //print($"Respawning to {SpawnPosition}");

        //spaceShip.ShipTransform.position = SpawnPosition;
        //spaceShip.Health = spaceShip.HealthMax;
        //spaceShip.Energy = spaceShip.EnergyMax;
        //spaceShip.ShipTransform.gameObject.SetActive(true);
    }

    public static void Explode(GameObject target, float removeTime, ParticleSystem explosionPrefab = null, bool reallyDestroy = true)
    {
        if (reallyDestroy)
        {
            Destroy(target);
        }
        else
        {
            target.SetActive(false);
        }

        if (explosionPrefab != null)
        {
            var explosion = Instantiate(explosionPrefab, target.transform.position, Quaternion.Inverse(target.transform.rotation));
            Destroy(explosion, removeTime);
        }
    }

    public static int RefreshedSeed()
    {
        var output = Convert.ToInt32(DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString());
        return output;
    }
}

