using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemStorage : MonoBehaviour
{
    public List<ItemInventory> items = new List<ItemInventory>();
    [SerializeField] private ItemsDatabase data;
    public float maxWeight;
    private int _maxCount = 72;
    [SerializeField] private FastSlotController fs;
    [SerializeField] private bool _isPlayer;
    [SerializeField] private ItemUsageHandler _itemUsageHandler;

    void Awake()
    {
        while (items.Count < 36)
        {
            ItemInventory item = new ItemInventory();
            item.id = 0;
            item.count = 0;
            items.Add(item);
        }

        if (_isPlayer)
        {
            fs.OverwriteItems(items.GetRange(items.Count - 6, 6));
        }
        List<ItemInventory> items1 = new List<ItemInventory>();
        items1 = items.GetRange(items.Count - 6, 6);
    }
    void Start()
    {
        
    }

    public void SetItems(List<ItemInventory> newItems)
    {
        items = newItems;
        if (_isPlayer)
        {
            fs.OverwriteItems(items.GetRange(30, 6)); 
        }
    }

    public List<ItemInventory> GetItems()
    {
        return items;
    }

    public void SearchForSameItem(int id, int count)
    {
        // ищем наш предмет в инвентаре, чтобы новые предметы адекватно добавлялись
        Item item = data.items[id];
        for (int i = 0; i < _maxCount / 2; i++)
        {
            if (items[i].id == item.id)
            {
                if (items[0].count < 64)
                {
                    items[i].count += count;

                    if (items[i].count > 64)
                    {
                        count = items[i].count - 64;
                        items[i].count = 64;
                    }
                    else
                    {
                        count = 0;
                        i = _maxCount / 2;
                    }
                }
            }
        }
        // добавляем предмет в ячейку
        if (count > 0)
        {
            for (int i = 0; i < _maxCount / 2; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = _maxCount / 2;
                }
            }
        }
    }

    // добавляем предмет класса item в инвентарь
    public void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
    }

    // добавляем предмет класса iteminventory в инвентарь
    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
    }

    public void UseItem(int cellId)
    {
        if (!_isPlayer) return;
        
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

[System.Serializable]

public class ItemInventory
{
    public int id;
    public GameObject itemGameObj;
    public int count;
}
