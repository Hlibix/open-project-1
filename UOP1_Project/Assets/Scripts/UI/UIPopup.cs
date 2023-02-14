using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public enum PopupButtonType
{
    Confirm,
    Cancel,
    Close,
    DoNothing
}

public enum PopupType
{
    Quit,
    NewGame,
    BackToMenu
}

public class UIPopup : MonoBehaviour
{
    [SerializeField]
    private LocalizeStringEvent _titleText;

    [SerializeField]
    private LocalizeStringEvent _descriptionText;

    [SerializeField]
    private Button _buttonClose;

    [SerializeField]
    private UIGenericButton _popupButton1;

    [SerializeField]
    private UIGenericButton _popupButton2;

    [SerializeField]
    private InputReader _inputReader;

    private PopupType _actualType;

    public event UnityAction<bool> ConfirmationResponseAction;
    public event UnityAction       ClosePopupAction;

    private void OnDisable()
    {
        _popupButton2.Clicked       -= CancelButtonClicked;
        _popupButton1.Clicked       -= ConfirmButtonClicked;
        _inputReader.MenuCloseEvent -= ClosePopupButtonClicked;
    }

    public void SetPopup(PopupType popupType)
    {
        _actualType = popupType;
        var isConfirmation = false;
        var hasExitButton  = false;
        _titleText.StringReference.TableEntryReference       = _actualType + "_Popup_Title";
        _descriptionText.StringReference.TableEntryReference = _actualType + "_Popup_Description";
        var tableEntryReferenceConfirm = PopupButtonType.Confirm + "_" + _actualType;
        var tableEntryReferenceCancel  = PopupButtonType.Cancel  + "_" + _actualType;
        switch (_actualType)
        {
            case PopupType.NewGame:
            case PopupType.BackToMenu:
                isConfirmation = true;

                _popupButton1.SetButton(tableEntryReferenceConfirm, true);
                _popupButton2.SetButton(tableEntryReferenceCancel, false);
                hasExitButton = true;
                break;
            case PopupType.Quit:
                isConfirmation = true;
                _popupButton1.SetButton(tableEntryReferenceConfirm, true);
                _popupButton2.SetButton(tableEntryReferenceCancel, false);
                hasExitButton = false;
                break;
            default:
                isConfirmation = false;
                hasExitButton  = false;
                break;
        }

        if (isConfirmation) // needs two button : Is a decision
        {
            _popupButton1.gameObject.SetActive(true);
            _popupButton2.gameObject.SetActive(true);

            _popupButton2.Clicked += CancelButtonClicked;
            _popupButton1.Clicked += ConfirmButtonClicked;
        }
        else // needs only one button : Is an information
        {
            _popupButton1.gameObject.SetActive(true);
            _popupButton2.gameObject.SetActive(false);

            _popupButton1.Clicked += ConfirmButtonClicked;
        }

        _buttonClose.gameObject.SetActive(hasExitButton);

        if (hasExitButton) // can exit : Has to take the decision or aknowledge the information
        {
            _inputReader.MenuCloseEvent += ClosePopupButtonClicked;
        }
    }

    public void ClosePopupButtonClicked()
    {
        ClosePopupAction.Invoke();
    }

    private void ConfirmButtonClicked()
    {
        ConfirmationResponseAction.Invoke(true);
    }

    private void CancelButtonClicked()
    {
        ConfirmationResponseAction.Invoke(false);
    }
}