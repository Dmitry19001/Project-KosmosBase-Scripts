using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public float HUD_UpdateInterval = 0.3f;

    ShipController _shipController;

    //public GameObject GUI;

    public GameObject DamageCanvas;
    public GameObject HUD;
    public GameObject InventoryPanel;
    //public GameObject DeathScreen;
    //public GameObject PauseMenu;

    //private Transform CrossFade;
    public Image CrossHair;

    private TMP_Text _enemyName;
    public TMP_Text debugger;
    private Slider _enemyHealthProgress;

    private Slider _healthBar;
    private Slider _energyBar;

    [SerializeField] private GameObject _inventoryContent;
    [SerializeField] private GameObject[] _inventorySlots;
    [SerializeField] private GameObject _inventroyEntryPrefab;

    InventorySystem _inventorySystem;

    // Start is called before the first frame update
    void Start()
    {
        _shipController = GetComponent<ShipController>();
        _healthBar = HUD.transform.Find("HealthBar").GetComponent<Slider>();
        _energyBar = HUD.transform.Find("EnergyBar").GetComponent<Slider>();

        _enemyHealthProgress = DamageCanvas.transform.Find("HealthBar").GetComponent<Slider>();
        _enemyName = DamageCanvas.transform.Find("EnemyName").GetComponent<TMP_Text>();

        DamageCanvas.SetActive(false);

        _inventorySystem = _shipController.SShip.InventorySystem;

        _energyBar.value = _shipController.SShip.EnergySystem.GetEnergyNormalized();
        _healthBar.value = _shipController.SShip.HealthSystem.GetHealthNormalized();

        SubscribeToEvents();

        GenerateInventorySlots();
    }

    private void SubscribeToEvents()
    {
        _inventorySystem.OnInventoryChanged += InventorySystem_OnInventoryChanged;
        _shipController.PlayerInput.Player.Inventory.performed += Inventory_performed;
        _shipController.SShip.HealthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        _shipController.SShip.HealthSystem.OnDead += HealthSystem_OnDead;
        _shipController.SShip.EnergySystem.OnEnergyChanged += EnergySystem_OnEnergyChanged;
    }
    
    private void GenerateInventorySlots()
    {
        if (_inventorySlots.Length != _inventorySystem.InventorySize)
        {
            for(int i = 0; i < _inventorySlots.Length; i++)
            {
                Destroy(_inventorySlots[i]);
            }
        }

        _inventorySlots = new GameObject[_inventorySystem.InventorySize];

        for (int i = 0; i < _inventorySystem.InventorySize; i++)
        {
            var id = i; //for some reason straight using of i messes with button listener
            var entry = Instantiate(_inventroyEntryPrefab, _inventoryContent.transform);

            entry.name = "ID" + i.ToString();

            entry.GetComponent<Button>().onClick.AddListener(() => DropItem(id));

            _inventorySlots[i] = entry;
        }
    }

    private void DropItem(int id)
    {
        if (_inventorySystem.Inventory[id] != null)
        {
            _inventorySystem.DropItem(id);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckLook();
    }

    private void CheckLook()
    {
        // Bit shift the index of the layer (8) to get a bit mask
        int layerMask = 1 << 8;

        // This would cast rays only against colliders in layer 8.
        // But instead we want to collide against everything except layer 8. The ~ operator does this, it inverts a bitmask.
        layerMask = ~layerMask;

        // Does the ray intersect any objects excluding the player layer
        Debug.DrawRay(_shipController.LookOffset.position, _shipController.LookOffset.TransformDirection(Vector3.forward) * 50, Color.green);
        if (Physics.Raycast(_shipController.LookOffset.position, _shipController.LookOffset.TransformDirection(Vector3.forward), out RaycastHit hit, 500f, layerMask))
        {
            Debug.DrawRay(_shipController.LookOffset.position, _shipController.LookOffset.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            CorrectCrosshair(hit.point);
            CheckHover(hit);
        }
        else
        {
            if (DamageCanvas.activeSelf)
                DamageCanvas.SetActive(false);
            CrossHair.gameObject.SetActive(false);
        }
    }

    private void CorrectCrosshair(Vector3 point)
    {
        CrossHair.gameObject.SetActive(true);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(point);
        CrossHair.rectTransform.position = screenPos;
    }

    private void CheckHover(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Asteroid"))
        {
            if (!DamageCanvas.activeSelf)
                DamageCanvas.SetActive(true);


            var asteroid = hit.transform.GetComponent<Asteroid>();

            _enemyHealthProgress.value = (float)asteroid.HealthSystem.Health / asteroid.HealthSystem.HealthMax;            
            _enemyName.text = "Asteroid";
        }
    }




    #region Events
    private void Inventory_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        InventoryPanel.SetActive(!InventoryPanel.activeSelf);
        _shipController.CanMove = !InventoryPanel.activeSelf;
    }

    private void EnergySystem_OnEnergyChanged(object sender, System.EventArgs e)
    {
        _energyBar.value = _shipController.SShip.EnergySystem.GetEnergyNormalized();
    }

    private void HealthSystem_OnDead(object sender, System.EventArgs e)
    {
        //Death screen here

    }

    private void HealthSystem_OnHealthChanged(object sender, System.EventArgs e)
    {
        _healthBar.value = _shipController.SShip.HealthSystem.GetHealthNormalized();
    }

    private void InventorySystem_OnInventoryChanged(object sender, System.EventArgs e)
    {

        if (_inventorySlots == null || _inventorySlots.Length != _inventorySystem.Inventory.Length)
        {
            GenerateInventorySlots();
        }

        for (int i = 0; i < _inventorySlots.Length; i++)
        {
            var slot = _inventorySlots[i];
            var item = _inventorySystem.Inventory[i];
            var color = Color.grey;

            if (item != null)
            {
                color = item.GetComponent<Item>().ItemType.Color;
            }

            slot.GetComponent<Image>().color = color;
        }
    }
    #endregion
}
