using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Engine")]
public class EngineUpgrade : ShipItemUpgrade
{
    [SerializeField] private Engine engine;
    public override object GetUpgrade()
    {
        return engine;
    }
}
