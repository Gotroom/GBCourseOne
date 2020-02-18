using UnityEngine;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    #region Fields

    private const float DEFAULT_VOLUME = 1.0f;
    private const string VOLUME_FORMAT = "0.00";

    [SerializeField] private Text _volumeText;
    [SerializeField] private Slider _volumeSlider;
    [SerializeField] private Dropdown _qualityComboBox;

    #endregion

    #region UnityMethods

    private void Start()
    {
        SetDefaultVolume();
        _qualityComboBox.value = QualitySettings.GetQualityLevel();
    }

    #endregion

    #region Methods

    private void SetDefaultVolume()
    {
        var playerVolume = PlayerPrefs.GetFloat("volume");
        playerVolume = playerVolume == 0.0f ? DEFAULT_VOLUME : playerVolume;

        AudioListener.volume = playerVolume;
        _volumeText.text = playerVolume.ToString(VOLUME_FORMAT);
        _volumeSlider.value = playerVolume;

        PlayerPrefs.SetFloat("volume", playerVolume);
    }

    public void SetVolume(float value)
    {
        AudioListener.volume = value;
        _volumeText.text = value.ToString(VOLUME_FORMAT);
        PlayerPrefs.SetFloat("volume", value);
    }

    public void SetQuality(int value)
    {
        QualitySettings.SetQualityLevel(value);
    }

    #endregion
}
