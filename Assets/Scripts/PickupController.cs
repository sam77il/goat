using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class PickupController : MonoBehaviour
{
    [SerializeField] private TMP_Text itemLabel;
    private List<Item> items;

    public void Initialize(List<Item> items)
    {
        this.items = new List<Item>(items);
        LoadUI();
    }

    private void LoadUI()
    {
        if (items == null || items.Count == 0)
        {
            itemLabel.text = "No items available";
            return;
        }
        itemLabel.text = "";
        foreach (var item in items)
        {
            itemLabel.text += $"{item.itemAmount}x {item.baseItem.itemLabel} ";
        }
    }

    public void ChangeItems(List<Item> newItems)
    {
        items = new List<Item>(newItems);
        LoadUI();
    }
}
