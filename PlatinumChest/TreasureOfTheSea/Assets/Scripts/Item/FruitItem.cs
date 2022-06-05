using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Create New Fruit Item")]
public class FruitItem : Item
{
    [Header("HP")]
    [SerializeField] int HPAmount;
    [SerializeField] bool restoreMaxHP;

}
