namespace CloudWorks.Application.DTOs.Profiles
{
    public record ProfileDTO
    {
        public Guid Id { get; init; }
        public string Email { get; init; }
        public Guid IdentityId { get; init; }
    }
}