using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public Dictionary<ItemData, int> items;
    public List<ItemDictEntry> savedItems;
    public float totalWeight;

    public InventoryData() 
    {
        items = new Dictionary<ItemData, int>();
        totalWeight = 0f;
    }

    public void SaveItems()
    {
        savedItems = new List<ItemDictEntry>();

        foreach (var item in items)
        {
            savedItems.Add(new ItemDictEntry(item.Key, item.Value));
        }
    }

    public void LoadItems()
    {
        items = new Dictionary<ItemData, int>();

        foreach (var item in savedItems)
        {
            items.Add(item.data, item.count);
        }
    }

    public void AddItem(ItemData data, int count)
    {
        if (items.ContainsKey(data))
        {
            items[data] += count;
        }
        else
        {
            items.Add(data, count);
        }
        calculateTotalWeight();
    }

    public void RemoveItem(ItemData data, int count)
    {
        if (items.ContainsKey(data))
        {
            if (items[data] >= count)
            {
                items[data] -= count;
                if (items[data] == 0)
                {
                    items.Remove(data);
                }
                calculateTotalWeight();
            }
        }
    }

    public void ClearAllItems()
    {
        items.Clear();
        totalWeight = 0f;
    }

    void calculateTotalWeight()
    {
        totalWeight = 0f;
        foreach (var key in items.Keys) 
        {
            totalWeight += key.weight * items[key];
        }
    }
}

[System.Serializable]
public class ItemDictEntry
{
    public ItemData data;
    public int count;

    public ItemDictEntry(ItemData data, int count)
    {
        this.data = data;
        this.count = count;
    }
}