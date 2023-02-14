using System;
using UnityEngine;

[Serializable]
public class DropItem
{
    [SerializeField]
    private ItemSO _item;

    [SerializeField]
    private float _itemDropRate;

    public ItemSO Item         => _item;
    public float  ItemDropRate => _itemDropRate;
}