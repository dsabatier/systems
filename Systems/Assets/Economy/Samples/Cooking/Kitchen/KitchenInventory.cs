using Noodlepop.Economy.Samples;
using System;
using System.Collections.Generic;
using UnityEngine;

public class KitchenInventory : MonoBehaviour
{
    [SerializeField]
    List<IngredientInitializer> _startingIngredients = new List<IngredientInitializer>();

    Dictionary<Guid, int> _inventory = new Dictionary<Guid, int>();

    public void Start()
    {
        foreach(var ingredient in _startingIngredients)
        {
            _inventory.Add(ingredient.Ingredient.Id, ingredient.Quantity);
        }
    }

    public bool HasIngredient(Guid ingredientId, out int quantity)
    {
        if (!_inventory.TryGetValue(ingredientId, out quantity))
        {
            return false;
        }

        return true;
    }

    public void AddIngredient(Guid ingredientId, int quantity)
    {
        if (_inventory.ContainsKey(ingredientId))
        {
            _inventory[ingredientId] += quantity;
        }
        else
        {
            _inventory.Add(ingredientId, quantity);
        }
    }

    public bool RemoveIngredient(Guid ingredientId, int quantity)
    {
        if(_inventory.ContainsKey(ingredientId) && _inventory[ingredientId] >= quantity)
        {
            _inventory[ingredientId] -= quantity;
            return true;
        }

        return false;
    }
}

[System.Serializable]
public class IngredientInitializer
{
    public IngredientAsset Ingredient;
    public int Quantity;
}