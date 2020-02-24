using UnityEngine;
using UnityEngine.UI;


public class UIHealthDisplay : MonoBehaviour
{
    #region Fields

    [SerializeField] private GameObject _healthBar;

    #endregion


    #region UnityMethods

    private void Start()
    {
        PlayerController.HealthChanged = OnHealthChanged;
        OnHealthChanged(PlayerDataController.instance.PlayerHealth, 5);
    }

    #endregion


    #region Methods

    private void OnHealthChanged(int health, int maxHealth)
    {
        float scale = (float)health / (float)maxHealth;
        //print(scale + " " + health + " " + maxHealth);
        _healthBar.transform.localScale = new Vector3(scale, 1.0f);
    }

    #endregion
}
