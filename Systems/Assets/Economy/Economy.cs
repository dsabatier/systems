using System;
using System.Collections.Generic;
using Noodlepop.Data;

namespace Noodlepop.Economy
{
    public interface IResource : IData
    {

    }

    public interface IResourceConverter
    {
        public bool Convert(Guid conversionId, Dictionary<Guid, IResourceValue> inputs, out IResourceValue result);
    }

    public interface IResourceConversion : IData
    {
        List<Guid> Inputs { get; }
        Guid OutputId { get; }
    }

    public interface IResourceValue
    {
        Guid ResourceId { get; }
    }

    public interface IPool<T>
    {
        IResourceValue Value { get; }
    }
}
