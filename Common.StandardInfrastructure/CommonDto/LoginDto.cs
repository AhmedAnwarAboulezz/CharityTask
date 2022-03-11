namespace Common.StandardInfrastructure.CommonDto
{
    public class LoginDto
    {
        public string Token { get; set; }
    }
    public class LoginParameter
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool RememberMe { get; set; } = false;
        public LoginParameter GetParameter(string userName = "superadmin", string password = "admin", bool rememberMe = false)
        {
            return new LoginParameter { UserName = userName, Password = password, RememberMe = rememberMe };
        }
    }
}
