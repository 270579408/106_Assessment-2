namespace _106_Assessment_2.Commons
{
    public static class DriveLinkConverter
    {
        public static string converter(string link)
        {
            if (string.IsNullOrWhiteSpace(link))
                return string.Empty;

            string[] splitedLink = link.Split("/");

            if (splitedLink.Length < 6)
                return link;

            string newLink = "https://drive.google.com/uc?export=view&id=" + splitedLink[splitedLink.Length - 2];

            return newLink;
        }
    }
}
