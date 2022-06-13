using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public string displayName;
    public Sprite icon;
    public int maxStackSize;
}
