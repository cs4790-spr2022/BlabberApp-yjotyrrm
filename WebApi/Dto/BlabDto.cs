namespace WebApi.Dto
{
    public class BlabDto
    {
        public BlabDto(string userEmail, string content)
        {
            UserEmail = userEmail;
            Content = content;
        }

        public string UserEmail { get; }
        public string Content { get; }
    }
}
