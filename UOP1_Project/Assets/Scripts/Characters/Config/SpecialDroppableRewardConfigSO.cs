using UnityEngine;

[CreateAssetMenu(fileName = "DroppableRewardConfig", menuName = "EntityConfig/Special Reward Dropping Rate Config")]
public class SpecialDroppableRewardConfigSO : DroppableRewardConfigSO
{
    [Tooltip("Current count of dropped items")]
    [SerializeField]
    private int _specialDroppableCurrentCount;

    [Tooltip("Max count where the special droppable needs to be dropped")]
    [SerializeField]
    private int _specialDroppableMaxCount;

    [SerializeField]
    private DropGroup _specialItem = new();

    public override DropGroup DropSpecialItem()
    {
        _specialDroppableCurrentCount++;
        if (_specialDroppableCurrentCount >= _specialDroppableMaxCount)
        {
            return _specialItem;
        }

        return null;
    }
}