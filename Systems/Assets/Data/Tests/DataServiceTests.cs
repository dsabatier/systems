using NUnit.Framework;
using Noodlepop.Data;
using Noodlepop.Data.Testing;

public class DataServiceTests
{
    // A Test behaves as an ordinary method
    [Test]
    public void CanAddData()
    {
        IData testData = new TestData();
        IDataService dataService = new TestDataService();

        Assert.DoesNotThrow(() => dataService.AddData(testData));
    }

    [Test]
    public void CanRetrieveDataById()
    {
        IData testData = new TestData();
        IDataService dataService = new TestDataService();

        dataService.AddData(testData);

        dataService.TryGetData(testData.Id, out TestData outData);

        Assert.That(outData, Is.Not.Null);
    }

    [Test]
    public void CanRemoveDataById()
    {
        IData testData = new TestData();
        IDataService dataService = new TestDataService();

        dataService.AddData(testData);

        dataService.RemoveData(testData.Id);

        Assert.That(dataService.TryGetData(testData.Id, out IData _) , Is.False);
    }
}
