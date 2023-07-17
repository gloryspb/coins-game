using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUsageHandler : MonoBehaviour
{
    // private ItemsDatabase _database;
    [SerializeField] private Player _player;
    public void UseItem(int id)
    {
        switch (id)
        {
            case 3:
                UseHealthPotion();
                break;
        }
    }

    private void UseHealthPotion()
    {
        _player.AddHealthPoints(5f);
    }
}
