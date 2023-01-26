using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance;

    //our inventory items.
    public List<AllItems> _inventoryItems = new List<AllItems>();

    private void Awake()

        {
        instance = this;
        }

    //Add items to inventory.
    public void AddItem(AllItems item)
    {
        if (!_inventoryItems.Contains(item))
        {
            _inventoryItems.Add(item);
        }
    }
    
    //Remove items from inventory.
    public void RemoveItem(AllItems item)
    {
        if (_inventoryItems.Contains(item))
        {
            _inventoryItems.Remove(item);
        }
    }
    //this shows all items that we want the player to store.
    public enum AllItems
    {
        KeyRed,
        KeyBlue,
        KeyGreen,
    }
}
