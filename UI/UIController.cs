using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public float HUD_UpdateInterval = 0.3f;

    ShipController shipController;

    //public GameObject GUI;

    public GameObject DamageCanvas;
    public GameObject HUD;
    //public GameObject DeathScreen;
    //public GameObject PauseMenu;

    //private Transform CrossFade;
    public Image crossHair;

    private TMP_Text enemyName;
    public TMP_Text debugger;
    private Slider enemyHealthProgress;

    private Slider healthBar;
    private Slider energyBar;

    // Start is called before the first frame update
    void Start()
    {
        shipController = GetComponent<ShipController>();
        healthBar = HUD.transform.Find("HealthBar").GetComponent<Slider>();
        energyBar = HUD.transform.Find("EnergyBar").GetComponent<Slider>();

        enemyHealthProgress = DamageCanvas.transform.Find("HealthBar").GetComponent<Slider>();
        enemyName = DamageCanvas.transform.Find("EnemyName").GetComponent<TMP_Text>();

        DamageCanvas.SetActive(false);
        StartCoroutine(updateHUD());
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
        Debug.DrawRay(shipController.LookOffset.position, shipController.LookOffset.TransformDirection(Vector3.forward) * 50, Color.green);
        if (Physics.Raycast(shipController.LookOffset.position, shipController.LookOffset.TransformDirection(Vector3.forward), out RaycastHit hit, 500f, layerMask))
        {
            Debug.DrawRay(shipController.LookOffset.position, shipController.LookOffset.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            CorrectCrosshair(hit.point);
            //print(hit.point);
            CheckHover(hit);
        }
        else
        {
            if (DamageCanvas.activeSelf)
                DamageCanvas.SetActive(false);
            crossHair.gameObject.SetActive(false);
        }
    }

    private void CorrectCrosshair(Vector3 point)
    {
        crossHair.gameObject.SetActive(true);
        Vector2 screenPos = Camera.main.WorldToScreenPoint(point);
        crossHair.rectTransform.position = screenPos /*- new Vector2(10,10)*/;
    }

    IEnumerator updateHUD()
    {
        for (; ; )
        {
            healthBar.value = (float)shipController.SShip.Health / shipController.SShip.MaxHealth;
            energyBar.value = (float)shipController.SShip.Energy / shipController.SShip.MaxEnergy;

            if (debugger != null)
            {
                debugger.text = $"HP: {shipController.SShip.Health}/{shipController.SShip.MaxHealth} | {(float)shipController.SShip.Health / shipController.SShip.MaxHealth}\n"
                    + $"Energy: {shipController.SShip.Energy}/{shipController.SShip.MaxEnergy}\n | {(float)shipController.SShip.Energy / shipController.SShip.MaxEnergy}"
                    + $"";
            }


            yield return new WaitForSeconds(HUD_UpdateInterval);
        }
    }

    private void CheckHover(RaycastHit hit)
    {
        if (hit.transform.CompareTag("Asteroid"))
        {
            if (!DamageCanvas.activeSelf)
                DamageCanvas.SetActive(true); 
            
            //var posMultiplier = hit.transform.localScale.y + 2;            
            //print($"cos {Mathf.Cos(Camera.main.transform.rotation.x)} sin {Mathf.Sin(Camera.main.transform.rotation.z)}");
            //DamageCanvas.transform.position = hit.transform.position + new Vector3(Mathf.Cos(Camera.main.transform.rotation.x) * posMultiplier, posMultiplier, Mathf.Sin(Camera.main.transform.rotation.z) * posMultiplier);
            //DamageCanvas.transform.rotation = Camera.main.transform.rotation;
            var asteroid = hit.transform.GetComponent<AsteroidBehavior>().asteroid;
            //HealthProgress.maxValue = asteroid.MaxHealth;
            enemyHealthProgress.value = (float)asteroid.Health / asteroid.MaxHealth;            
            enemyName.text = "Asteroid";
        }
    }
}
