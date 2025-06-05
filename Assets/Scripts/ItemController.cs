using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemAmount;
    private Item invItem;

    public void Initialize(Item givenItem)
    {
        invItem = givenItem;
        LoadUI();
    }

    private void LoadUI()
    {
        itemImage.sprite = invItem.baseItem.itemImage;
        itemAmount.text = invItem.itemAmount.ToString();
    }
}
