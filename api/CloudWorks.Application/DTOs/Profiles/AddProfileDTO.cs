namespace CloudWorks.Application.DTOs.Profiles
{
    public class AddProfileDTO
    {
        public string Email { get; set; } = default!;
        public Guid? IdentityId { get; set; }
    }
}