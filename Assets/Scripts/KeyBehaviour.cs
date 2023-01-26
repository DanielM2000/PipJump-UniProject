using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    [SerializeField] InventoryManager.AllItems _itemType;

    //this allows us to pick up and store items into the InventoryManager.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            InventoryManager.instance.AddItem(_itemType);
            Destroy(gameObject);
        }
    }
}

