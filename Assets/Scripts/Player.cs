using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private string pName;
    public int pLevel { get; private set; }
    public InventoryManager inventory { get; private set; }
    public void Initialize(string playerName, int playerLevel, InventoryManager playerInventory)
    {
        pName = playerName;
        pLevel = playerLevel;
        inventory = playerInventory;
    }

    public void AddInventoryItem(Item item)
    {
        if (inventory == null) return;

        inventory.AddItem(item);
    }
}

[System.Serializable]
public class PlayerSaveData
{
    public string playerName;
    public int level;
    public List<Item> inventory;

    public PlayerSaveData(string name, int level, List<Item> inventory)
    {
        this.playerName = name;
        this.level = level;
        this.inventory = inventory;
    }
}