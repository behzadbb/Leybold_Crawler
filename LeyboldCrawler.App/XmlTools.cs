using LeyboldCrawler.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LeyboldCrawler.App
{
    public static class XmlTools
    {
        public static string sitemapUrl = "https://www.leyboldproducts.com/sitemap.xml";
        public static async Task<string> LoadSitemap()
        {
            string page = sitemapUrl;
            string result = "";

            using (HttpClient client = new HttpClient())
            using (HttpResponseMessage response = await client.GetAsync(page))
            using (HttpContent content = response.Content)
            {
                result = await content.ReadAsStringAsync();
            }
            return await Task.Run(() =>
            {
                return result;
            });
        }
        public static Urlset getUrls(string xml)
        {
            Urlset reply = new Urlset();
            string xmlClean = xml.Replace("<urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">\n", "<urlset>");
            var ser = new System.Xml.Serialization.XmlSerializer(typeof(Urlset));

            //Replace("</urlset>", "").Trim();

            Stream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlClean));

            reply = (Urlset)ser.Deserialize(stream);
            return reply;
        }
        public static IEnumerable<string> GetUrls
        {
            get
            {
                return File.ReadLines(@"C:\Users\Behzad\Desktop\txt_lebold\Leybold.txt");
            }
        }
    }
}
