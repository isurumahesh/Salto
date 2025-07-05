namespace CloudWorks.Application.Cache
{
    public interface ICachableQuery
    {
        string CacheKey { get; }
        TimeSpan CacheDuration { get; }
    }
}