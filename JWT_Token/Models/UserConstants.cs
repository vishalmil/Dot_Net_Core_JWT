namespace JWT_Token.Models
{
    public class UserConstants
    {
        public static List<UserModel> users = new List<UserModel>()
        {
            new UserModel(){UserName="CybageAdmin", Password="CybageAdmin@123", Role="Admin"}
        };
    }
}
