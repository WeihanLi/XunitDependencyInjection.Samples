using SharedProject;
using Xunit;
using Xunit.DependencyInjection;

namespace BasicUsage;

public class IdGeneratorTest
{
    private readonly IIdGenerator _idGenerator;

    public IdGeneratorTest(IIdGenerator idGenerator)
    {
        _idGenerator = idGenerator;
    }

    [Fact]
    public void NewIdTest()
    {
        var newId = _idGenerator.NewId();
        Assert.NotNull(newId);
        Assert.NotEmpty(newId);
    }

    [Theory]
    [InlineData(null, "test")]
    public void MethodInjectionTest([FromServices] IIdGenerator idGenerator, string data)
    {
        Assert.NotNull(data);
        Assert.NotNull(idGenerator);
        Assert.Equal(_idGenerator, idGenerator);
        var newId = idGenerator.NewId();
        Assert.NotNull(newId);
        Assert.NotEmpty(newId);
    }
    
    [Theory]
    [MemberData(nameof(GetTestData))]
    public void MemberDataMethodInjectionTest([FromServices] IIdGenerator idGenerator, string data)
    {
        Assert.NotNull(data);
        Assert.NotNull(idGenerator);
        Assert.Equal(_idGenerator, idGenerator);
        var newId = idGenerator.NewId();
        Assert.NotNull(newId);
        Assert.NotEmpty(newId);
    }
    
    [Theory]
    [MethodData(nameof(GetTestData))]
    public void MethodDataMethodInjectionTest([FromServices] IIdGenerator idGenerator, string data)
    {
        Assert.NotNull(data);
        Assert.Equal("test", data);
        
        Assert.NotNull(idGenerator);
        Assert.Equal(_idGenerator, idGenerator);
        var newId = idGenerator.NewId();
        Assert.NotNull(newId);
        Assert.NotEmpty(newId);
    }
    
    [Theory]
    [MethodData(nameof(TestClass.GetTestData), typeof(TestClass), "test2")]
    public void MethodDataMethodInjectionExternalClassTest([FromServices] IIdGenerator idGenerator, string data)
    {
        Assert.NotNull(data);
        Assert.Equal("test2", data);
        
        Assert.NotNull(idGenerator);
        Assert.Equal(_idGenerator, idGenerator);
        var newId = idGenerator.NewId();
        Assert.NotNull(newId);
        Assert.NotEmpty(newId);
    }

    public static IEnumerable<object?[]> GetTestData()
    {
        yield return new object?[] { null, "test" };
    }
}

public static class TestClass
{
    public static IEnumerable<object?[]> GetTestData(string data)
    {
        yield return new object?[] { null, data };
    }
}