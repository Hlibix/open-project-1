using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UIActionButton : MonoBehaviour
{
    [SerializeField]
    private LocalizeStringEvent _buttonActionText;

    [SerializeField]
    private Button _buttonAction;

    [SerializeField]
    private UIButtonPrompt _buttonPromptSetter;

    [SerializeField]
    private InputReader _inputReader;

    public UnityAction Clicked;

    private bool _hasEvent;

    public void FillInventoryButton(ItemTypeSO itemType, bool isInteractable = true)
    {
        _buttonAction.interactable        = isInteractable;
        _buttonActionText.StringReference = itemType.ActionName;

        var isKeyboard = true;
        _buttonPromptSetter.SetButtonPrompt(isKeyboard);
        if (isInteractable)
        {
            if (_inputReader != null)
            {
                _hasEvent                               =  true;
                _inputReader.InventoryActionButtonEvent += ClickActionButton;
            }
        }
        else
        {
            if (_inputReader != null)
            {
                if (_hasEvent)
                {
                    _inputReader.InventoryActionButtonEvent -= ClickActionButton;
                }
            }
        }
    }

    public void ClickActionButton()
    {
        Clicked.Invoke();
    }

    private void OnDisable()
    {
        if (_inputReader != null)
        {
            if (_hasEvent)
            {
                _inputReader.InventoryActionButtonEvent -= ClickActionButton;
            }
        }
    }
}