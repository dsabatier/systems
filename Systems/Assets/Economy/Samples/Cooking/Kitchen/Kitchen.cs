using Noodlepop.Data;
using Noodlepop.Economy;
using Noodlepop.Economy.Samples;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Kitchen : MonoBehaviour
{
    [SerializeField]
    KitchenDataService _dataService;

    [SerializeField]
    private RecipeAsset _recipeAsset;

    IResourceValue A;
    IResourceValue B;
    IResourceValue C;

    private void Start()
    {
        _dataService.Load();
    }

    public void SetSlotA(IResourceValue resourceValue)
    {
        A = resourceValue;
    }

    public void SetSlotB(IResourceValue resourceValue)
    {
        B = resourceValue;
    }

    public void SetSlotC(IResourceValue resourceValue)
    {
        C = resourceValue;
    }

    [ContextMenu("Cook")]
    public void Cook()
    {
        if(CookMeal(out Meal meal))
        {
            Debug.Log("Cooked a:" + meal.Name);
            return;
        }

        Debug.LogError("Failed to cook meal!");
            
    }
    public bool CookMeal(out Meal meal)
    {
        if(A == null || B == null || C == null)
        {
            meal = null;
            return false;
        }
            

        Dictionary<Guid, IResourceValue> inputs = new Dictionary<Guid, IResourceValue>
        {
            { A.ResourceId, A },
            { B.ResourceId, B },
            { C.ResourceId, C }
        };

        Cooker cooker = new Cooker(_dataService);
        if(!cooker.Convert(_recipeAsset.Id, inputs, out IResourceValue result))
        {
            meal = null;
            return false;
        }

        ClearSlots();

        meal = result as Meal;
        return true;
    }

    private void ClearSlots()
    {
        A = null;
        B = null;
        C = null;
    }

    private class Cooker : IResourceConverter
    {
        IDataService _dataService;

        public Cooker(IDataService dataService)
        {
            _dataService = dataService;
        }

        public bool Convert(Guid recipeId, Dictionary<Guid, IResourceValue> ingredients, out IResourceValue result)
        {
            if(_dataService == null)
            {
                result = null;
                return false;
            }

            if (!_dataService.TryGetData(recipeId, out RecipeAsset conversion))
            {

                result = null;
                return false;
            }

            if (!_dataService.TryGetData(conversion.OutputId, out MealAsset resource))
            {
                result = null;
                return false;
            }

            result = resource.Create();

            // are the lists the same length and have all the same values
            return ingredients.Count == conversion.Inputs.Count && !ingredients.Keys.Except(conversion.Inputs).Any();
        }
    }

    public class Fridge
    {
        List<FridgeSlot> _inventory = new List<FridgeSlot>();

        public void Add(Guid ingredient) 
        {
            _inventory.Add(new FridgeSlot(ingredient));
        }
    }

    public class FridgeSlot : IResourceValue
    {
        private Guid _resourceId;
        public Guid ResourceId => _resourceId;

        public FridgeSlot(Guid resourceId)
        {
            _resourceId = resourceId;
        }
    }
}
