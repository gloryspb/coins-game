using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStorage : ItemStorage
{
    public static PlayerStorage instance;
    [SerializeField] private ItemUsageHandler _itemUsageHandler;
    private void Awake()
    {
        while (items.Count < 36)
        {
            ItemInventory item = new ItemInventory();
            item.id = 0;
            item.count = 0;
            items.Add(item);
        }
        
        // if (instance == null)
        // {
        //     instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
        // else
        // {
        //     Destroy(gameObject);
        // }

        fs.OverwriteItems(items.GetRange(items.Count - 6, 6));
        
        List<ItemInventory> items1 = new List<ItemInventory>();
        items1 = items.GetRange(items.Count - 6, 6);
    }
    
    public override void SetItems(List<ItemInventory> newItems)
    {
        items = newItems;
        fs.OverwriteItems(items.GetRange(30, 6));
    }
    
    public void UseItem(int cellId)
    {
        // if (!_isPlayer) return;
        
        cellId += 30;
        
        if (!data.items[items[cellId].id].isUsable) return;
        
        items[cellId].count--;
        _itemUsageHandler.UseItem(items[cellId].id);

        if (items[cellId].count < 1)
        {
            items[cellId].id = 0;
            items[cellId].count = 0;
        }
    }
}
