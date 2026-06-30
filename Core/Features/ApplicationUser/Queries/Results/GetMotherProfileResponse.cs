namespace Core.Features.ApplicationUser.Queries.Results
{
    public class GetMotherProfileResponse
    {
        // User Information
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }

        // Child Profile Information
        public string? ChildNickname { get; set; }
        public string? Message { get; set; }
    }
}
