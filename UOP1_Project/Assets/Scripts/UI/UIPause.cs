using UnityEngine;
using UnityEngine.Events;

public class UIPause : MonoBehaviour
{
    [SerializeField]
    private InputReader _inputReader;

    [SerializeField]
    private UIGenericButton _resumeButton;

    [SerializeField]
    private UIGenericButton _settingsButton;

    [SerializeField]
    private UIGenericButton _backToMenuButton;

    [Header("Listening to")]
    [SerializeField]
    private BoolEventChannelSO _onPauseOpened;

    public event UnityAction Resumed;
    public event UnityAction SettingsScreenOpened;
    public event UnityAction BackToMainRequested;

    private void OnEnable()
    {
        _onPauseOpened.RaiseEvent(true);

        _resumeButton.SetButton(true);
        _inputReader.MenuCloseEvent += Resume;
        _resumeButton.Clicked       += Resume;
        _settingsButton.Clicked     += OpenSettingsScreen;
        _backToMenuButton.Clicked   += BackToMainMenuConfirmation;
    }

    private void OnDisable()
    {
        _onPauseOpened.RaiseEvent(false);

        _inputReader.MenuCloseEvent -= Resume;
        _resumeButton.Clicked       -= Resume;
        _settingsButton.Clicked     -= OpenSettingsScreen;
        _backToMenuButton.Clicked   -= BackToMainMenuConfirmation;
    }

    private void Resume()
    {
        Resumed.Invoke();
    }

    private void OpenSettingsScreen()
    {
        SettingsScreenOpened.Invoke();
    }

    private void BackToMainMenuConfirmation()
    {
        BackToMainRequested.Invoke();
    }

    public void CloseScreen()
    {
        Resumed.Invoke();
    }
}