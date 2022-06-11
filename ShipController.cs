using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(SpaceShip))]

public class ShipController : MonoBehaviour
{
    [Header("Player Behavior Settings")]
    [SerializeField] private float collisionDamageModifier = 0.25f;
    [SerializeField] private float speedModifier = 200;
    [SerializeField] private float stopModifier = 0.1f;
    [SerializeField] private float mouseSensitivity = 0.25f;
    [SerializeField] private float shotsPerSecond = 20f;

    //[Header("UI Components")]
    //[SerializeField] TMP_Text debugText;

    [Header("Prefabs")]
    [SerializeField] GameObject laserBullet;

    [Header("Materials")]
    [SerializeField] List<Material> _engineMaterials;

    [HideInInspector] public Transform LookOffset;
    public SpaceShip SShip;

    private Rigidbody _rb;
    private PlayerInput _input;
    private Transform[] _weapons;

    // Start is called before the first frame update
    void Start()
    {
        SShip = GetComponent<SpaceShip>();
        _rb = GetComponent<Rigidbody>();

        GetWeapons();
    }

    private void Awake()
    {
        LookOffset = transform.Find("LookOffset");

        _input = new PlayerInput();

        _input.Player.Shoot.performed += Shoot_performed;
    }

    private void GetWeapons()
    {
        Transform weaponsContainer = transform.Find("Weapons");

        _weapons = new Transform[weaponsContainer.childCount];

        for (int i = 0; i < weaponsContainer.childCount; i++)
        {
            _weapons[i] = weaponsContainer.GetChild(i);
        }
    }

    private void Shoot_performed(InputAction.CallbackContext context)
    {
        StartCoroutine(ShootCoroutine());
        //Debug.Log("Shoot performed");
    }

    private void OnEnable()
    {
        _input.Player.Enable();
    }

    private void OnDisable()
    {
        _input.Player.Disable();
    }

    private void Update()
    {
    }

    private void FixedUpdate()
    {
        Vector2 move_direction = _input.Player.Move.ReadValue<Vector2>();
        Vector2 move_direction_y = _input.Player.UpDown.ReadValue<Vector2>();      
        Vector2 look_direction = _input.Player.Look.ReadValue<Vector2>();

        Vector3 roll_direction = _input.Player.Roll.ReadValue<Vector3>();
        Vector3 rotate_direction = new(look_direction.y * -1 * mouseSensitivity, look_direction.x * mouseSensitivity, roll_direction.x * -1);

        Move(move_direction, move_direction_y);
        Rotate(rotate_direction);
        
        SShip.SetSpeed(_rb.velocity.magnitude);
        SShip.Position = transform.position;     
    }

    public void Shoot()
    {
        if (SShip.EnergySystem.Energy > 0)
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                var weapon = _weapons[i];
                GameObject go = Instantiate(laserBullet, weapon.position, weapon.rotation);
                go.GetComponent<LaserBulletBehavior>().Owner = gameObject;

                SShip.Discharge(1);
            }
        }
    }

    private IEnumerator ShootCoroutine()
    {
        for ( ; ; )
        {
            if (_input.Player.Shoot.ReadValue<float>() == 1f)
            {
                Shoot();
            }
            else
            {
                StopCoroutine(ShootCoroutine());
            }
            yield return new WaitForSeconds(1f / shotsPerSecond * _weapons.Length);
        }
    }

    public void Move(Vector2 direction, Vector2 directionY)
    {
        Vector3 force = new(direction.x * speedModifier, directionY.y * speedModifier, direction.y * speedModifier);

        _rb.AddRelativeForce(force);

        if (direction.magnitude == 0 && directionY.magnitude == 0)
        {
            ForceStop();
        }
    }

    private void ForceStop()
    {
        if (_rb.velocity.magnitude > 0)
        {
            float x_velocity = ApproximateToZero(_rb.velocity.x, stopModifier);
            float y_velocity = ApproximateToZero(_rb.velocity.y, stopModifier);
            float z_velocity = ApproximateToZero(_rb.velocity.z, stopModifier);

            _rb.velocity = new Vector3(x_velocity, y_velocity, z_velocity);
        }

        if (_rb.angularVelocity.magnitude > 0)
        {
            float x_velocity = ApproximateToZero(_rb.angularVelocity.x, stopModifier);
            float y_velocity = ApproximateToZero(_rb.angularVelocity.y, stopModifier);
            float z_velocity = ApproximateToZero(_rb.angularVelocity.z, stopModifier);

            _rb.angularVelocity = new Vector3(x_velocity, y_velocity, z_velocity);
        }
    }

    private float ApproximateToZero(
        float input,
        float stepModifier,
        float threshold = 0.01f)
    {
        float output = 0f;

        if (Mathf.Abs(input) > threshold && Mathf.Abs(input) > stepModifier)
        {
            //if input is bigger than 0 - decreasing by stepModifier, otherwise increasing
            output = input > 0 ? (input - stepModifier) : (input + stepModifier);

            return output;
        }
        else
        {
            return output;
        }      
    }

    public void Rotate(Vector3 direction)
    {
        transform.Rotate(direction);
    }

    private void OnCollisionEnter(Collision collision)
    {
        int damage = Convert.ToInt16(SShip.Speed * collisionDamageModifier);
        SShip.Damage(damage);


        Debug.Log($"Damage taken: {damage} and HP remains: {SShip.HealthSystem.Health}");
    }

    private void OnTriggerEnter(Collider other)
    {    
        if (other.TryGetComponent<ICollectable>(out var collectable))
        {
            collectable.Pickup(gameObject);
        }
    }

    private void EngineGlow(float glowMode)
    {
        //TODO: engine smooth glow effect
    }
}
