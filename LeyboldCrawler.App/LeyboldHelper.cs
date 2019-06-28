using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using LeyboldCrawler.Model;
using System.Linq;
using LeyboldCrawler.App.Translate;
using LeyboldCrawler.Model.Translate;

namespace LeyboldCrawler.App
{
    public class LeyboldHelper : IDisposable
    {
        ITranslate translate = new GoogleTranslate();
        public LProduct GetProduct(string url)
        {
            LProduct product = new LProduct();
            HtmlDocument doc = new HtmlDocument();
            using (HtmlHelper html = new HtmlHelper())
            {
                doc = html.GetDocument(url);
            }
            var pagewrap = doc.DocumentNode.SelectSingleNode("//body//div[@class='page-wrap']");
            //var pro = pagewrap.SelectSingleNode("//section[@class='content-main']");
            var content = pagewrap.ChildNodes["section"].SelectSingleNode("//div[@class='content-main--inner']//div[@class='content--wrapper']//div[@class='content product--details']");
            var divDetails = content.SelectSingleNode("//div[@class='product--detail-upper block-group']");

            #region images
            var divImages = divDetails.SelectSingleNode("//div[@class='product--image-container image-slider product--image-zoom']");

            var imageMain = divImages.SelectNodes("//div[@class='image-slider--container']//div[@class='image-slider--slide']//div[@class='image--box image-slider--item']//span[@class='image--element']");
            List<LeyboldImage> productImages = new List<LeyboldImage>();
            foreach (var item in imageMain.Where(x => x.HasAttributes))
            {
                LeyboldImage image = new LeyboldImage();
                image.Large = item.Attributes["data-img-large"].Value;
                image.Small = item.Attributes["data-img-small"].Value;
                image.Orginal = item.Attributes["data-img-original"].Value;
                image.Title = item.Attributes["data-alt"].Value;
                productImages.Add(image);
            }

            //var imageThumbnails = divImages.SelectNodes("//div[@class='image-slider--thumbnails-slide']//img");
            //List<string> imgs = new List<string>();
            //foreach (var item in imageThumbnails)
            //{
            //    imgs.Add(item.Attributes["srcset"].Value.Replace(",", ""));
            //}
            #endregion

            #region Title Buyer
            product.TitleEn = divDetails.SelectSingleNode("//div[@class='product--buybox block']//header[@class='product--header']//div[@class='product--info']//h1")
                .InnerText.Replace("\n", " ").Replace("  ", " ").Trim();
            product.Title = "پمپ وکیوم " + product.TitleEn;
            product.Property = divDetails.SelectSingleNode("//div[@class='product--buybox block']//header[@class='product--header']//div[@class='product--info']//div[@class='product--properties']").InnerHtml.Replace("\n", "").Trim();
            #endregion

            #region Tabs

            var divTabs = content.SelectSingleNode("//div[@class='em-tabs']");
            //foreach (HtmlTextNode node in textNodes)
            //    node.Text = node.Text.Replace(node.Text, translate.Translate(node.Text).Result);

            var tabs = divTabs.SelectNodes("div");
            
            foreach (var item in tabs)
            {
                if (!string.IsNullOrWhiteSpace(item.InnerHtml.Trim()))
                {
                    using (HtmlHelper html = new HtmlHelper())
                    {
                        string tabName = item.Attributes["id"].Value.Replace("\n", "").Trim();
                        if (tabName.ToLower() == "description")
                        {
                            string text = html.StripHTML(item.InnerText.Trim()).Replace("&nbsp;", " ");
                            string[] paraphs = text.Split('\n');
                            product.Html = "<p>" + translate.Translate(new InputTranslate { Input = text }).Result + "</p>";
                            product.HtmlEn = "<p>" + string.Join("<p> </p>", paraphs) + "</p>";
                        }
                    }
                }
            }

            var sssss = divTabs.SelectNodes("//div[@id='downloads']//ul//li//a");
            List<ProductCatalog> catalogs = new List<ProductCatalog>();
            foreach (var item in sssss)
            {
                ProductCatalog catalog = new ProductCatalog();
                catalog.File = item.Attributes["href"].Value;
                catalog.TitleEn = item.InnerText.Replace("\n"," ").Trim();
                catalog.Title = translate.Translate(new InputTranslate { Input = catalog.TitleEn }).Result;
                catalogs.Add(catalog);
            }
            #endregion

            product.URL = url;
            product.Catalog = catalogs;
            //List<string> imgss = new List<string>();
            //int i = 0;
            //foreach (var img in imgs)
            //{
            //    var _temp = img.Split(' ').Where(x => x.Contains("http")).Distinct();
            //    imgss.AddRange(_temp);
            //}
            product.Images = productImages;
            product.Keywords = url.Replace("https://www.leyboldproducts.com/products/", "").Replace("/pumps/", "/").Split('/');
            product.Category = url.Replace("https://www.leyboldproducts.com/products/", "").Replace("/pumps/", "/").Split('/').FirstOrDefault();


            return product;
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~LeyboldHelper()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
