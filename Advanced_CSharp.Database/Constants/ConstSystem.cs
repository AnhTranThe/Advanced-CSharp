namespace Advanced_CSharp.Database.Constants
{
    public static class ConstSystem
    {

        public const string AdminRole = "Admin";
        public const string CustomerRole = "Customer";
        public const string AdminRoleId = "66DACA59-9B83-462E-BD80-54BEF2918B5C";
        public const string AdminUserId = "731ACDC1-6507-4C97-86C8-9C9F135D85FE";
        public const string CustomerRoleId = "E362EB26-D015-4A22-9B24-D32B6BE4DCFA";
        public const string TesterUserId = "E26F15D9-AD4A-41FD-BD57-54A24DB7DB0B";
        public static Guid loggedInUserId { get; set; } = Guid.Empty; // Keep track of the logged-in user
        public static string loggedUserName { get; set; } = string.Empty;



    }
}
