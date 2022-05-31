using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DockBehavior : MonoBehaviour
{
    [SerializeField] private float _repairingInerval = 0.5f;
    [SerializeField] private int _energyLoadSpeed = 10;
    [SerializeField] private int _healthRegenSpeed = 10;

    [SerializeField] private GameObject[] _laserSpots;
    private SpaceShip _spaceShip;
    private bool _isDrawingLasers = false;

    private bool _isRepairing = false;

    // Start is called before the first frame update
    void Start()
    {
        var spots = transform.parent.Find("RepairLasers");
        if (spots != null)
        {
            _laserSpots = new GameObject[spots.childCount];

            for (int x = 0; x < spots.childCount; x++)
            {
                _laserSpots[x] = spots.GetChild(x).gameObject;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered dock!");
            _spaceShip = other.GetComponent<ShipController>().SShip;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (_spaceShip != null)
            {
                
                if (_spaceShip.Energy != _spaceShip.MaxEnergy || _spaceShip.Health != _spaceShip.MaxHealth)
                {
                    if (!_isRepairing)
                    {
                        _isRepairing = true;
                        StartCoroutine(RepairingRoutine());
                    }

                    DrawLaser(true);
                }
                else if (_isDrawingLasers)
                {
                    StopCoroutine(RepairingRoutine());
                    _isRepairing = false;
                    DrawLaser(false);
                }
            }
        }
    }

    IEnumerator RepairingRoutine()
    {
        for (; ; )
        {
            if (_spaceShip != null)
            {
                if (_spaceShip.Energy != _spaceShip.MaxEnergy)
                {
                    _spaceShip.ChangeEnergy(_energyLoadSpeed);
                }

                if (_spaceShip.Health != _spaceShip.MaxHealth)
                {
                    _spaceShip.Heal(_healthRegenSpeed);
                }
            }

            yield return new WaitForSeconds(_repairingInerval);
        }
    }


    private void DrawLaser(bool enable)
    {
        if (_laserSpots != null && _laserSpots.Length > 0)
        {
            for (int x = 0; x < _laserSpots.Length; x++)
            {
                var lRenderer = _laserSpots[x].transform.GetComponent<LineRenderer>();

                if (!lRenderer.enabled && enable)
                {
                    lRenderer.enabled = true;
                    _isDrawingLasers = true;
                }
                else if (!enable)
                {
                    lRenderer.enabled = false;
                    _isDrawingLasers = false;
                }

                if (_spaceShip != null)
                {
                    Debug.DrawLine(_laserSpots[x].transform.position, _spaceShip.Position, new Color(1, 0, 0));

                    lRenderer.SetPositions(new Vector3[] { _laserSpots[x].transform.position, _spaceShip.Position });
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _spaceShip = null;

            if (_isRepairing)
            {
                _isRepairing = false;
                StopCoroutine(RepairingRoutine());
            }

            DrawLaser(false);
        }
    }
}
