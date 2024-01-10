using Noodlepop.Economy.Samples;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IngredientList : MonoBehaviour
{
    [SerializeField]
    private UnityEvent<Guid> _onIngredientAdded = new UnityEvent<Guid>();

    [SerializeField]
    private UnityEvent<Guid> _onIngredientRemoved = new UnityEvent<Guid>();

    [SerializeField]
    private KitchenDataService _dataService;

    [SerializeField]
    private StyledButton _buttonPrefab;

    [SerializeField]
    private ButtonStyle _normalButtonStyle;

    [SerializeField]
    private ButtonStyle _selectedButtonStyle;

    private HashSet<Guid> _ingredientsAdded =  new HashSet<Guid>();

    public void ShowIngredients(Guid recipeId)
    {
        _ingredientsAdded.Clear();

        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if(!_dataService.TryGetData(recipeId, out RecipeAsset recipeAsset))
        {
            return;
        }

        foreach(Guid input in recipeAsset.Inputs)
        {
            if(!_dataService.TryGetData(input, out IngredientAsset ingredientType))
            {
                continue;
            }

            StyledButton button = Instantiate(_buttonPrefab, transform);
            button.SetLabel(ingredientType.name);
            button.SetStyle(_normalButtonStyle);

            button.AddListener(() => HandleIngredientClicked(button, ingredientType));
        }
    }

    private void HandleIngredientClicked(StyledButton button, IngredientAsset ingredientType)
    {
        if(_ingredientsAdded.Contains(ingredientType.Id))
        {
            _ingredientsAdded.Remove(ingredientType.Id);
            _onIngredientRemoved?.Invoke(ingredientType.Id);

            button.SetStyle(_normalButtonStyle);
            return;
        }   

        _ingredientsAdded.Add(ingredientType.Id);
        button.SetStyle(_selectedButtonStyle);

        _onIngredientAdded?.Invoke(ingredientType.Id);
        
    }
}
