namespace _106_Assessment_2
{
    public static class GlobalData
    {
        public static string CurrentUserId { get; set; }
        public static string CurrentUserName { get; set; }
        public static string CurrentUserEmail { get; set; }

        public void ChangeLogInOption(string text)
        {
            LoginSideBarOption.Content = text;
        }

        public static bool IsAdmin { get; set; }
    }
}