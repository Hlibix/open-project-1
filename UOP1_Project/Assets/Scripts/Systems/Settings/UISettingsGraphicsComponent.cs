using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.Serialization;

[Serializable]
public class ShadowDistanceTier
{
    public float  Distance;
    public string TierDescription;
}

public class UISettingsGraphicsComponent : MonoBehaviour
{
    [FormerlySerializedAs("ShadowDistanceTierList")]
    [SerializeField]
    private List<ShadowDistanceTier> _shadowDistanceTierList = new(); // filled from inspector

    [FormerlySerializedAs("URPAsset")]
    [SerializeField]
    private UniversalRenderPipelineAsset _uRPAsset;

    private int  _savedResolutionIndex;
    private int  _savedAntiAliasingIndex;
    private int  _savedShadowDistanceTier;
    private bool _savedFullscreenState;

    private int              _currentResolutionIndex;
    private List<Resolution> _resolutionsList;

    [SerializeField]
    private UISettingItemFiller _resolutionsField;

    /*  private int _currentShadowQualityIndex = default;
      private List<string> _shadowQualityList = default;
     [SerializeField] private UISettingItemFiller _shadowQualityField = default;*/

    private int          _currentAntiAliasingIndex;
    private List<string> _currentAntiAliasingList;

    [SerializeField]
    private UISettingItemFiller _antiAliasingField;

    private int _currentShadowDistanceTier;

    [SerializeField]
    private UISettingItemFiller _shadowDistanceField;

    private bool _isFullscreen;

    [SerializeField]
    private UISettingItemFiller _fullscreenField;

    public event UnityAction<int, int, float, bool> _save = delegate { };

    private Resolution _currentResolution;

    [SerializeField]
    private UIGenericButton _saveButton;

    [SerializeField]
    private UIGenericButton _resetButton;

    private void OnEnable()
    {
        _resolutionsField.OnNextOption     += NextResolution;
        _resolutionsField.OnPreviousOption += PreviousResolution;

        _shadowDistanceField.OnNextOption     += NextShadowDistanceTier;
        _shadowDistanceField.OnPreviousOption += PreviousShadowDistanceTier;

        _fullscreenField.OnNextOption     += NextFullscreenState;
        _fullscreenField.OnPreviousOption += PreviousFullscreenState;

        _antiAliasingField.OnNextOption     += NextAntiAliasingTier;
        _antiAliasingField.OnPreviousOption += PreviousAntiAliasingTier;

        _saveButton.Clicked  += SaveSettings;
        _resetButton.Clicked += ResetSettings;
    }

    private void OnDisable()
    {
        ResetSettings();

        _resolutionsField.OnNextOption     -= NextResolution;
        _resolutionsField.OnPreviousOption -= PreviousResolution;

        _shadowDistanceField.OnNextOption     -= NextShadowDistanceTier;
        _shadowDistanceField.OnPreviousOption -= PreviousShadowDistanceTier;

        _fullscreenField.OnNextOption     -= NextFullscreenState;
        _fullscreenField.OnPreviousOption -= PreviousFullscreenState;

        _antiAliasingField.OnNextOption     -= NextAntiAliasingTier;
        _antiAliasingField.OnPreviousOption -= PreviousAntiAliasingTier;

        _saveButton.Clicked  -= SaveSettings;
        _resetButton.Clicked -= ResetSettings;
    }

    public void Init()
    {
        _resolutionsList           = GetResolutionsList();
        _currentShadowDistanceTier = GetCurrentShadowDistanceTier();
        _currentAntiAliasingList   = GetDropdownData(Enum.GetNames(typeof(MsaaQuality)));

        _currentResolution        = Screen.currentResolution;
        _currentResolutionIndex   = GetCurrentResolutionIndex();
        _isFullscreen             = GetCurrentFullscreenState();
        _currentAntiAliasingIndex = GetCurrentAntialiasing();

        _savedResolutionIndex    = _currentResolutionIndex;
        _savedAntiAliasingIndex  = _currentAntiAliasingIndex;
        _savedShadowDistanceTier = _currentShadowDistanceTier;
        _savedFullscreenState    = _isFullscreen;
    }

    public void Setup()
    {
        Init();
        SetResolutionField();
        SetShadowDistance();
        SetFullscreen();
        SetAntiAliasingField();
    }

    #region Resolution

    private void SetResolutionField()
    {
        var displayText = _resolutionsList[_currentResolutionIndex].ToString();
        displayText = displayText.Substring(0, displayText.IndexOf("@"));

        _resolutionsField.FillSettingField(_resolutionsList.Count, _currentResolutionIndex, displayText);
    }

    private List<Resolution> GetResolutionsList()
    {
        var options = new List<Resolution>();
        for (var i = 0; i < Screen.resolutions.Length; i++)
        {
            options.Add(Screen.resolutions[i]);
        }

        return options;
    }

    private int GetCurrentResolutionIndex()
    {
        if (_resolutionsList == null)
        {
            _resolutionsList = GetResolutionsList();
        }

        var index = _resolutionsList.FindIndex(o => o.width == _currentResolution.width && o.height == _currentResolution.height);
        return index;
    }

    private void NextResolution()
    {
        _currentResolutionIndex++;
        _currentResolutionIndex = Mathf.Clamp(_currentResolutionIndex, 0, _resolutionsList.Count - 1);
        OnResolutionChange();
    }

