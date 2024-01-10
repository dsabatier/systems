using Noodlepop.Data;
using System;
using UnityEngine;

namespace Noodlepop.Economy.Samples
{
    [CreateAssetMenu(fileName = "New Ingredient", menuName = "Economy/Samples/Cooking/Ingredient")]
    public class IngredientAsset : ScriptableObject, IData
    {
        [SerializeField]
        private Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        public Ingredient Create()
        {
            return new Ingredient(_id);
        }
    }

    public class Ingredient : IResourceValue
    {
        private Guid _resourceId;
        public Guid ResourceId => _resourceId;

        public Ingredient(Guid resourceId)
        {
            _resourceId = resourceId;
        }
    }
}