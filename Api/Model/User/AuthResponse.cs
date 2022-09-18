namespace DisneyApi.Model.DTO.User
{
    public class AuthResponse
    {
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public bool IsAuthSuccessful { get; set; }
    }
}
