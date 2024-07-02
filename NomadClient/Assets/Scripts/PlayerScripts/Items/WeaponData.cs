using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeaponData : ItemData
{
    public float durability;
    public float cooldown {  get; private set; }
    public float leftCooldown;
    public float damage { get; private set; }


    public WeaponData(string name, float weight, ItemType type, float durability, float cooldown, float leftCooldown) : base(name, weight, type)
    {
        this.durability = durability;
        this.cooldown = cooldown;
        this.leftCooldown = leftCooldown;
    }

    public virtual void LightAttack()
    {

    }
}
