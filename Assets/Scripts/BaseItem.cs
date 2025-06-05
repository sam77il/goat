using UnityEngine;

[CreateAssetMenu(fileName = "New Base Item", menuName = "BaseItem/Create Base Item")]
public class BaseItem : ScriptableObject
{
    public string itemName;
    public string itemLabel;
    public Sprite itemImage;
}
