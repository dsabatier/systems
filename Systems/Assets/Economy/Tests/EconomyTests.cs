using NUnit.Framework;
using System;
using System.Collections.Generic;

using Noodlepop.Economy;
using Noodlepop.Data;
using Noodlepop.Data.Testing;
using Noodlepop.Economy.Testing;

public class EconomyTests
{
    IDataService _dataService;

    TestResourceA testResourceA;
    TestResourceB testResourceB;
    TestResourceC testResourceC;
    TestResourceD testResourceD;

    IResourceConversion testConversionA;

    [SetUp]
    public void SetUp()
    {
        _dataService = new TestDataService();

        // test resources
        testResourceA = new TestResourceA();
        _dataService.AddData(testResourceA);

        testResourceB = new TestResourceB();
        _dataService.AddData(testResourceB);

        testResourceC = new TestResourceC();
        _dataService.AddData(testResourceC);

        testResourceD = new TestResourceD();
        _dataService.AddData(testResourceD);

        // create a conversion
        List<Guid> inputs = new List<Guid>()
        {
            { Guid.NewGuid() },
            { Guid.NewGuid() },
            { Guid.NewGuid() },
        };

        testConversionA = new TestConversion(inputs, testResourceA.Id);

        _dataService.AddData(testConversionA);
    }
    [TearDown]
    public void TearDown() 
    {
        _dataService = null;

        testResourceA = null;
        testResourceB = null;
        testResourceC = null;
        testResourceD = null;

        testConversionA = null;
    }

    [Test]
    public void CanConvertResources()
    {
        IResourceConverter resourceConverter = new TestConverter(_dataService);

        Dictionary<Guid, IResourceValue> inputs = new Dictionary<Guid, IResourceValue>();
        foreach(var input in testConversionA.Inputs)
        {
            inputs.Add(input, new TestResourceValue(input));
        }

        if(!resourceConverter.Convert(testConversionA.Id, inputs, out IResourceValue result))
        {
            Assert.Fail("couldn't convert with valid inputs");
            return;
        }

        Assert.That(result.ResourceId, Is.EqualTo(testConversionA.OutputId));
    }

    [Test]
    public void FailToConvertResourcesWhenInputIsInvalid()
    {
        IResourceConverter resourceConverter = new TestConverter(_dataService);

        Dictionary<Guid, IResourceValue> inputs = new Dictionary<Guid, IResourceValue>();

        if (!resourceConverter.Convert(testConversionA.Id, inputs, out IResourceValue result))
        {
            Assert.Pass();
            return;
        }

        Assert.Fail("Converted successfully, but shouldn't have");
    }
}