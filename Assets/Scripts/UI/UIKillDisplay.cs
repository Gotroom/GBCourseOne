using UnityEngine;
using TMPro;

public class UIKillDisplay : MonoBehaviour
{
    #region Fields

    [SerializeField] private TextMeshProUGUI _killText;

    private int _killCounter = 0;

    #endregion

    #region UnityMethods

    private void Start()
    {
        BaseWeapon.EnemiesDestroyed = OnEnemiesDestroyed;
    }

    void Update()
    {
        _killText.text = "Kills: " + _killCounter;
    }

    #endregion

    #region Methods

    private void OnEnemiesDestroyed(int count)
    {
        _killCounter += count;
    }

    #endregion
}
