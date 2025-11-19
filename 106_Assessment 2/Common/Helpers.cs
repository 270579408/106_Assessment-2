using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Text.RegularExpressions;
using System.Windows;


namespace _106_Assessment_2.Common
{
    public static class Helpers
    {
        public static bool IsPasswordValid(string password)
        {
            // Check minimum length
            if (password.Length < 6)
                return false;

            // Check for uppercase letter
            if (!Regex.IsMatch(password, @"[A-Z]"))
                return false;

            // Check for lowercase letter
            if (!Regex.IsMatch(password, @"[a-z]"))
                return false;

            // Check for digit
            if (!Regex.IsMatch(password, @"[0-9]"))
                return false;

            // If all conditions pass
            return true;
        }

        public static string UploadToCloudinary(string filePath, string innerFolder)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return null;

            try
            {
                Account account = new Account(
                    "devjm9laj",
                    "641983795813514",
                    "-a042HqSMoH07m00VXZXSApdTk0");

                Cloudinary cloudinary = new Cloudinary(account);

                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(filePath),
                    Folder = "onewhero_bay/" + innerFolder
                };

                var result = cloudinary.Upload(uploadParams);

                if (result.StatusCode == System.Net.HttpStatusCode.OK)
                    return result.SecureUrl.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Upload Failed: " + ex.Message);
            }

            return null;
        }

    }
}
