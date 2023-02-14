using DG.Tweening;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField]
    private ItemSO _currentItem;

    [SerializeField]
    private GameObject _itemGO;

    private void Start()
    {
        AnimateItem();
    }

    public ItemSO GetItem()
    {
        return _currentItem;
    }

    public void SetItem(ItemSO item)
    {
        _currentItem = item;
    }

    public void AnimateItem()
    {
        if (_itemGO != null)
        {
            _itemGO.transform.DORotate(Vector3.one * 180, 5).SetLoops(-1, LoopType.Incremental);
        }
    }
}