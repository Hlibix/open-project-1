using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField]
    private InventorySO _currentInventory;

    [SerializeField]
    private SaveSystem _saveSystem;

    [Header("Listening on")]
    [SerializeField]
    private ItemEventChannelSO _cookRecipeEvent;

    [SerializeField]
    private ItemEventChannelSO _useItemEvent;

    [SerializeField]
    private ItemEventChannelSO _equipItemEvent;

    [SerializeField]
    private ItemStackEventChannelSO _rewardItemEvent;

    [SerializeField]
    private ItemEventChannelSO _giveItemEvent;

    [SerializeField]
    private ItemEventChannelSO _addItemEvent;

    [SerializeField]
    private ItemEventChannelSO _removeItemEvent;

    private void OnEnable()
    {
        _cookRecipeEvent.OnEventRaised += CookRecipeEventRaised;
        _useItemEvent.OnEventRaised    += UseItemEventRaised;
        _equipItemEvent.OnEventRaised  += EquipItemEventRaised;
        _addItemEvent.OnEventRaised    += AddItem;
        _removeItemEvent.OnEventRaised += RemoveItem;
        _rewardItemEvent.OnEventRaised += AddItemStack;
        _giveItemEvent.OnEventRaised   += RemoveItem;
    }

    private void OnDisable()
    {
        _cookRecipeEvent.OnEventRaised -= CookRecipeEventRaised;
        _useItemEvent.OnEventRaised    -= UseItemEventRaised;
        _equipItemEvent.OnEventRaised  -= EquipItemEventRaised;
        _addItemEvent.OnEventRaised    -= AddItem;
        _removeItemEvent.OnEventRaised -= RemoveItem;
    }

    private void AddItemWithUIUpdate(ItemSO item)
    {
        _currentInventory.Add(item);
        if (_currentInventory.Contains(item))
        {
            var itemToUpdate = _currentInventory.Items.Find(o => o.Item == item);
        }
    }

    private void RemoveItemWithUIUpdate(ItemSO item)
    {
        var itemToUpdate = new ItemStack();

        if (_currentInventory.Contains(item))
        {
            itemToUpdate = _currentInventory.Items.Find(o => o.Item == item);
        }

        _currentInventory.Remove(item);

        var removeItem = _currentInventory.Contains(item);
    }

    private void AddItem(ItemSO item)
    {
        _currentInventory.Add(item);
        _saveSystem.SaveDataToDisk();
    }

    private void AddItemStack(ItemStack itemStack)
    {
        _currentInventory.Add(itemStack.Item, itemStack.Amount);
        _saveSystem.SaveDataToDisk();
    }

    private void RemoveItem(ItemSO item)
    {
        _currentInventory.Remove(item);
        _saveSystem.SaveDataToDisk();
    }

    private void CookRecipeEventRaised(ItemSO recipe)
    {
        if (_currentInventory.Contains(recipe))
        {
            var ingredients = recipe.IngredientsList;

            //remove ingredients (when it's a consumable)
            if (_currentInventory.hasIngredients(ingredients))
            {
                for (var i = 0; i < ingredients.Count; i++)
                {
                    if (ingredients[i].Item.ItemType.ActionType == ItemInventoryActionType.Use)
                    {
                        _currentInventory.Remove(ingredients[i].Item, ingredients[i].Amount);
                    }
                }

                _currentInventory.Add(recipe.ResultingDish);
            }
        }

        _saveSystem.SaveDataToDisk();
    }

    private void UseItemEventRaised(ItemSO item)
    {
        RemoveItem(item);
    }

    //This empty function is left here for the possibility of adding decorative 3D items
    private void EquipItemEventRaised(ItemSO item)
    {
    }
}