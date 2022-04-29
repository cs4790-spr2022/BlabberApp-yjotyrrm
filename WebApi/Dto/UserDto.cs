namespace WebApi.Dto
{
    public class UserDto
    {
        public UserDto(string email, string username, string firstName, string lastName)
        {
            Email = email;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
        }

        public string? Username { get; }
        public string? Email { get; }
        public string? FirstName { get; }
        public string? LastName { get; }
    }
}
