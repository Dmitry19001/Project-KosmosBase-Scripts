using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System;

[RequireComponent(typeof(Rigidbody))]

public class ShipController : MonoBehaviour
{
    [Header("Main class")]
    [SerializeField] private PlayerStats playerStats;

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

    [HideInInspector] public Transform LookOffset;
    public SpaceShip SShip;

    private Rigidbody _rb;
    private PlayerInput _input;
    private Transform[] _weapons;

    // Start is called before the first frame update
    void Start()
    {
        SShip = new();
        playerStats = new();
        refreshStats();

        _rb = GetComponent<Rigidbody>();

        GetWeapons();
    }

    private void refreshStats()
    {
        playerStats.Name = SShip.Name;
        playerStats.Description = SShip.Description;
        playerStats.BaseMass = SShip.BaseMass;
        playerStats.Mass = SShip.Mass;
        playerStats.ShipModel = SShip.Model;
        playerStats.Health = SShip.Health;
        playerStats.Speed = SShip.Speed;
        playerStats.EnginePower = SShip.EnginePower;
        playerStats.MaxHealth = SShip.MaxHealth;
        playerStats.MaxSpeed = SShip.MaxSpeed;
        playerStats.Energy = SShip.Energy;
        playerStats.MaxEnergy = SShip.MaxEnergy;
        playerStats.Inventory = SShip.Inventory;
        playerStats.InventorySize = SShip.InventorySize;
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

    //private void OnDrawGizmos()
    //{
    //    Debug.DrawRay(transform.position, transform.forward * 10, Color.red);
    //    Debug.DrawRay(transform.position, transform.right * 10, Color.blue);
    //    Debug.DrawRay(transform.position, transform.up * 10, Color.green);
    //}

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
        refreshStats();      
    }

    public void Shoot()
    {
        if (SShip.Energy > 0)
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                var weapon = _weapons[i];
                GameObject go = Instantiate(laserBullet, weapon.position, weapon.rotation);
                go.GetComponent<LaserBulletBehavior>().Owner = gameObject;

                SShip.ChangeEnergy(-1);
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
            forceStop();
        }
    }

    private void forceStop()
    {
        if (_rb.velocity.magnitude > 0)
        {
            float x_velocity = approximateToZero(_rb.velocity.x, stopModifier);
            float y_velocity = approximateToZero(_rb.velocity.y, stopModifier);
            float z_velocity = approximateToZero(_rb.velocity.z, stopModifier);

            _rb.velocity = new Vector3(x_velocity, y_velocity, z_velocity);
        }

        if (_rb.angularVelocity.magnitude > 0)
        {
            float x_velocity = approximateToZero(_rb.angularVelocity.x, stopModifier);
            float y_velocity = approximateToZero(_rb.angularVelocity.y, stopModifier);
            float z_velocity = approximateToZero(_rb.angularVelocity.z, stopModifier);

            _rb.angularVelocity = new Vector3(x_velocity, y_velocity, z_velocity);
        }
    }

    private float approximateToZero(float input, float stepModifier, float threshold = 0.01f)
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
        SShip.ChangeHealth(-damage);


        Debug.Log($"Damage taken: {damage} and HP remains: {SShip.Health}");
    }

    //DEBUG ONLY
    [Serializable]
    public struct PlayerStats
    {
        public int Health;
        public int MaxHealth;
        public int Energy;
        public int MaxEnergy;
        public int InventorySize;

        public float Mass;
        public float BaseMass;
        public float Speed;
        public float MaxSpeed;
        public float EnginePower;

        public string Name;
        public string Description;

        public GameObject ShipModel;

        public List<Item> Inventory;
    }
}
