using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public GameObject prefab;
    [SerializeField] private string itemName;
    [SerializeField] public Sprite icon;
    [SerializeField] private bool isInspectable;
    [SerializeField] private bool isPlaceable;
}
