using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class HealthSystem
{
    public event EventHandler OnHealthChanged;
    public event EventHandler OnHealthMaxChanged;
    public event EventHandler OnDamaged;
    public event EventHandler OnHealed;
    public event EventHandler OnDead;

    private int _health;
    private int _healthMax;

    public int HealthMax { get => _healthMax; set => _healthMax = value; }
    public int Health { get => _health; set => _health = value; }

    public HealthSystem(int healhtMax)
    {
        HealthMax = healhtMax;
        Health = healhtMax;
    }

    public float GetHealthNormalized()
    {
        return (float)Health / HealthMax;
    }

    public void Damage(int damageAmount)
    {
        Health -= damageAmount;

        //on health less than 0 just making it zero
        //and calling onDead envent
        if (Health < 0)
        {
            Health = 0;
            OnDead?.Invoke(this, EventArgs.Empty);
        }

        //Calling health changed event
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnDamaged?.Invoke(this, EventArgs.Empty);
    }

    public void Heal(int healAmount)
    {
        Health += healAmount;

        if (Health > HealthMax)
        {
            Health = HealthMax;
        }

        //Calling health changed event
        OnHealthChanged?.Invoke(this, EventArgs.Empty);
        OnHealed?.Invoke(this, EventArgs.Empty);
    }
}
