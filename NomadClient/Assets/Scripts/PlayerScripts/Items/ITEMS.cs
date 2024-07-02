using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ITEMS
{
    //fill items in here like this
    public static ItemData DEFAULT_ITEM { get { return new ItemData("default_item", 1f, ItemType.Material); } }
    public static WeaponData SIMPLE_SWORD { get { return new WeaponData("simple_sword", 1f, ItemType.Weapon, 1f, 1f, 5f); } }
    public static WeaponData FIST { get { return new WeaponData("fist", 0f, ItemType.Weapon, - 1f, 1f, 5f); } }

    public static ItemData GetItemFromName(string name)
    {
        switch (name)
        {
            case "simple_sword": return SIMPLE_SWORD;
            case "fist": return FIST;
            default: return DEFAULT_ITEM;
        }
    }
}
