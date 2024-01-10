using Noodlepop.Data;
using System.Collections.Generic;
using System;
using System.Linq;

namespace Noodlepop.Economy.Testing
{

    public class TestResourceValue : IResourceValue
    {
        private Guid _resourceId;
        public Guid ResourceId => _resourceId;

        public TestResourceValue(Guid resourceId)
        {
            _resourceId = resourceId;
        }
    }

    public class TestResourceA : IResource
    {
        public Guid Id { get; }
        public TestResourceA() { Id = Guid.NewGuid(); }
    }

    public class TestResourceB : IResource
    {
        public Guid Id { get; }
        public TestResourceB() { Id = Guid.NewGuid(); }
    }

    public class TestResourceC : IResource
    {
        public Guid Id { get; }
        public TestResourceC() { Id = Guid.NewGuid(); }
    }

    public class TestResourceD : IResource
    {
        public Guid Id { get; }
        public TestResourceD() { Id = Guid.NewGuid(); }
    }

    public class TestConversion : IResourceConversion
    {
        private Guid _id = Guid.NewGuid();
      
        public Guid Id => _id;

        private List<Guid> _inputs = new List<Guid>();
       
        public List<Guid> Inputs => _inputs;

        private Guid _outputId;
        public Guid OutputId => _outputId;

        public TestConversion(List<Guid> inputs, Guid outputId)
        {
            _inputs = inputs;
            _outputId = outputId;
        }
    }

    public class TestConverter : IResourceConverter
    {
        IDataService _dataService;

        public TestConverter(IDataService dataService)
        {
            _dataService = dataService;
        }

        public bool Convert(Guid conversionId, Dictionary<Guid, IResourceValue> inputs, out IResourceValue result)
        {
            if(!_dataService.TryGetData(conversionId, out IResourceConversion conversion))
            {
                
                result = null;
                return false;
            }

            if(!_dataService.TryGetData(conversion.OutputId, out IResource resource))
            {
                result = null;
                return false;
            }

            result = new TestResourceValue(resource.Id);

            // are the lists the same length and have all the same values
            return inputs.Count == conversion.Inputs.Count && !inputs.Keys.Except(conversion.Inputs).Any();
        }
    }
}

