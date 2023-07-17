using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastSlotController : MonoBehaviour
{
    private List<GameObject> slots;
    private List<Image> imageComponents;
    [SerializeField] private GameObject _inventory;
    private Transform[] slotsArray = new Transform[6];
    private int _activeSlot = 0;
    public List<ItemInventory> items = new List<ItemInventory>();
    [SerializeField] private ItemsDatabase data;
    [SerializeField] private ItemStorage _playerStorage;
    
    private void Awake()
    {
        Transform[] childrenComponents = _inventory.GetComponentsInChildren<Transform>();
        int i = 0;
        foreach (var slot in childrenComponents)
        {
            if (!slot.CompareTag("Slot")) continue;
            slotsArray[i] = slot;
            i++;
        }
    }

    private void Start()
    {
        UpdateGraphics();
    }

    private void Update()
    {
        if (UIEventHandler.gameIsPaused || InventoryRenderer.inventoryIsOpen)
        {
            return;
        }
        UpdateGraphics();
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            UseItem();
        }
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _activeSlot = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _activeSlot = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _activeSlot = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _activeSlot = 3;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            _activeSlot = 4;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            _activeSlot = 5;
        }
        
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (_activeSlot < 5)
            {
                _activeSlot++;
            }
            else
            {
                _activeSlot = 0;
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            if (_activeSlot > 0)
            {
                _activeSlot--;
            }
            else
            {
                _activeSlot = 5;
            }
        }
        
        UpdateActiveSlot(_activeSlot);
    }
    
    
    private void UpdateActiveSlot(int activeSlot)
    {
        foreach (var slot in slotsArray)
        {
            slot.gameObject.GetComponent<Image>().color = Color.white;
        }
        
        slotsArray[activeSlot].gameObject.GetComponent<Image>().color = Color.gray;
    }

    public void OverwriteItems(List<ItemInventory> newItems)
    {
        items = newItems;
    }

    private void UpdateGraphics()
    {
        for (int i = 0; i <= 5; i++)
        {
            if (items[i].id == 0)
            {
                slotsArray[i].Find("Item").gameObject.GetComponent<Image>().enabled = false;
            }
            else
            {
                slotsArray[i].Find("Item").gameObject.GetComponent<Image>().enabled = true;
            }
            slotsArray[i].Find("Item").gameObject.GetComponent<Image>().sprite = data.items[items[i].id].img;
            slotsArray[i].Find("Count").gameObject.GetComponent<Text>().text = items[i].count > 1 ? items[i].count.ToString() : "";
            
        }
    }

    private void UseItem()
    {
        _playerStorage.UseItem(_activeSlot);
    }
}
