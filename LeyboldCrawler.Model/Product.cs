using System;
using System.Collections.Generic;
using System.Text;

namespace LeyboldCrawler.Model
{
    public class Product
    {
        public string URL { get; set; }  
        public string Title { get; set; }
        public string Property { get; set; }
        public string[] Keywords { get; set; }
        public string Html { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Image { get; set; }
        public string[] Images { get; set; }
        public string[] Catalog { get; set; }
        public string Category { get; set; }
        public List<ProductTabs> Tabs { get; set; }
    }
    public class ProductTabs
    {
        public string TabName { get; set; }
        public string HTML { get; set; }
        public string Text { get; set; }
    }
}
