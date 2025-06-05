using UnityEngine;

[System.Serializable]
public class Item
{
    public BaseItem baseItem;
    public int itemAmount;

    public Item(BaseItem baseItem, int itemAmount)
    {
        this.baseItem = baseItem;
        this.itemAmount = itemAmount;
    }

}
