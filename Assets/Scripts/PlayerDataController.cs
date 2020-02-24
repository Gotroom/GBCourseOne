using UnityEngine;
using System;
using System.Collections.Generic;

public class PlayerDataController : MonoBehaviour
{
    public static PlayerDataController instance;

    public Dictionary<InventoryItem, int> ItemsList;
    public int PlayerHealth;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            ItemsList = new Dictionary<InventoryItem, int>();
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        SceneController.StorePlayerData = OnStore;
    }

    private void OnStore()
    {
        ItemsList.Clear();
        var inventory = FindObjectOfType<InventoryController>();
        ItemsList = inventory.Items;
        var player = FindObjectOfType<PlayerController>();
        PlayerHealth = player.Health;
        print(ItemsList + " " + PlayerHealth);
    }

}
