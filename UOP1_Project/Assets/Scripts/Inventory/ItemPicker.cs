using UnityEngine;

public class ItemPicker : MonoBehaviour
{
    [Header("Broadcasting on")]
    [SerializeField]
    private ItemEventChannelSO _addItemEvent;

    public void PickItem(ItemSO item)
    {
        if (_addItemEvent != null)
        {
            _addItemEvent.RaiseEvent(item);
        }
    }
}