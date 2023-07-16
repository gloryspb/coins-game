using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FastSlotController : MonoBehaviour
{
    private List<GameObject> slots;
    [SerializeField] private GameObject _inventory;
    
    private void Awake()
    {
        // slots = new List<GameObject>(_inventory.GetComponentsInChildren<GameObject>());
        // Debug.Log(slots);
        // GameObject[] slotsArray 
        GameObject[] slotsArray = _inventory.GetComponentsInChildren<GameObject>();
        Debug.Log(slotsArray);
        // foreach(Transform child in transform)
        // {
        //     Debug.Log(child.name);
        // }
    }
    
    
    // private void ActiveSlot()
    // {
    //     GetComponentInChildren<Image>();
    //     
    // }
}
