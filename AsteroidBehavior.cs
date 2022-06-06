using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Asteroid))]

public class AsteroidBehavior : MonoBehaviour
{
    public Asteroid asteroid;
    // Start is called before the first frame update
    void Start()
    {
        if (asteroid == null)
        {
            asteroid = GetComponent<Asteroid>();
        }
    }

    public void GetDamage(int damage)
    {
        asteroid.Damage(damage);
    }


    private void OnCollisionEnter(Collision collision)
    {
        GetDamage(asteroid.Health);
    }
    // Update is called once per frame
    //void Update()
    //{

    //}
}
