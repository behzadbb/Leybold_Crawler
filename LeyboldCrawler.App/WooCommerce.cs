using System;
using System.Collections.Generic;
using System.Text;
using WooCommerceNET;
using WooCommerceNET.WooCommerce.v3;
using WooCommerceNET.WooCommerce.v3.Extension;
using Newtonsoft.Json;
using RestSharp;
using LeyboldCrawler.Model;
using System.Linq;
using WooCommerceNET.Base;


namespace LeyboldCrawler.App
{
    public class WooCommerce : IDisposable
    {
        private static RestAPI rest = new RestAPI("http://localhost/shop1/wp-json/wc/v3/", "ck_b814b6db891255cbe53918f6a7f9b32995fa89a5", "cs_536f214e0a4c372498f6c5d39bb8862df34ddcf0");
        private static WCObject wc = new WCObject(rest);
        public async void createPost(LProduct lProduct)
        {
            //Get all products
            //var products = await wc.Product.GetAll();

            List<ProductImage> productImages = new List<ProductImage>();
            int i = 0;
            foreach (var image in lProduct.Images)
            {
                ProductImage pi = new ProductImage();
                pi.date_created = DateTime.Now;
                pi.src = image.Large;
                pi.alt = lProduct.Title;
                pi.date_created = DateTime.Now.AddMinutes(-10);
                pi.name = lProduct.TitleEn + "_" + lProduct.Title + "_" + i++;

                productImages.Add(pi);
            }

            List<ProductTagLine> productTags = new List<ProductTagLine>();
            foreach (var tag in lProduct.Keywords)
            {
                var tagResult = wc.Tag.Get(tag);
                var t = new ProductTagLine();
                if (tagResult!=null)
                {
                    t.id = tagResult.Id;
                }
                else
                {
                    ProductTag newTag = new ProductTag();
                    newTag.name = tag;
                    t.id = (wc.Tag.Add(newTag)).Id;
                }
                productTags.Add(t);
            }

            var vacCatId = wc.Category.Get("vacuumpump").Id;
            ProductCategory pc = new ProductCategory() { name = lProduct.Category, parent = vacCatId };
            var getddd = wc.Category.Add(pc);


            //Add new product
            var p = new WooCommerceNET.WooCommerce.v3.Product()
            {
                name = lProduct.Title,
                images = productImages,
                description = string.IsNullOrWhiteSpace(lProduct.Html) ? string.Empty : $"<div>{lProduct.Html}</div><br /><div style='direction:ltr !important;text-align: left;'><h2>Description</h2>{lProduct.HtmlEn}</div>",
                price = 0,
                tags = productTags,
                date_created = DateTime.Now.AddHours(-5),
                date_on_sale_from = DateTime.Now.AddHours(-3),
                categories = new List<ProductCategoryLine>() { new ProductCategoryLine { name = lProduct.Category } }
            };
            await wc.Product.Add(p);
        }

        public async void createCatgory(string name)
        {
            ProductCategory category = new ProductCategory();
            category.name = name;
            var result = await wc.Category.Add(category);
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
        // ~WooCommerce()
        // {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion

    }
}
