using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class LaserBulletBehavior : MonoBehaviour
{
    [SerializeField] private float lifeTime = 3f;
    [SerializeField] private float m_Speed = 2000f;
    [SerializeField] private int dealingDamage = 10;
    public GameObject Owner;

    private Rigidbody _rb;

    // Start is called before the first frame update
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.AddForce(transform.forward * m_Speed, ForceMode.Force);
        checkSelf();
    }

    private void checkSelf()
    {
        if (lifeTime > 0)
        {
            lifeTime -= Time.fixedDeltaTime;
        }
        else
        {
            Destroy(gameObject);
        }     
    }

    private void OnTriggerEnter(Collider collision)
    {
        var damagable = collision.GetComponent<IDamageable>();
        if (damagable != null && Owner.name != collision.name)
        {
            Destroy(gameObject);
            //Debug.Log($"Destroyed by [{collision.transform.tag}]{collision.transform.name}");
            damagable.Damage(dealingDamage);
        }
    }
}
