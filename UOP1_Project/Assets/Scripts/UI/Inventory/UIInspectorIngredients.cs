using System.Collections.Generic;
using UnityEngine;

public class UIInspectorIngredients : MonoBehaviour
{
    [SerializeField]
    private List<UIInspectorIngredientFiller> _instantiatedGameObjects = new();

    public void FillIngredients(List<ItemStack> listofIngredients, bool[] availabilityArray)
    {
        var maxCount = Mathf.Max(listofIngredients.Count, _instantiatedGameObjects.Count);

        for (var i = 0; i < maxCount; i++)
        {
            if (i < listofIngredients.Count)
            {
                if (i >= _instantiatedGameObjects.Count)
                {
                    //Do nothing, maximum ingredients for a recipe reached
                    Debug.Log("Maximum ingredients reached");
                }
                else
                {
                    var isAvailable = availabilityArray[i];
                    _instantiatedGameObjects[i].FillIngredient(listofIngredients[i], isAvailable);

                    _instantiatedGameObjects[i].gameObject.SetActive(true);
                }
            }
            else if (i < _instantiatedGameObjects.Count)
            {
                _instantiatedGameObjects[i].gameObject.SetActive(false);
            }
        }
    }
}