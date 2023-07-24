using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlobalMapController : MonoBehaviour
{
    [SerializeField] private Canvas _mapCanvas;
    [SerializeField] private RawImage _mapImage;
    [SerializeField] private RawImage _mapTexture;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private GameObject _playerMark;
    public bool isMapOpen = false;
    private float _mapScale = 1f;
    private Vector3 _lastMousePosition;
    
    
    private void Start()
    {
        // Скрыть карту при запуске игры
        _mapCanvas.gameObject.SetActive(false);
        _playerMark.GetComponent<SpriteRenderer>().enabled = true;
    }

    public void HideMap()
    {
        isMapOpen = false;
        _mapCanvas.gameObject.SetActive(isMapOpen);
    }
    
    public void ShowMap()
    {
        isMapOpen = true;
        _mapCanvas.gameObject.SetActive(isMapOpen);
        UIEventHandler.gameIsPaused = isMapOpen;

        if (InventoryRenderer.inventoryIsOpen)
        {
            InventoryRenderer.Inventory.CloseInventory();
        }
        Time.timeScale = 0f;
    }

    private void Update()
    {
        // Открытие и закрытие карты при нажатии на клавишу M
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (isMapOpen)
            {
                HideMap();
                UIEventHandler.gameIsPaused = false;
                Time.timeScale = 1f;
            }
            else if (!isMapOpen && !UIEventHandler.gameIsPaused)
            {
                ShowMap();
            }
        }

        if (isMapOpen)
        {
            // Масштабирование карты
            float scrollDelta = Input.mouseScrollDelta.y;
            _mapScale += scrollDelta * 0.1f;
            _mapScale = Mathf.Clamp(_mapScale, 0.5f, 3f);
            _mapImage.transform.localScale = new Vector3(_mapScale, _mapScale, 1f);
            _mapTexture.transform.localScale = _mapImage.transform.localScale;

            // Перемещение карты
            if (Input.GetMouseButtonDown(0))
            {
                _lastMousePosition = Input.mousePosition;
            }
            else if (Input.GetMouseButton(0))
            {
                Vector3 deltaMousePosition = Input.mousePosition - _lastMousePosition;
                _mapImage.transform.localPosition += deltaMousePosition;
                _mapTexture.transform.localPosition = _mapImage.transform.localPosition;
                _lastMousePosition = Input.mousePosition;
            }
        }
    }
}
