﻿using System;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using HtmlAgilityPack;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using LeyboldCrawler.Model;
using LeyboldCrawler.App;
using System.Collections.Generic;
using LeyboldCrawler.Model.Translate;
using LeyboldCrawler.App.Translate;

namespace LeyboldCrawler.Command
{
    class Program
    {
        //ITranslate translate = new TargomanTranslate();
        static async Task Main(string[] args)
        {
            w("Start");
            wl("Enter Command: ");
            string command = Console.ReadLine();
            if (command == "w")
                wp();
            if (command == "c")
                crawler();
            if (command == "t")
                translate();


            Console.ReadKey();
        }
        static void LoadSitemap(string uri)
        {
            //var web = new HtmlWeb();
            //var doc = web.Load(uri);

            //var txt = doc.DocumentNode.SelectSingleNode("//body//div//section");
            //var xxml = "";

            using (var driver = new ChromeDriver(@"C:\Users\Behzad\Desktop\chrom"))
            {
                // Go to the home page
                driver.Navigate().GoToUrl(uri);

                // Get the page elements
                var userNameField = driver.FindElementByClassName("page-wrap");
                //var userPasswordField = driver.FindElementById("pwd");
                //var loginButton = driver.FindElementByXPath("//input[@value='Login']");

                //// Type user name and password
                //userNameField.SendKeys("admin");
                //userPasswordField.SendKeys("12345");

                //// and click the login button
                //loginButton.Click();

                //// Extract the text and save it into result.txt
                //var result = driver.FindElementByXPath("//div[@id='case_login']/h3").Text;
                //File.WriteAllText("result.txt", result);

                //// Take a screenshot and save it into screen.png
                //driver.GetScreenshot().SaveAsFile(@"screen.png", ImageFormat.Png);
            }
        }

        private static void crawler()
        {
            int take = 1;
            //string xml = await XmlTools.LoadSitemap();
            //Urlset urls = XmlTools.getUrls(xml);
            //string[] url = urls.urls.Where(x => x.loc.Contains("/products/")).Select(x => x.loc).ToArray();
            string[] url = XmlTools.GetUrls.Where(x => x.Contains("/products/") && x.Contains("/pumps/") && x.Length > x.IndexOf("/pumps/") + 7).Skip(30).Take(take).Select(x => x).ToArray();
            List<LProduct> products = new List<LProduct>();
            //System.IO.File.WriteAllLines(@"C:\Users\Behzad\Desktop\txt_lebold\Leybold.txt", url);
            wl("Loading ");
            foreach (var item in url)
            {
                int s = item.IndexOf("/pumps/");
                if (item.Length > s + 7)
                {
                    using (LeyboldHelper leybold = new LeyboldHelper())
                    {
                        var pr = leybold.GetProduct("https://www.leyboldproducts.com/products/oil-sealed-vacuum-pumps/vacube/2744/vacube-vq-800?c=16026");
                        products.Add(pr);
                        wl("|");
                    }
                }
            }
            //LoadSitemap("https://www.leyboldproducts.com/products/vacuum-pump-systems/fore-vacuum-pump-systems/ruta-pumpsystems-with/1507/ruta-wau-1001/sv-200/a");
            var j = JsonConvert.SerializeObject(products);
            w("\n");
            w(j);
            w("\n");
            w("\n");
            w("finish");
        }
        private static void wp()
        {
            string[] urla = XmlTools.GetUrls.Where(x => x.Contains("/products/") && x.Contains("/pumps/") && x.Length > x.IndexOf("/pumps/") + 7).Select(x => x).Skip(14).Take(3).ToArray();
            foreach (string url in urla)
            {
                LProduct product = new LProduct();
                using (LeyboldHelper leybold = new LeyboldHelper())
                {
                    product = leybold.GetProduct(url);
                }
                using (WooCommerce woo = new WooCommerce())
                {
                    woo.createPost(product);
                }
            }
        }
        private static void translate()
        {
            using (ITranslate translate = new TargomanTranslate())
            {
                ResultTranslate result = translate.Translate(new InputTranslate { Input = "hello ali" });
                string res = result.Result;
                w(res);
            }
        }
        static async void w(string s)
        {
            Console.WriteLine(s);
        }
        static async void wl(string s)
        {
            Console.Write(s);
        }
    }
}
