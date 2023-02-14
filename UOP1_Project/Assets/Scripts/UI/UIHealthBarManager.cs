using UnityEngine;

public class UIHealthBarManager : MonoBehaviour
{
    [SerializeField]
    private HealthSO _protagonistHealth; //the HealthBar is watching this object, which is the health of the player

    [SerializeField]
    private HealthConfigSO _healthConfig;

    [SerializeField]
    private UIHeartDisplay[] _heartImages;

    [Header("Listening to")]
    [SerializeField]
    private VoidEventChannelSO _UIUpdateNeeded; //The player's Damageable issues this

    private void OnEnable()
    {
        _UIUpdateNeeded.OnEventRaised += UpdateHeartImages;

        InitializeHealthBar();
    }

    private void OnDestroy()
    {
        _UIUpdateNeeded.OnEventRaised -= UpdateHeartImages;
    }

    private void InitializeHealthBar()
    {
        _protagonistHealth.SetMaxHealth(_healthConfig.InitialHealth);
        _protagonistHealth.SetCurrentHealth(_healthConfig.InitialHealth);

        UpdateHeartImages();
    }

    private void UpdateHeartImages()
    {
        var heartValue       = _protagonistHealth.MaxHealth / _heartImages.Length;
        var filledHeartCount = Mathf.FloorToInt((float)_protagonistHealth.CurrentHealth / heartValue);

        for (var i = 0; i < _heartImages.Length; i++)
        {
            float heartPercent = 0;

            if (i < filledHeartCount)
            {
                heartPercent = 1;
            }
            else if (i == filledHeartCount)
            {
                heartPercent = (_protagonistHealth.CurrentHealth - filledHeartCount * (float)heartValue) / heartValue;
            }
            else
            {
                heartPercent = 0;
            }

            _heartImages[i].SetImage(heartPercent);
        }
    }
}