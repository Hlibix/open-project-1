using UnityEngine;
using UnityEngine.Localization.Components;

public class LocalizeSceneNameFromSO : MonoBehaviour
{
    [SerializeField]
    private LocalizeStringEvent _localizationEvent;

    [SerializeField]
    private LocationSO _location;

    private void Start()
    {
        _localizationEvent.StringReference = _location.locationName;
    }
}