    private void PreviousResolution()
    {
        _currentResolutionIndex--;
        _currentResolutionIndex = Mathf.Clamp(_currentResolutionIndex, 0, _resolutionsList.Count - 1);
        OnResolutionChange();
    }

    private void OnResolutionChange()
    {
        _currentResolution = _resolutionsList[_currentResolutionIndex];
        Screen.SetResolution(_currentResolution.width, _currentResolution.height, _isFullscreen);
        SetResolutionField();
    }

    #endregion

    #region ShadowDistance

    private void SetShadowDistance()
    {
        _shadowDistanceField.FillSettingField_Localized(_shadowDistanceTierList.Count, _currentShadowDistanceTier, _shadowDistanceTierList[_currentShadowDistanceTier].TierDescription);
    }

    private int GetCurrentShadowDistanceTier()
    {
        var tierIndex = -1;
        if (_shadowDistanceTierList.Exists(o => o.Distance == _uRPAsset.shadowDistance))
        {
            tierIndex = _shadowDistanceTierList.FindIndex(o => o.Distance == _uRPAsset.shadowDistance);
        }
        else
        {
            Debug.LogError("Current shadow distance is not in the tier List " + _uRPAsset.shadowDistance);
        }

        return tierIndex;
    }

    private void NextShadowDistanceTier()
    {
        _currentShadowDistanceTier++;
        _currentShadowDistanceTier = Mathf.Clamp(_currentShadowDistanceTier, 0, _shadowDistanceTierList.Count);
        OnShadowDistanceChange();
    }

    private void PreviousShadowDistanceTier()
    {
        _currentShadowDistanceTier--;
        _currentShadowDistanceTier = Mathf.Clamp(_currentShadowDistanceTier, 0, _shadowDistanceTierList.Count);
        OnShadowDistanceChange();
    }

    private void OnShadowDistanceChange()
    {
        _uRPAsset.shadowDistance = _shadowDistanceTierList[_currentShadowDistanceTier].Distance;
        SetShadowDistance();
    }

    #endregion

    #region fullscreen

    private void SetFullscreen()
    {
        if (_isFullscreen)
        {
            _fullscreenField.FillSettingField_Localized(2, 1, "On");
        }
        else
        {
            _fullscreenField.FillSettingField_Localized(2, 0, "Off");
        }
    }

    private bool GetCurrentFullscreenState()
    {
        return Screen.fullScreen;
    }

    private void NextFullscreenState()
    {
        _isFullscreen = true;
        OnFullscreenChange();
    }

    private void PreviousFullscreenState()
    {
        _isFullscreen = false;
        OnFullscreenChange();
    }

    private void OnFullscreenChange()
    {
        Screen.fullScreen = _isFullscreen;
        SetFullscreen();
    }

    #endregion

    #region Anti Aliasing

    private void SetAntiAliasingField()
    {
        var optionDisplay = _currentAntiAliasingList[_currentAntiAliasingIndex].Replace("_", "");
        _antiAliasingField.FillSettingField(_currentAntiAliasingList.Count, _currentAntiAliasingIndex, optionDisplay);
    }

    private int GetCurrentAntialiasing()
    {
        return _uRPAsset.msaaSampleCount;
    }

    private void NextAntiAliasingTier()
    {
        _currentAntiAliasingIndex++;
        _currentAntiAliasingIndex = Mathf.Clamp(_currentAntiAliasingIndex, 0, _currentAntiAliasingList.Count - 1);
        OnAntiAliasingChange();
    }

    private void PreviousAntiAliasingTier()
    {
        _currentAntiAliasingIndex--;
        _currentAntiAliasingIndex = Mathf.Clamp(_currentAntiAliasingIndex, 0, _currentAntiAliasingList.Count - 1);
        OnAntiAliasingChange();
    }

    private void OnAntiAliasingChange()
    {
        _uRPAsset.msaaSampleCount = _currentAntiAliasingIndex;
        SetAntiAliasingField();
    }

    #endregion

    private List<string> GetDropdownData(string[] optionNames, params string[] customOptions)
    {
        var options = new List<string>();
        foreach (var option in optionNames)
        {
            options.Add(option);
        }

        foreach (var option in customOptions)
        {
            options.Add(option);
        }

        return options;
    }

    public void SaveSettings()
    {
        _savedResolutionIndex    = _currentResolutionIndex;
        _savedAntiAliasingIndex  = _currentAntiAliasingIndex;
        _savedShadowDistanceTier = _currentShadowDistanceTier;
        _savedFullscreenState    = _isFullscreen;
        var shadowDistance = _shadowDistanceTierList[_currentShadowDistanceTier].Distance;
        _save.Invoke(_currentResolutionIndex, _currentAntiAliasingIndex, shadowDistance, _isFullscreen);
    }

    public void ResetSettings()
    {
        _currentResolutionIndex = _savedResolutionIndex;
        OnResolutionChange();
        _currentAntiAliasingIndex = _savedAntiAliasingIndex;
        OnAntiAliasingChange();
        _currentShadowDistanceTier = _savedShadowDistanceTier;
        OnShadowDistanceChange();
        _isFullscreen = _savedFullscreenState;
        OnFullscreenChange();
    }
}