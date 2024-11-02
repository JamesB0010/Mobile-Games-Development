using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Engine")]
public class EngineUpgrade : ShipItemUpgrade
{
    [SerializeField] private EngineUpgrade engine;
    public override object GetUpgrade()
    {
        return engine;
    }
}
