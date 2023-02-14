using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Localization.Components;
using UnityEngine.UI;

public class UIItemForAnimation : MonoBehaviour
{
    [SerializeField]
    private LocalizeSpriteEvent _bgLocalizedImage;

    [SerializeField]
    private Image _itemPreviewImage;

    [SerializeField]
    private Image _bgImage;

    public event UnityAction AnimationEnded;

    public void SetItem(ItemSO item)
    {
        if (item.IsLocalized)
        {
            _bgLocalizedImage.enabled        = true;
            _bgLocalizedImage.AssetReference = item.LocalizePreviewImage;
        }
        else
        {
            _bgLocalizedImage.enabled = false;
            _itemPreviewImage.sprite  = item.PreviewImage;
        }

        _bgImage.color = item.ItemType.TypeColor;
    }

    public void OnAnimationEnded()
    {
        AnimationEnded.Invoke();
    }
}