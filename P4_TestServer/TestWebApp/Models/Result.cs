namespace TestWebApp.Models
{
    public class Result
    {
        public int ErrorCode { get; set; }

        public string? ErrorMsg { get; set; }
    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }
    }
}