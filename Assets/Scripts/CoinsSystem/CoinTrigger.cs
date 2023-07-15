using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    [SerializeField] private string _coinType;
    private ItemStorage playerStorage;
    public UITooltipDisplay tooltipDisplay;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            playerStorage = other.gameObject.GetComponent<ItemStorage>();
            switch (_coinType)
            {
                case "SilverCoin":
                    ScoreManager.Instance.AddScore(1);
                    playerStorage.SearchForSameItem(1, 1);
                    Destroy(gameObject);
                    break;
                case "GoldCoin":
                    tooltipDisplay.ShowTooltip(transform);
                    break;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        playerStorage = other.gameObject.GetComponent<ItemStorage>();
        if (other.gameObject.name == "Player")
        {
            switch (_coinType)
            {
                case "GoldCoin":
                    if (Input.GetKey(KeyCode.E))
                    {
                        ScoreManager.Instance.AddScore(2);
                        playerStorage.SearchForSameItem(2, 1);
                        Destroy(gameObject);

                        tooltipDisplay.HideTooltip();
                    }
                    break;
            }
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