using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    // public ItemsDatabase data;
    // public List<ItemInventory> items = new List<ItemInventory>();
    // public GameObject gameObjShow;
    // public GameObject InventoryMainObject;
    // public int maxCount;
    // public Camera cam;
    // public EventSystem es;
    // public int currentID;
    // public ItemInventory currentItem;
    // public RectTransform movingObject;
    // public Vector3 offset;
    // public GameObject inventoryBackground;
    // public static InventoryManager Instance;
    // private bool isSingleSelection;
    // public GameObject ChestObject;
    // public GameObject chestBackground;

    // public void Start()
    // {
    //     Instance = this;

    //     if (items.Count == 0)
    //     {
    //         AddGraphics();
    //     }

    //     //
    //     SearchForSameItem(1, 64);
    //     SearchForSameItem(2, 64);
    //     SearchForSameItem(1, 64);
    //     SearchForSameItem(2, 64);

    //     // исправить баг при добавлении больше 64
    // }

    // public void Update()
    // {
    //     if (currentID != -1)
    //     {
    //         MoveObject();
    //     }

    //     if (Input.GetKeyDown(KeyCode.Tab) && !UIEventHandler.gameIsPaused)
    //     {
    //         inventoryBackground.SetActive(!inventoryBackground.activeSelf);

    //         if (inventoryBackground.activeSelf)
    //         {
    //             UpdateInventory();
    //         }
    //         else
    //         {
    //             chestBackground.SetActive(false);
    //         }

    //         Time.timeScale = inventoryBackground.activeSelf ? 0f : 1f;
    //     }

    //     if (inventoryBackground.activeSelf)
    //     {
    //         if (Input.GetKeyDown(KeyCode.Escape))
    //         {
    //             inventoryBackground.SetActive(false);
    //         }
    //     }
    // }

    // public void SearchForSameItem(int id, int count)
    // {
    //     // ищем наш предмет в инвентаре, чтобы новые предметы адекватно добавлялись
    //     Item item = data.items[id];
    //     for (int i = 0; i < maxCount; i++)
    //     {
    //         if (items[i].id == item.id)
    //         {
    //             if (items[0].count < 64)
    //             {
    //                 items[i].count += count;

    //                 if (items[i].count > 64)
    //                 {
    //                     count = items[i].count - 64;
    //                     items[i].count = 64;
    //                 }
    //                 else
    //                 {
    //                     count = 0;
    //                     i = maxCount;
    //                 }
    //             }
    //         }
    //     }
    //     // добавляем предмет в ячейку
    //     if (count > 0)
    //     {
    //         for (int i = 0; i < maxCount; i++)
    //         {
    //             if (items[i].id == 0)
    //             {
    //                 AddItem(i, item, count);
    //                 i = maxCount;
    //             }
    //         }
    //     }
    // }

    // // добавляем предмет в инвентарь
    // public void AddItem(int id, Item item, int count)
    // {
    //     items[id].id = item.id;
    //     items[id].count = count;
    //     items[id].itemGameObj.GetComponent<Image>().sprite = item.img;

    //     if (count > 1 && item.id != 0)
    //     {
    //         items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();
    //     }
    //     else
    //     {
    //         items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
    //     }
    // }

    // // добавляем в конкретную ячейку наш предмет (заменяем)
    // public void AddInventoryItem(int id, ItemInventory invItem)
    // {
    //     items[id].id = invItem.id;
    //     items[id].count = invItem.count;
    //     items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].img;

    //     if (invItem.count > 1 && invItem.id != 0)
    //     {
    //         items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();
    //     }
    //     else
    //     {
    //         items[id].itemGameObj.GetComponentInChildren<Text>().text = "";
    //     }
    // }

    // ////////////
    // public void OpenChest()
    // {
    //     inventoryBackground.SetActive(!inventoryBackground.activeSelf);
    //     chestBackground.SetActive(!chestBackground.activeSelf);

    //     if (inventoryBackground.activeSelf)
    //     {
    //         UpdateInventory();
    //     }

    //     Time.timeScale = inventoryBackground.activeSelf ? 0f : 1f;
    // }
    // ////////////


    // // добавляем префабы кнопок в инвентарь при старте
    // public void AddGraphics(ChestManager chestManager = null)
    // {
    //     for (int i = 0; i < maxCount; i++)
    //     {
    //         GameObject newItem = Instantiate(gameObjShow, InventoryMainObject.transform) as GameObject;

    //         newItem.name = i.ToString();

    //         ItemInventory ii = new ItemInventory();
    //         ii.itemGameObj = newItem;
    //         RectTransform rt = newItem.GetComponent<RectTransform>();
    //         rt.localPosition = new Vector3(0, 0, 0);
    //         rt.localScale = new Vector3(1, 1, 1);
    //         newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

    //         ///////////

    //         GameObject chestItem = Instantiate(gameObjShow, ChestObject.transform) as GameObject;

    //         chestItem.name = (i + 36).ToString();

    //         ItemInventory ic = new ItemInventory();
    //         ic.itemGameObj = chestItem;
    //         RectTransform rt1 = chestItem.GetComponent<RectTransform>();
    //         rt1.localPosition = new Vector3(0, 0, 0);
    //         rt1.localScale = new Vector3(1, 1, 1);
    //         chestItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

    //         ///////////

    //         Button tempButton = newItem.GetComponent<Button>();

    //         Button tempButton1 = chestItem.GetComponent<Button>();

    //         tempButton.onClick.AddListener(delegate { SelectObject(); });

    //         tempButton1.onClick.AddListener(delegate { SelectObject(); });

    //         items.Add(ii);

    //         chestManager.items.Add(ic);
    //     }
    // }

    // // обновляем текст количества у предметов в инвентаре и картинку
    // public void UpdateInventory()
    // {
    //     for (int i = 0; i < maxCount; i++)
    //     {
    //         if (items[i].id != 0 && items[i].count > 1)
    //         {
    //             items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
    //         }
    //         else
    //         {
    //             items[i].itemGameObj.GetComponentInChildren<Text>().text = "";
    //         }

    //         items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].id].img;
    //     }
    // }

    // // при нажатии на предмет в инвентаре
    // public void SelectObject()
    // {
    //     if (currentID == -1 && items[int.Parse(es.currentSelectedGameObject.name)].id == 0)
    //     {
    //         return;
    //     }
    //     // если предмет не выбран, мы его выбираем
    //     if (currentID == -1 && !Input.GetKey(KeyCode.LeftControl))
    //     {
    //         currentID = int.Parse(es.currentSelectedGameObject.name);
    //         currentItem = CopyInventoryItem(items[currentID]);
    //         movingObject.gameObject.SetActive(true);
    //         movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

    //         // заменяем наш выбранный объект на пустую ячейку
    //         AddItem(currentID, data.items[0], 0);
    //     }
    //     else if (Input.GetKey(KeyCode.LeftControl))
    //     {
    //         ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];
    //         if (II.count < 1)
    //         {
    //             return;
    //         }

    //         if (currentItem.id != II.id && currentItem.id != 0)
    //         {
    //             return;
    //         }

    //         if (currentItem.count >= 64)
    //         {
    //             return;
    //         }

    //         currentID = int.Parse(es.currentSelectedGameObject.name);
    //         if (currentItem.id == 0)
    //         {
    //             currentItem = CopyInventoryItem(items[currentID]);
    //             currentItem.count = 1;
    //         }
    //         else
    //         {
    //             currentItem.count++;
    //         }

    //         II.count--;
    //         II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();

    //         movingObject.gameObject.SetActive(true);
    //         movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

    //         if (II.count == 0)
    //         {
    //             AddItem(currentID, data.items[0], 0);
    //         }
    //         isSingleSelection = true;
    //     }
    //     else
    //     {
    //         // если предмет выбран и мы хотим сложить его в другой слот
    //         ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

    //         if (currentItem.id != II.id)
    //         {
    //             // если предметы разные, меняем местами
    //             if (!isSingleSelection)
    //             {
    //                 AddInventoryItem(currentID, II);
    //                 AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
    //             }
    //             else
    //             {
    //                 if (II.id != 0)
    //                 {
    //                     currentItem.count += items[currentID].count;

    //                     AddInventoryItem(currentID, II);
    //                     AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
    //                 }
    //                 else
    //                 {
    //                     AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
    //                 }
    //             }

    //         }
    //         else
    //         {
    //             // если предметы одинаковые, то складываем их в одну ячейку (до 64)
    //             if (II.count + currentItem.count <= 64)
    //             {
    //                 II.count += currentItem.count;
    //             }
    //             else
    //             {
    //                 AddItem(currentID, data.items[II.id], II.count + currentItem.count - 64);
    //                 II.count = 64;
    //             }

    //             II.itemGameObj.GetComponentInChildren<Text>().text = II.count.ToString();
    //         }
    //         currentID = -1;

    //         movingObject.gameObject.SetActive(false);
    //         currentItem.count = 0;
    //         currentItem.id = 0;
    //         isSingleSelection = false;
    //     }

    //     //
    //     if (currentItem.count != 0 && currentItem.count > 1)
    //     {
    //         movingObject.GetComponentInChildren<Text>().text = currentItem.count.ToString();
    //     }
    //     else
    //     {
    //         movingObject.GetComponentInChildren<Text>().text = "";
    //     }
    // }

    // // двигаем нашу картинку с предметом
    // public void MoveObject()
    // {
    //     Vector3 pos = Input.mousePosition + offset;
    //     pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z + 15f;
    //     movingObject.position = cam.ScreenToWorldPoint(pos);
    // }

    // // public ItemInventory CopyInventoryItem(ItemInventory old)
    // // {
    // //     ItemInventory New = new ItemInventory();
    // //     New.id = old.id;
    // //     New.itemGameObj = old.itemGameObj;
    // //     New.count = old.count;
    // //     return New;
    // // }
}

// [System.Serializable]

// public class ItemInventory
// {
//     public int id;
//     public GameObject itemGameObj;
//     public int count;
// }