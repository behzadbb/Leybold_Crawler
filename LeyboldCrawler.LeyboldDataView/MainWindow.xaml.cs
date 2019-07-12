using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using LeyboldCrawler;
using LeyboldCrawler.App;
using LeyboldCrawler.Model;
using Newtonsoft.Json;

namespace LeyboldCrawler.LeyboldDataView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string fileName = System.IO.Path.GetTempPath() + "behzad.tmp";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int? count = useCount;
            if (count.HasValue && count >= 0 && count < 4)
            {
                List<LProduct> products = new List<LProduct>();
                using (LeyboldHelper leybold = new LeyboldHelper())
                {
                    var pr = leybold.GetProduct(txtUrl.Text);
                    products.Add(pr);
                }
                var j = JsonConvert.SerializeObject(products, Formatting.Indented);
                richText1.Document.Blocks.Clear();
                richText1.Document.Blocks.Add(new Paragraph(new Run(j)));
                MessageBox.Show($"شما {count + 1} از 5 بار از برنامه استفاده کرده اید");
            }
            else
            {
                MessageBox.Show("تعداد دفعات مورد استفاده بیش از حد مجاز بوده است.");
            }
        }

        private string file => File.Exists(fileName) ? getValueFile : createFile;
        private string createFile
        {
            get
            {
                try
                {
                    using (FileStream fs = File.Create(fileName))
                    {
                        Byte[] info = new UTF8Encoding(true).GetBytes("0");
                        fs.Write(info, 0, info.Length);
                    }
                    using (StreamReader sr = File.OpenText(fileName))
                    {
                        string s = "1";
                        while ((s = sr.ReadLine()) != null)
                        {
                            Console.WriteLine(s);
                        }
                    }
                }
                catch (Exception ex)
                {
                }
                return "1";
            }
        }
        private int? useCount
        {
            get
            {
                try
                {
                    if (int.TryParse(file, out int result))
                    {
                        return result;
                    }
                    else
                    {
                        return (int?)null;
                    }
                }
                catch (Exception)
                {
                    return (int?)null;
                }
            }
        }
        private string getValueFile
        {
            get
            {
                string _temp = File.ReadAllText(fileName);
                if (int.TryParse(_temp, out int result))
                {
                    File.WriteAllText(fileName, $"{result + 1}");
                    return _temp;
                }
                else
                {
                    return "";
                }
            }
        }
    }
}