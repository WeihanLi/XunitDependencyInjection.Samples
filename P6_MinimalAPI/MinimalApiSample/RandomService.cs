namespace MinimalApiSample;

public interface IRandomService
{
    int GetNumber(int max = 100);
}

public sealed class RandomService : IRandomService
{
    public int GetNumber(int max = 100) => Random.Shared.Next(max);
}