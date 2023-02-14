using UnityEngine;
using UnityEngine.Localization.Settings;
using UnityEngine.Rendering.Universal;

public class SettingsSystem : MonoBehaviour
{
    [SerializeField]
    private VoidEventChannelSO SaveSettingsEvent;

    [SerializeField]
    private SettingsSO _currentSettings;

    [SerializeField]
    private UniversalRenderPipelineAsset _urpAsset;

    [SerializeField]
    private SaveSystem _saveSystem;

    [SerializeField]
    private FloatEventChannelSO _changeMasterVolumeEventChannel;

    [SerializeField]
    private FloatEventChannelSO _changeSFXVolumeEventChannel;

    [SerializeField]
    private FloatEventChannelSO _changeMusicVolumeEventChannel;

    private void Awake()
    {
        _saveSystem.LoadSaveDataFromDisk();
        _currentSettings.LoadSavedSettings(_saveSystem.saveData);
        SetCurrentSettings();
    }

    private void OnEnable()
    {
        SaveSettingsEvent.OnEventRaised += SaveSettings;
    }

    private void OnDisable()
    {
        SaveSettingsEvent.OnEventRaised -= SaveSettings;
    }

    /// <summary>
    /// Set current settings
    /// </summary>
    private void SetCurrentSettings()
    {
        _changeMusicVolumeEventChannel.RaiseEvent(_currentSettings.MusicVolume);   //raise event for volume change
        _changeSFXVolumeEventChannel.RaiseEvent(_currentSettings.SfxVolume);       //raise event for volume change
        _changeMasterVolumeEventChannel.RaiseEvent(_currentSettings.MasterVolume); //raise event for volume change
        var currentResolution = Screen.currentResolution;                          // get a default resolution in case saved resolution doesn't exist in the resolution List
        if (_currentSettings.ResolutionsIndex < Screen.resolutions.Length)
        {
            currentResolution = Screen.resolutions[_currentSettings.ResolutionsIndex];
        }

        Screen.SetResolution(currentResolution.width, currentResolution.height, _currentSettings.IsFullscreen);
        _urpAsset.shadowDistance  = _currentSettings.ShadowDistance;
        _urpAsset.msaaSampleCount = _currentSettings.AntiAliasingIndex;

        LocalizationSettings.SelectedLocale = _currentSettings.CurrentLocale;
    }

    private void SaveSettings()
    {
        _saveSystem.SaveDataToDisk();
    }
}