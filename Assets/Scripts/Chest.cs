using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject pickupPrefab;
    [SerializeField] private List<Item> items;
    private bool isPlayerNearby = false;
    private bool isChestOpen = false;
    private PlayerInputActions inputActions;
    private Player closestPlayer;
    private PickupController pickupController;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
        inputActions.Player.Interact.performed += ctx => TryOpenChest();
        // inputActions.Player.Interact.canceled += ctx => TryOpenChest();
    }

    private void OnEnable()
    {
        inputActions.Player.Enable();
    }

    private void OnDisable()
    {
        inputActions.Player.Disable();
    }

    private void TryOpenChest()
    {
        Debug.Log("Versuche, die Truhe zu öffnen...");
        if (isPlayerNearby && !isChestOpen)
        {
            OpenChest();
        }
    }

    void OpenChest()
    {
        if (closestPlayer == null)
        {
            Debug.LogWarning("Kein Spieler in der Nähe, um die Truhe zu öffnen.");
            return;
        }
        isChestOpen = true;
        Debug.Log("Truhe geöffnet!");
        foreach (Item item in items)
        {
            closestPlayer.AddInventoryItem(item);
        }
        items.Clear();
        pickupController.ChangeItems(items);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            closestPlayer = other.transform.parent.GetComponent<Player>();
            Debug.Log(closestPlayer);
            isPlayerNearby = true;
            Canvas canvas = FindAnyObjectByType<Canvas>();
            GameObject p = Instantiate(pickupPrefab);
            p.transform.SetParent(canvas.transform, false);
            pickupController = p.GetComponent<PickupController>();
            pickupController.Initialize(items);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Destroy(pickupController.gameObject);
            isPlayerNearby = false;
            Debug.Log("Spieler hat den Bereich verlassen.");
        }
    }
}
