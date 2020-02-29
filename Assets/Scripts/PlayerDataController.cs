using UnityEngine;
using System.Collections.Generic;


public class PlayerDataController : MonoBehaviour
{
    #region Fields

    public static PlayerDataController instance;

    public Dictionary<InventoryItem, int> ItemsList;
    public int PlayerHealth;

    #endregion


    #region UnityMethods

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

    #endregion


    #region Methods

    private void OnStore()
    {
        ItemsList.Clear();
        var inventory = FindObjectOfType<InventoryController>();
        ItemsList = inventory.Items;
        var player = FindObjectOfType<PlayerController>();
        PlayerHealth = player.Health;
        print(ItemsList + " " + PlayerHealth);
    }
    #endregion
}
