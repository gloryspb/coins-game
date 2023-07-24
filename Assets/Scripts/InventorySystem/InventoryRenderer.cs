using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryRenderer : MonoBehaviour
{
    public List<ItemInventory> items = new List<ItemInventory>();
    public static bool inventoryIsOpen;
    public static InventoryRenderer Inventory;
    [SerializeField] private ItemsDatabase data;
    [SerializeField] private GameObject gameObjShow;
    [SerializeField] private GameObject InventoryMainObject;
    [SerializeField] private GameObject InventorySecondObject;
    [SerializeField] private int maxCount;
    [SerializeField] private Camera _cam;
    [SerializeField] private EventSystem es;
    private int _currentID = -1;
    private ItemInventory _currentItem;
    [SerializeField] private RectTransform _movingObject;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private GameObject _inventoryBackground;
    [SerializeField] private GameObject _chestBackground;
    private ItemStorage _currentStorage;
    [SerializeField] private ItemStorage _playerStorage;
    [SerializeField] private Text _volumeText;
    private float _currentStorageMaxWeight;
    private float _currentWeight;
    private bool _storageIsFull;
    private bool _isSingleSelection;
	[SerializeField] private FastSlotController fs;
    [SerializeField] private ItemUsageHandler _itemUsageHandler;
    [SerializeField] private GlobalMapController _globalMapController; 
    private float _squareSize = 100f;
    private Rect _squareRect; 

    public void Start()
    {
        Inventory = this;
        inventoryIsOpen = false;
        _currentStorageMaxWeight = 36f;

        if (items.Count == 0)
        {
            AddGraphics();
        }
        
        float squareX = (Screen.width - _squareSize) / 2f;
        float squareY = (Screen.height - _squareSize) / 2f;
        _squareRect = new Rect(squareX, squareY, _squareSize, _squareSize);
    }

    public void Update()
    {
        if (_currentID != -1)
        {
            MoveObject();
        }

        _currentWeight = 0;
        for (int i = 36; i < 72; i++)
        {
            _currentWeight += data.items[items[i].id].weight * items[i].count;
        }
        _volumeText.text = "Volume: " + _currentWeight.ToString() + "/" + _currentStorageMaxWeight.ToString();
        
        if (_currentItem != null && es.currentSelectedGameObject != null && inventoryIsOpen)
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

            if ((data.items[_currentItem.id].weight * _currentItem.count + _currentWeight > _currentStorageMaxWeight
            && _currentID != -1 && int.Parse(es.currentSelectedGameObject.name) > 35)
            || (_currentID > 35 && data.items[II.id].weight * II.count > _currentStorageMaxWeight - _currentWeight 
                                && int.Parse(es.currentSelectedGameObject.name) < 36))
            {
                _storageIsFull = true;
            }
            else
            {
                _storageIsFull = false;
            }
        }

        if (_currentItem != null)
        {
            if (Input.GetKeyDown(KeyCode.E)) // применение предмета на клавишу E
            {
                UseItem();
            }
            if (Input.GetMouseButtonDown(0) && _squareRect.Contains(Input.mousePosition))
            {
                UseItem(); // применение предмета при нажатии на персонажа
            }
        }
    }

    public void UseItem()
    {
        if (!data.items[_currentItem.id].isUsable) return;

        _itemUsageHandler.UseItem(_currentItem.id);

        _currentItem.count--;
        _movingObject.GetComponentInChildren<Text>().text = _currentItem.count > 1 ? _currentItem.count.ToString() : "";
        
        if (_currentItem.count < 1)
        {
            _movingObject.gameObject.SetActive(false);  // можно будет потом вынести в отдельный метод
            _currentItem.id = 0;
            _currentID = -1;
        }
        
        // также сделать какую-нить анимацию при использованиии
    }

    public void OpenInventory(ItemStorage itemStorage, bool isSecondInventory)
    {
        _globalMapController.HideMap();
        List<ItemInventory> newItems = itemStorage.GetItems();
        List<ItemInventory> playerItems = PlayerStorage.instance.GetItems();
        int length = items.Count / 2;
        int startIndex;

        if (isSecondInventory)
        {
            startIndex = items.Count - length;

            for (int i = startIndex; i < length + startIndex; i++)
            {
                items[i].id = newItems[i - startIndex].id;
                items[i].count = newItems[i - startIndex].count;
            }

            _currentStorage = itemStorage;
            _currentStorageMaxWeight = itemStorage.maxWeight;
            _volumeText.text = "Volume: " + _currentWeight.ToString() + "/" + _currentStorageMaxWeight.ToString();
        }
        else
        {
            _currentStorage = null;
        }

        for (int i = 0; i < 36; i++)
        {
            items[i].id = playerItems[i].id;
            items[i].count = playerItems[i].count;
        }

        _inventoryBackground.SetActive(!_inventoryBackground.activeSelf);
        _chestBackground.SetActive(isSecondInventory);

        if (_inventoryBackground.activeSelf)
        {
            UpdateInventory();
        }

        Time.timeScale = _inventoryBackground.activeSelf ? 0f : 1f;
        inventoryIsOpen = true;
    }

    public void CloseInventory()
    {
        if (_currentID != -1)
        {
            if (_isSingleSelection)
            {
                _currentItem.count += items[_currentID].count;
                _isSingleSelection = false;
            }
            AddInventoryItem(_currentID, _currentItem);
            _currentID = -1;
            _movingObject.gameObject.SetActive(false);
            _currentItem.count = 0;
            _currentItem.id = 0;
        }

        List<ItemInventory> newItems = new List<ItemInventory>();
        List<ItemInventory> playerItems = new List<ItemInventory>();
        for (int i = 0; i < 36; i++)
        {
            ItemInventory newItem = new ItemInventory();
            newItem.id = items[i + 36].id;
            newItem.count = items[i + 36].count;
            newItems.Add(newItem);
        }
        for (int i = 0; i < 36; i++)
        {
            ItemInventory newItem = new ItemInventory();
            newItem.id = items[i].id;
            newItem.count = items[i].count;
            playerItems.Add(newItem);
        }
        if (_currentStorage != null)
        {
            _currentStorage.SetItems(newItems);
            _currentStorageMaxWeight = 36f;
        }
        PlayerStorage.instance.SetItems(playerItems);

        _inventoryBackground.SetActive(!_inventoryBackground.activeSelf);

        if (_inventoryBackground.activeSelf)
        {
            UpdateInventory();
        }
        else
        {
            _chestBackground.SetActive(false);
        }

        Time.timeScale = _inventoryBackground.activeSelf ? 0f : 1f;

        inventoryIsOpen = false;
    }

    private void SearchForSameItem(int id, int count)
    {
        // ищем наш предмет в инвентаре, чтобы новые предметы адекватно добавлялись
        Item item = data.items[id];
        for (int i = 0; i < maxCount / 2; i++)
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
                        i = maxCount;
                    }
                }
            }
        }
        // добавляем предмет в ячейку
        if (count > 0)
        {
            for (int i = 0; i < maxCount / 2; i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i = maxCount;
                }
            }
        }
    }

    // добавляем предмет класса item в инвентарь
    private void AddItem(int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObj.GetComponent<Image>().sprite = item.img;

        if (count > 1 && item.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    // добавляем предмет класса iteminventory в инвентарь
    private void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].img;

        if (invItem.count > 1 && invItem.id != 0)
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
        }
    }

    // добавляем префабы кнопок в инвентарь при старте
    private void AddGraphics()
    {
        for (int i = 0; i < maxCount; i++)
        {
            GameObject newItem;

            if (i < maxCount / 2)
            {
                newItem = Instantiate(gameObjShow, InventoryMainObject.transform) as GameObject;
            }
            else
            {
                newItem = Instantiate(gameObjShow, InventorySecondObject.transform) as GameObject;
            }

            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;
            RectTransform rt = newItem.GetComponent<RectTransform>();
            rt.localPosition = new Vector3(0, 0, 0);
            rt.localScale = new Vector3(1, 1, 1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });

            items.Add(ii);
        }
    }

    // обновляем текст количества у предметов в инвентаре и картинку
    private void UpdateInventory()
    {
        for (int i = 0; i < maxCount; i++)
        {
            if (items[i].id != 0 && items[i].count > 1)
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
            }

            items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].id].img;
        }
    }

    // при нажатии на предмет в инвентаре
    private void SelectObject()
    {
        if (_storageIsFull)
        {
            return;
        }

        if (_currentID == -1 && items[int.Parse(es.currentSelectedGameObject.name)].id == 0)
        {
            return;
        }
        // если предмет не выбран, мы его выбираем
        if (_currentID == -1 && !Input.GetKey(KeyCode.LeftControl))
        {
            _currentID = int.Parse(es.currentSelectedGameObject.name);
            _currentItem = CopyInventoryItem(items[_currentID]);
            _movingObject.gameObject.SetActive(true);
            _movingObject.GetComponent<Image>().sprite = data.items[_currentItem.id].img;

            // заменяем наш выбранный объект на пустую ячейку
            AddItem(_currentID, data.items[0], 0);
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];
            if (II.count < 1)
            {
                return;
            }

            if (_currentItem.id != II.id && _currentItem.id != 0)
            {
                return;
            }

            if (_currentItem.count >= 64)
            {
                return;
            }

            _currentID = int.Parse(es.currentSelectedGameObject.name);
            if (_currentItem.id == 0)
            {
                _currentItem = CopyInventoryItem(items[_currentID]);
                _currentItem.count = 1;
            }
            else
            {
                _currentItem.count++;
            }

            II.count--;
            if (II.count > 1)
            {
                II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
            }
            else
            {
                II.itemGameObj.GetComponentInChildren<Text>().text = "";
            }

            _movingObject.gameObject.SetActive(true);
            _movingObject.GetComponent<Image>().sprite = data.items[_currentItem.id].img;

            if (II.count == 0)
            {
                AddItem(_currentID, data.items[0], 0);
            }
            _isSingleSelection = true;
        }
        else
        {
            // если предмет выбран и мы хотим сложить его в другой слот
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

            if (_currentItem.id != II.id)
            {
                // если предметы разные, меняем местами
                if (!_isSingleSelection)
                {
                    AddInventoryItem(_currentID, II);
                    AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), _currentItem);
                }
                else
                {
                    if (II.id != 0)
                    {
                        _currentItem.count += items[_currentID].count;

                        AddInventoryItem(_currentID, II);
                        AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), _currentItem);
                    }
                    else
                    {
                        AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), _currentItem);
                    }
                }

            }
            else
            {
                // если предметы одинаковые, то складываем их в одну ячейку (до 64)
                if (II.count + _currentItem.count <= 64)
                {
                    II.count += _currentItem.count;
                }
                else
                {
                    AddItem(_currentID, data.items[II.id], II.count + _currentItem.count - 64);
                    II.count = 64;
                }

                II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
            }
            _currentID = -1;

            _movingObject.gameObject.SetActive(false);
            _currentItem.count = 0;
            _currentItem.id = 0;
            _isSingleSelection = false;
        }

        //
        if (_currentItem.count != 0 && _currentItem.count > 1)
        {
            _movingObject.GetComponentInChildren<Text>().text = _currentItem.count.ToString();
        }
        else
        {
            _movingObject.GetComponentInChildren<Text>().text = "";
        }
    }

    // двигаем нашу картинку с предметом
    private void MoveObject()
    {
        Vector3 pos = Input.mousePosition + _offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z + 15f;
        _movingObject.position = _cam.ScreenToWorldPoint(pos);
    }

    private ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();
        New.id = old.id;
        New.itemGameObj = old.itemGameObj;
        New.count = old.count;
        return New;
    }
}