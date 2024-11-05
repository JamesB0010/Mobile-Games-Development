using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class ItemShopAction : ScriptableObject
{
    [FormerlySerializedAs("playerWeaponsState")] [SerializeField] protected PlayerUpgradesState playerUpgradesState;


}
