using System;
using System.Collections.Generic;

namespace Noodlepop.Data.Testing
{
    public class TestData : IData
    {
        private Guid _id = Guid.NewGuid();
        public Guid Id => _id;
    }

    public class TestDataService : IDataService
    {
        private Dictionary<Guid, IData> _data = new Dictionary<Guid, IData>();

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
}