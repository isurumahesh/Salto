namespace CloudWorks.Application.Cache
{
    public interface ICacheInvalidator
    {
        IEnumerable<string> CachePatternsToInvalidate { get; }
    }
}