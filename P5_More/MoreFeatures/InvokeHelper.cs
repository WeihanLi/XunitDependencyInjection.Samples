using System.Diagnostics;
using Xunit.DependencyInjection;

namespace MoreFeatures;

public sealed class InvokeHelper
{
    private readonly ITestOutputHelperAccessor _outputHelperAccessor;

    public InvokeHelper(ITestOutputHelperAccessor outputHelperAccessor)
    {
        _outputHelperAccessor = outputHelperAccessor;
    }

    public void Profile(Action action, string actionName)
    {
        var watch = Stopwatch.StartNew();
        action();
        watch.Stop();
        _outputHelperAccessor.Output?.WriteLine($"{actionName} elapsed:{watch.ElapsedMilliseconds}ms");
    }
}