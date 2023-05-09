using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    [SerializeField] private string _coinType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            switch (_coinType)
            {
                case "SilverCoin":
                    ScoreManager.Instance.addScore(1);
                    Destroy(gameObject);
                    break;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            switch (_coinType)
            {
                case "GoldCoin":
                    if (Input.GetKey(KeyCode.E))
                    {
                        ScoreManager.Instance.addScore(2);
                        Destroy(gameObject);
                    }
                    break;
            }
        }
    }
}