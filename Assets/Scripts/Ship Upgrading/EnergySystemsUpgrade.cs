using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Energy System")]
public class EnergySystemsUpgrade : ShipItemUpgrade
{
    [SerializeField] private EnergySystem energySystem;
    public EnergySystem EnergySystem => this.energySystem;

    public override object GetUpgrade()
    {
        return this.energySystem;
    }
    
}
