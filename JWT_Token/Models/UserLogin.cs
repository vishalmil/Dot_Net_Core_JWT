using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace JWT_Token.Models
{
    public class UserLogin
    {
        public string UserName { get; set; }

        public string Password { get; set; }

    }
}
