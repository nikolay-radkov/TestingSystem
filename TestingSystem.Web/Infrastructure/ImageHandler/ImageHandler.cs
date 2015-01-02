namespace TestingSystem.Web.Infrastructure.ImageHandler
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Net;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Web;

    public static class ImageHandler
    {
        private const string ClientId = "67216f0a73b7e85";

        public static string UploadImageToImgur(byte[] image)
        {
            WebClient w = new WebClient();
            w.Headers.Add("Authorization", "Client-ID " + ClientId);

            NameValueCollection keys = new NameValueCollection();
            try
            {
                keys.Add("image", Convert.ToBase64String(image));

                byte[] responseArray = w.UploadValues("https://api.imgur.com/3/image", keys);
                dynamic result = Encoding.ASCII.GetString(responseArray);

                Regex reg = new Regex("link\":\"(.*?)\"");
                Match match = reg.Match(result);

                string url = match.ToString().Replace("link\":\"", string.Empty).Replace("\"", string.Empty).Replace("\\/", "/");

                return url;
            }
            catch (Exception s)
            {
                return "error " + s.Message;
            }
        }
    }
}