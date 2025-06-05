using UnityEngine;
using System.Collections.Generic;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryContainer;
    [SerializeField] private GameObject inventoryItemPrefab;
    private PlayerInputActions inputActions;
    private bool inventoryOpen = false;
    private List<Item> inventoryItems = new List<Item>();

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Inventory.performed += ctx => ToggleInventory();
    }

    public void Initialize(List<Item> items)
    {
        inventoryItems = items;
        Debug.Log("Inventory initialized with " + inventoryItems.Count + " items.");
        Debug.Log("Inventory items: " + string.Join(", ", inventoryItems.ConvertAll(item => item.baseItem.itemLabel)));
        inventoryContainer.SetActive(false);
        LoadInventoryItems();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    public void ToggleInventory()
    {
        inventoryOpen = !inventoryOpen;
        inventoryContainer.SetActive(inventoryOpen);
    }

    private void LoadInventoryItems()
    {
        GameObject itemList = inventoryContainer.transform.Find("ItemList").gameObject;
        Debug.Log(itemList);
        ClearAllChildrenForeach(itemList.transform);
        foreach (Item item in inventoryItems)
        {
            GameObject itemObject = Instantiate(inventoryItemPrefab);
            itemObject.transform.SetParent(itemList.transform, false);
            itemObject.SetActive(true);
            ItemController itemController = itemObject.GetComponent<ItemController>();
            itemController.Initialize(item);
        }
    }

    public void ClearAllChildrenForeach(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }

    public void AddItem(Item item)
    {
        if (inventoryItems == null) return;

        Item newItem = inventoryItems.Find(i => i.baseItem.itemLabel == item.baseItem.itemLabel);
        if (newItem != null)
        {
            newItem.itemAmount += item.itemAmount;
            Debug.Log("Item amount updated: " + newItem.baseItem.itemLabel + " x" + newItem.itemAmount);
            LoadInventoryItems(); // Refresh the inventory UI
            return;
        }

        inventoryItems.Add(item);
        Debug.Log("Item added: " + item.baseItem.itemLabel);
        LoadInventoryItems(); // Refresh the inventory UI
    }
}
