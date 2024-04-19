using System.Globalization;
using System.Text.RegularExpressions;
using System.Text;

namespace WebEcommerce.Areas.Admin.Services
{
    public class SlugifyService
    {
        private static SlugifyService? instance;

        private SlugifyService() { }

        public static SlugifyService Instance
        {
            get
            {
                instance ??= new SlugifyService();

                return instance;
            }
        }

        public string GenerateSlug(string s)
        {
            string str = RemoveAccent(s).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }

        public string RemoveAccent(string txt)
        {
            string normalizedString = txt.Normalize(NormalizationForm.FormD);
            StringBuilder stringBuilder = new StringBuilder();

            foreach (char c in normalizedString)
            {
                UnicodeCategory unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString();
        }
    }
}
