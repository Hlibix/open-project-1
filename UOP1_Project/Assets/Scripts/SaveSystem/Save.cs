﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;

/// <summary>
/// This class contains all the variables that will be serialized and saved to a file.<br/>
/// Can be considered as a save file structure or format.
/// </summary>
[Serializable]
public class Save
{
    // This is test data, written according to TestScript.cs class
    // This will change according to whatever data that needs to be stored

    // The variables need to be public, else we would have to write trivial getter/setter functions.
    public string                    _locationId;
    public List<SerializedItemStack> _itemStacks                  = new();
    public List<string>              _finishedQuestlineItemsGUIds = new();

    public float  _masterVolume;
    public float  _musicVolume;
    public float  _sfxVolume;
    public int    _resolutionsIndex;
    public int    _antiAliasingIndex;
    public float  _shadowDistance;
    public bool   _isFullscreen;
    public Locale _currentLocale;

    public void SaveSettings(SettingsSO settings)
    {
        _masterVolume      = settings.MasterVolume;
        _musicVolume       = settings.MusicVolume;
        _sfxVolume         = settings.SfxVolume;
        _resolutionsIndex  = settings.ResolutionsIndex;
        _antiAliasingIndex = settings.AntiAliasingIndex;
        _shadowDistance    = settings.ShadowDistance;
        _isFullscreen      = settings.IsFullscreen;
        _currentLocale     = settings.CurrentLocale;
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }

    public void LoadFromJson(string json)
    {
        JsonUtility.FromJsonOverwrite(json, this);
    }
}