using UnityEngine;
using TMPro;

public class PickupController : MonoBehaviour
{
    [SerializeField] private TMP_Text itemLabel;
    [SerializeField] private TMP_Text itemAmount;
    private string itemName;

    public void Initialize(string name, string label, int amount)
    {
        itemName = name;
        itemLabel.text = label;
        itemAmount.text = amount.ToString();
    }
}
