namespace SharedProject;

public interface IIdGenerator
{
    string NewId();
}

public class GuidIdGenerator : IIdGenerator
{
    public string NewId()
    {
        return Guid.NewGuid().ToString("N");
    }
}

public class IntIdGenerator : IIdGenerator
{
    private long _id;

    public string NewId()
    {
        Interlocked.Increment(ref _id);
        return _id.ToString();
    }
}