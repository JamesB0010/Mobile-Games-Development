using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Guns/Enemy/Enemy Gun")]
public class EnemyGun : Gun
{
    public override object Clone()
    {
        return ScriptableObject.Instantiate(this);
    }
}
