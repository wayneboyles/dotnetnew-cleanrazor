namespace CleanRazor.Security
{
    public interface ICurrentUser
    {
        public string? UserId { get; }

        public string? UserName { get; }

        public bool IsAuthenticated { get; }
    }
}
