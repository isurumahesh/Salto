namespace CloudWorks.Application.DTOs.Profiles
{
    public record AddProfileDTO
    {
        public string Email { get; init; } = default!;
        public Guid IdentityId { get; init; }
    }
}