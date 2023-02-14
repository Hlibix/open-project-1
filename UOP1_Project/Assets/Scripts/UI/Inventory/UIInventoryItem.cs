using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UIInventoryItem : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _itemCount;

    [SerializeField]
    private Image _itemPreviewImage;

    [SerializeField]
    private Image _bgImage;

    [SerializeField]
    private Image _imgHover;

    [SerializeField]
    private Image _imgSelected;

    [SerializeField]
    private Image _bgInactiveImage;

    [SerializeField]
    private Button _itemButton;

    [SerializeField]
    private LocalizeSpriteEvent _bgLocalizedImage;

    public UnityAction<ItemSO> ItemSelected;

    [HideInInspector]
    public ItemStack currentItem;

    private bool _isSelected;

    public void SetItem(ItemStack itemStack, bool isSelected)
    {
        _isSelected = isSelected;
        _itemPreviewImage.gameObject.SetActive(true);
        _itemCount.gameObject.SetActive(true);
        _bgImage.gameObject.SetActive(true);
        _imgHover.gameObject.SetActive(true);
        _imgSelected.gameObject.SetActive(true);
        _itemButton.gameObject.SetActive(true);
        _bgInactiveImage.gameObject.SetActive(false);

        UnhoverItem();
        currentItem = itemStack;

        _imgSelected.gameObject.SetActive(isSelected);

        if (itemStack.Item.IsLocalized)
        {
            _bgLocalizedImage.enabled        = true;
            _bgLocalizedImage.AssetReference = itemStack.Item.LocalizePreviewImage;
        }
        else
        {
            _bgLocalizedImage.enabled = false;
            _itemPreviewImage.sprite  = itemStack.Item.PreviewImage;
        }

        _itemCount.text = itemStack.Amount.ToString();
        _bgImage.color  = itemStack.Item.ItemType.TypeColor;
    }

    public void SetInactiveItem()
    {
        UnhoverItem();
        currentItem = null;
        _itemPreviewImage.gameObject.SetActive(false);
        _itemCount.gameObject.SetActive(false);
        _bgImage.gameObject.SetActive(false);
        _imgHover.gameObject.SetActive(false);
        _imgSelected.gameObject.SetActive(false);
        _itemButton.gameObject.SetActive(false);
        _bgInactiveImage.gameObject.SetActive(true);
    }

    public void SelectFirstElement()
    {
        _isSelected = true;
        _itemButton.Select();
        SelectItem();
    }

    private void OnEnable()
    {
        if (_isSelected)
        {
            SelectItem();
        }
    }

    public void HoverItem()
    {
        _imgHover.gameObject.SetActive(true);
    }

    public void UnhoverItem()
    {
        _imgHover.gameObject.SetActive(false);
    }

    public void SelectItem()
    {
        _isSelected = true;
        if (ItemSelected != null && currentItem != null && currentItem.Item != null)

        {
            _imgSelected.gameObject.SetActive(true);
            ItemSelected.Invoke(currentItem.Item);
        }
        else
        {
            _imgSelected.gameObject.SetActive(false);
        }
    }

    public void UnselectItem()
    {
        _isSelected = false;
        _imgSelected.gameObject.SetActive(false);
    }
}