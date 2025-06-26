namespace CloudWorks.Services.Contracts.Sites;

public interface ISiteService
{
    Task<List<Site>> GetSites(CancellationToken cancellationToken);
}
