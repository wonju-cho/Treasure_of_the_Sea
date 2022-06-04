using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public string displayName;
    [TextArea(4, 4)]
    public string Description;
    public Sprite icon;
    public int maxStackSize;
}
