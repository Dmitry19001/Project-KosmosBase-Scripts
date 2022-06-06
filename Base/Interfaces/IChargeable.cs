using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IChargeable
{
    void Charge(int chargeAmount);

    void Discharge(int chargeAmount);
}
