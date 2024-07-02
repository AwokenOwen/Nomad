using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryData
{
    public Dictionary<ItemData, int> materials;
    public List<MaterialDictEntry> savedMaterials;
    public List<WeaponData> weapons;


    public float totalWeight;

    public InventoryData() 
    {
        materials = new Dictionary<ItemData, int>();
        totalWeight = 0f;
    }

    public void SaveItems()
    {
        savedMaterials = new List<MaterialDictEntry>();

        foreach (var item in materials)
        {
            savedMaterials.Add(new MaterialDictEntry(item.Key, item.Value));
        }
    }

    public void LoadItems()
    {
        materials = new Dictionary<ItemData, int>();

        foreach (var item in savedMaterials)
        {
            materials.Add(ITEMS.GetItemFromName(item.data.name), item.count);
        }
    }

    public void AddItem(ItemData data, int count)
    {
        switch (data.type)
        {
            case ItemType.Weapon:
                weapons.Add((WeaponData)data);
                break;
            case ItemType.Armor:
                break;
            case ItemType.Material:
                if (materials.ContainsKey(data))
                {
                    materials[data] += count;
                }
                else
                {
                    materials.Add(data, count);
                }
                break;
        }
        calculateTotalWeight();
    }

    public void RemoveItem(ItemData data, int count)
    {
        switch (data.type)
        {
            case ItemType.Weapon:
                if (weapons.Contains((WeaponData)data))
                {
                    weapons.Remove((WeaponData)data);
                    calculateTotalWeight();
                }
                break;
            case ItemType.Armor:
                break;
            case ItemType.Material:
                if (materials.ContainsKey(data))
                {
                    if (materials[data] >= count)
                    {
                        materials[data] -= count;
                        if (materials[data] == 0)
                        {
                            materials.Remove(data);
                        }
                        calculateTotalWeight();
                    }
                }
                break;
            default: break;
        }
    }

    public void ClearAllItems()
    {
        materials.Clear();
        totalWeight = 0f;
    }

    void calculateTotalWeight()
    {
        totalWeight = 0f;
        
        foreach (var key in materials.Keys) 
        {
            totalWeight += key.weight * materials[key];
        }
        foreach (WeaponData weapon in weapons)
        {
            totalWeight += weapon.weight;
        }
    }
}

[System.Serializable]
public class MaterialDictEntry
{
    public ItemData data;
    public int count;

    public MaterialDictEntry(ItemData data, int count)
    {
        this.data = data;
        this.count = count;
    }
}