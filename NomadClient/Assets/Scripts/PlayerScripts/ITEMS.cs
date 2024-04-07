using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ITEMS
{
    //fill items in here like this
    public static ItemData DEFAULT_ITEM { get { return new ItemData("default_item", 1f); } }
    public static ItemData SIMPLE_SWORD { get { return new ItemData("simple_sword", 1f); } }

    public static ItemData GetItemFromName(string name)
    {
        switch (name)
        {
            case "simple_sword": return SIMPLE_SWORD;
            default: return DEFAULT_ITEM;
        }
    }
}
