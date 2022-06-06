using System;
using UnityEngine;

[Serializable]
public class EnergySystem
{
    public event EventHandler OnEnergyChanged;
    public event EventHandler OnEnergyMaxChanged;
    public event EventHandler OnDecreased;
    public event EventHandler OnIncreased;
    public event EventHandler OnEmpty;

    [SerializeField] private int _energy;
    [SerializeField] private int _energyMax;

    public int EnergyMax
    {
        get => _energyMax; 
        set
        {
            _energyMax = value;
            OnEnergyMaxChanged?.Invoke(this, EventArgs.Empty);
        }
    }
    public int Energy { get => _energy; set => _energy = value; }

    public EnergySystem(int energyMax)
    {
        EnergyMax = energyMax;
        Energy = energyMax;
    }

    public float GetEnergyNormalized()
    {
        return (float)Energy / EnergyMax;
    }

    public void Discharge(int damageAmount)
    {
        Energy -= damageAmount;

        //on Energy less than 0 just making it zero
        //and calling onDead envent
        if (Energy < 0)
        {
            Energy = 0;
            OnEmpty?.Invoke(this, EventArgs.Empty);
        }

        //Calling Energy changed event
        OnEnergyChanged?.Invoke(this, EventArgs.Empty);
        OnDecreased?.Invoke(this, EventArgs.Empty);
    }

    public void Charge(int healAmount)
    {
        Energy += healAmount;

        if (Energy > EnergyMax)
        {
            Energy = EnergyMax;
        }

        //Calling Energy changed event
        OnEnergyChanged?.Invoke(this, EventArgs.Empty);
        OnIncreased?.Invoke(this, EventArgs.Empty);
    }
}
