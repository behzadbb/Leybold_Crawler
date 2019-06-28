using System;
using System.Collections.Generic;
using System.Text;

namespace LeyboldCrawler.Model
{
    public class LProduct
    {
        public string URL { get; set; }  
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string Property { get; set; }
        public string[] Keywords { get; set; }
        public string Html { get; set; }
        public string HtmlEn { get; set; }
        public DateTime? CreateDate { get; set; }
        public string Image { get; set; }
        public List<LeyboldImage> Images { get; set; }
        public List<ProductCatalog> Catalog { get; set; }
        public string Category { get; set; }
    }
    public class ProductCatalog
    {
        public string Title { get; set; }
        public string TitleEn { get; set; }
        public string File { get; set; }
    }
    public class LeyboldImage
    {
        public string Large { get; set; }
        //public string Medium { get; set; }
        public string Small { get; set; }
        public string Orginal { get; set; }
        public string Title { get; set; }
    }

}
