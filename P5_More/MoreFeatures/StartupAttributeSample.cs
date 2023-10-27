using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;
using Xunit.DependencyInjection;

namespace MoreFeatures;

[Startup(typeof(Startup1))]
public class StartupAttributeSample
{
    private readonly ICalc _calc;

    public StartupAttributeSample(ICalc calc)
    {
        _calc = calc;
    }
    
    [Fact]
    public void Calc_Test()
    {
        Assert.IsType<AddCalc>(_calc);
        Assert.Equal(2, _calc.Calc(1, 1));
    }
}

[Startup(typeof(Startup2))]
public class StartupAttributeSample2
{
    private readonly ICalc _calc;
    private readonly ITestOutputHelper _outputHelper;

    public StartupAttributeSample2(ICalc calc, ITestOutputHelper outputHelper)
    {
        _calc = calc;
        _outputHelper = outputHelper;
    }
    
    [Fact]
    public void Calc_Test()
    {
        _outputHelper.WriteLine($"{nameof(_calc)}: {_calc.GetHashCode()}");
        Assert.IsType<MultiCalc>(_calc);
        Assert.Equal(1, _calc.Calc(1, 1));
    }
    
    
    [Fact]
    public void Calc_Test2()
    {
        _outputHelper.WriteLine($"{nameof(_calc)}: {_calc.GetHashCode()}");
        Assert.IsType<MultiCalc>(_calc);
        Assert.Equal(2, _calc.Calc(1, 2));
    }
}

[Startup(typeof(Startup2), Shared = false)]
public class StartupAttributeSample3
{
    private readonly ICalc _calc;
    private readonly ITestOutputHelper _outputHelper;

    public StartupAttributeSample3(ICalc calc, ITestOutputHelper outputHelper)
    {
        _calc = calc;
        _outputHelper = outputHelper;
    }
    
    [Fact]
    public void Calc_Test()
    {
        _outputHelper.WriteLine($"{nameof(_calc)}: {_calc.GetHashCode()}");
        Assert.IsType<MultiCalc>(_calc);
        Assert.Equal(1, _calc.Calc(1, 1));
        Assert.Equal(1, ((MultiCalc)_calc).Counter);
    }
}

public interface ICalc
{
    int Calc(int num1, int num2);
}

file sealed class AddCalc : ICalc
{
    public int Calc(int num1, int num2) => num1 + num2;
}

file sealed class MultiCalc : ICalc
{
    public int Counter;
    public int Calc(int num1, int num2)
    {
        Interlocked.Increment(ref Counter);
        return num1 * num2;
    }
}

file sealed class Startup1
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ICalc, AddCalc>();
    }
}

file sealed class Startup2
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<ICalc, MultiCalc>();
    }
}