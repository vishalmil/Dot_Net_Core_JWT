namespace JWT_Token.Models
{
    public class UserConstants
    {
        public static List<UserModel> users = new List<UserModel>()
        {
            new UserModel(){UserName="TestAdmin", Password="TestAdmin@123", Role="Admin"}
        };
    }
}
