using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Text;
using LeyboldCrawler.Model;
using System.Linq;

namespace LeyboldCrawler.App
{
    public class LeyboldHelper : IDisposable
    {
        public Product GetProduct(string url)
        {
            Product product = new Product();
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
            var imageThumbnails = divImages.SelectNodes("//div[@class='image-slider--thumbnails-slide']//img");
            List<string> imgs = new List<string>();
            foreach (var item in imageThumbnails)
            {
                imgs.Add(item.Attributes["srcset"].Value.Replace(",",""));
            }
            #endregion
            
            #region Title Buyer
            product.Title = divDetails.SelectSingleNode("//div[@class='product--buybox block']//header[@class='product--header']//div[@class='product--info']//h1").InnerText.Replace("\n","").Trim();
            product.Property = divDetails.SelectSingleNode("//div[@class='product--buybox block']//header[@class='product--header']//div[@class='product--info']//div[@class='product--properties']").InnerHtml.Replace("\n", "").Trim();
            #endregion

            #region Tabs
            var divTabs = content.SelectSingleNode("//div[@class='em-tabs']");
            var tabs = divTabs.SelectNodes("div");
            List<ProductTabs> productTabs = new List<ProductTabs>();
            foreach (var item in tabs)
            {
                if (!string.IsNullOrWhiteSpace(item.InnerHtml.Trim()))
                {
                    ProductTabs tab = new ProductTabs();
                    tab.TabName = item.Attributes["id"].Value;
                    tab.HTML = item.InnerHtml.Replace("\n", "").Trim();
                    productTabs.Add(tab);
                }
            }

            var sssss = divTabs.SelectNodes("//div[@id='downloads']//ul//li//a");
            List<string> catalogs = new List<string>();
            foreach (var item in sssss)
            {
                catalogs.Add(item.Attributes["href"].Value);
            }
            #endregion

            product.URL = url;
            product.Tabs = productTabs;
            product.Catalog = catalogs.ToArray();
            List<string> imgss = new List<string>();
            foreach (var img in imgs)
            {
                var _temp = img.Split(' ').Where(x => x.Contains("http")).Distinct();
                imgss.AddRange(_temp);
            }
            product.Images = imgss.ToArray();
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
