using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    public Asteroid asteroid;
    // Start is called before the first frame update
    void Start()
    {
        if (asteroid == null)
        {
            asteroid = new Asteroid();
        }
    }

    public void GetDamage(int damage)
    {
        if (asteroid.Health - damage > 0)
        {
            asteroid.ChangeHealth(-damage);
        }
        else
        {
            asteroid.ChangeHealth(-asteroid.Health);
            DestroySelf();
        }
    }

    private void DestroySelf()
    {
        asteroid.Drop();
        Destroy(gameObject);
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