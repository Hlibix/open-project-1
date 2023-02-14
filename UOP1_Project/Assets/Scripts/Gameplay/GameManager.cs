using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private QuestManagerSO _questManager;

    [SerializeField]
    private GameStateSO _gameState;

    [Header("Inventory")]
    [SerializeField]
    private ItemSO _rockCandyRecipe;

    [SerializeField]
    private ItemSO _sweetDoughRecipe;

    [SerializeField]
    private ItemSO[] _finalRecipes;

    [SerializeField]
    private InventorySO _inventory;

    [Header("Broadcasting on")]
    [SerializeField]
    private VoidEventChannelSO _addRockCandyRecipeEvent;

    [SerializeField]
    private VoidEventChannelSO _cerisesMemoryEvent;

    [SerializeField]
    private VoidEventChannelSO _decideOnDishesEvent;

    private void Start()
    {
        StartGame();
    }

    private void OnEnable()
    {
        _addRockCandyRecipeEvent.OnEventRaised += AddRockCandyRecipe;
        _cerisesMemoryEvent.OnEventRaised      += AddSweetDoughRecipe;
        _decideOnDishesEvent.OnEventRaised     += AddFinalRecipes;
    }

    private void OnDisable()
    {
        _addRockCandyRecipeEvent.OnEventRaised -= AddRockCandyRecipe;
        _cerisesMemoryEvent.OnEventRaised      -= AddSweetDoughRecipe;
        _decideOnDishesEvent.OnEventRaised     -= AddFinalRecipes;
    }

    private void AddRockCandyRecipe()
    {
        _inventory.Add(_rockCandyRecipe);
    }

    private void AddSweetDoughRecipe()
    {
        _inventory.Add(_sweetDoughRecipe);
    }

    private void AddFinalRecipes()
    {
        foreach (var item in _finalRecipes)
        {
            _inventory.Add(item);
        }
    }

    private void StartGame()
    {
        _gameState.UpdateGameState(GameState.Gameplay);
        _questManager.StartGame();
    }
}