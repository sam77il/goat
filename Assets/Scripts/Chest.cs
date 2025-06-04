using UnityEngine;
using UnityEngine.InputSystem;

public class Chest : MonoBehaviour
{
    private bool isPlayerNearby = false;
    private bool isChestOpen = false;
    public string itemName = "sword";
    public string itemLabel = "Sword";
    public int itemAmount = 1;
    private PlayerInputActions inputActions;
    [SerializeField] private GameObject pickupPrefab;

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
        isChestOpen = true;
        Debug.Log("Truhe geöffnet!");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            Debug.Log("pickupPrefab: " + pickupPrefab + " | Objektname: " + gameObject.name);
            Canvas canvas = FindAnyObjectByType<Canvas>();
            GameObject p = Instantiate(pickupPrefab);
            p.transform.SetParent(canvas.transform, false);
            PickupController pickupController = p.GetComponent<PickupController>();
            pickupController.Initialize(itemName, itemLabel, itemAmount);
            Debug.Log("Spieler in der Nähe – Drücke E zum Öffnen.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            Debug.Log("Spieler hat den Bereich verlassen.");
        }
    }
}
