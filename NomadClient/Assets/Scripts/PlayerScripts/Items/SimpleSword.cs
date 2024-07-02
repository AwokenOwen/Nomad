using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleSword : WeaponData
{
    public SimpleSword(string name, float weight, ItemType type, float durability, float cooldown, float damage) : base(name, weight, type, durability, cooldown, damage)
    {
    }

    public override void LightAttack()
    {
        
    }
}
