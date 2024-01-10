using System;

namespace Noodlepop.Data
{
    public interface IData
    {
        Guid Id { get; }
    }

    public interface IDataService
    {
        void AddData(IData data);
        void RemoveData(Guid id);

        bool TryGetData<T>(Guid dataId, out T outData) where T : IData;
    }
}
