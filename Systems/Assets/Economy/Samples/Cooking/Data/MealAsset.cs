using Noodlepop.Data;
using System;
using UnityEngine;

namespace Noodlepop.Economy.Samples
{
    [CreateAssetMenu(fileName = "New Meal", menuName = "Economy/Samples/Cooking/Meal")]
    public class MealAsset : ScriptableObject, IData
    {
        private Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        public Meal Create()
        {
            Meal meal = new Meal(_id, name);
            return meal;
        }
    }

    public class Meal : IResourceValue
    {
        public string Name { get; private set; }
        private Guid _resourceId;

        public Meal(Guid resourceId, string name)
        {
            _resourceId = resourceId;
            Name = name;
        }

        public Guid InstanceId { get; private set; } = Guid.NewGuid();
        public Guid ResourceId => _resourceId;
    }
}