namespace FileParser;
using NUnit.Framework;

[TestFixture]
public class Tests
{
        
    [SetUp]
    public void Setup()
    {
    }
    
    [TestCase("col=123", true)]
    [TestCase("col=123=", false)]
    [TestCase("=col123", false)]
    [TestCase("col123", false)]
    public void ValidateCondition(string condition, bool res)
    {
        var parser = new InputParser("");
        // Assert.AreEqual(parser.ValidateCondition(condition), res);

    }

}