using Noodlepop.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Noodlepop.Economy.Samples
{
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Economy/Samples/Cooking/Recipe")]
    public class RecipeAsset : ScriptableObject, IResourceConversion
    {
        [SerializeField]
        private Guid _guid = Guid.NewGuid();

        public Guid Id => _guid;

        [SerializeField]
        private List<IngredientAsset> _ingredients = new List<IngredientAsset>();

        public List<Guid> Inputs => GetInputIds();
        private List<Guid> GetInputIds()
        {
            List<Guid> guids = new List<Guid>();
            foreach (var input in _ingredients)
            {
                guids.Add(input.Id);
            }
            return guids;
        }

        [SerializeField]
        private MealAsset _output;
        public Guid OutputId => _output.Id;

    }
}