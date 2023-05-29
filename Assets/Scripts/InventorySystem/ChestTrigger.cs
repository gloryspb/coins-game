using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    // public InventoryRenderer inventoryRenderer;
    public ItemStorage itemStorage;
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            if (Input.GetKey(KeyCode.E))
            {
                InventoryRenderer.Instance.OpenInventory(itemStorage, true);
            }
        }
    }
}

