using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIButtonPrompt : MonoBehaviour
{
    [SerializeField]
    private Image _interactionKeyBG;

    [SerializeField]
    private TextMeshProUGUI _interactionKeyText;

    [SerializeField]
    private Sprite _controllerSprite;

    [SerializeField]
    private Sprite _keyboardSprite;

    [SerializeField]
    private string _interactionKeyboardCode;

    [SerializeField]
    private string _interactionJoystickKeyCode;

    public void SetButtonPrompt(bool isKeyboard)
    {
        if (!isKeyboard)
        {
            _interactionKeyBG.sprite = _controllerSprite;
            _interactionKeyText.text = _interactionJoystickKeyCode;
        }
        else
        {
            _interactionKeyBG.sprite = _keyboardSprite;
            _interactionKeyText.text = _interactionKeyboardCode;
        }
    }
}