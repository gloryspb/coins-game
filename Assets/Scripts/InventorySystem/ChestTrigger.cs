using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestTrigger : MonoBehaviour
{
    public ItemStorage itemStorage;
    public UITooltipDisplay tooltipDisplay;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            tooltipDisplay.ShowTooltip(transform);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            tooltipDisplay.HideTooltip();
        }
    }
}

