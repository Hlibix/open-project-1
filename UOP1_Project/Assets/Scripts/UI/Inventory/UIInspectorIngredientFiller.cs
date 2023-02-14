using TMPro;
using UnityEngine;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UIInspectorIngredientFiller : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _ingredientAmount;

    [SerializeField]
    private GameObject _availableCheckMark;

    [SerializeField]
    private GameObject _unavailableCheckMark;

    [SerializeField]
    private GameObject _tooltip;

    [SerializeField]
    private LocalizeStringEvent _tooltipMessage;

    [SerializeField]
    private Image _ingredientIcon;

    [SerializeField]
    private Color _textColorAvailable;

    [SerializeField]
    private Color _textColorUnavailable;

    public void FillIngredient(ItemStack ingredient, bool isAvailable)
    {
        if (isAvailable)
        {
            _ingredientAmount.color = _textColorAvailable;
        }
        else
        {
            _ingredientAmount.color = _textColorUnavailable;
        }

        _ingredientAmount.text                    = ingredient.Amount.ToString();
        _tooltipMessage.StringReference           = ingredient.Item.Name;
        _tooltipMessage.StringReference.Arguments = new[] { new { ingredient.Amount } };

        _ingredientIcon.sprite = ingredient.Item.PreviewImage;
        _availableCheckMark.SetActive(isAvailable);
        _unavailableCheckMark.SetActive(!isAvailable);
    }

    public void HoveredItem()
    {
        _tooltip.SetActive(true);
    }

    public void UnHoveredItem()
    {
        _tooltip.SetActive(false);
    }
}