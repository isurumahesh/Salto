namespace CloudWorks.Data.Contracts.Entities;

public sealed class Profile
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public Guid? IdentityId { get; set; }
}