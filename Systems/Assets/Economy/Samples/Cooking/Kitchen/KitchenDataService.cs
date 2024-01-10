using Noodlepop.Data;
using Noodlepop.Economy.Samples;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Kitchen Data Service", menuName = "Economy/Samples/Cooking/Kitchen Data Service")]
public class KitchenDataService : ScriptableObject, IDataService
{
    [SerializeField]
    List<IngredientAsset> _ingredients = new List<IngredientAsset>();
    [SerializeField]
    List<RecipeAsset> _recipes = new List<RecipeAsset>();
    [SerializeField]
    List<MealAsset> _meals = new List<MealAsset>();

    Dictionary<Guid, IData> _data = new Dictionary<Guid, IData>();

    [ContextMenu("Load Data")]
    public void Load()
    {
        foreach(var ingredient in  _ingredients)
        {
            _data.Add(ingredient.Id, ingredient);
        }

        foreach(var recipe in _recipes)
        {
            _data.Add(recipe.Id, recipe);
        }

        foreach(var meal in _meals)
        {
            _data.Add(meal.Id, meal);
        }
    }

    public void AddData(IData data)
    {
        _data.Add(data.Id, data);
    }

    public void RemoveData(Guid id)
    {
        _data.Remove(id);
    }

    public bool TryGetData<T>(Guid dataId, out T outData) where T : IData
    {
        IData data;

        bool success = _data.TryGetValue(dataId, out data);

        outData = (T)data;

        return success && outData != null;

    }
}
