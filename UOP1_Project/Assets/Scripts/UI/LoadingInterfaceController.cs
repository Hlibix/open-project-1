using UnityEngine;

public class LoadingInterfaceController : MonoBehaviour
{
    [SerializeField]
    private GameObject _loadingInterface;

    [Header("Listening on")]
    [SerializeField]
    private BoolEventChannelSO _toggleLoadingScreen;


    private void OnEnable()
    {
        _toggleLoadingScreen.OnEventRaised += ToggleLoadingScreen;
    }

    private void OnDisable()
    {
        _toggleLoadingScreen.OnEventRaised -= ToggleLoadingScreen;
    }

    private void ToggleLoadingScreen(bool state)
    {
        _loadingInterface.SetActive(state);
    }
}