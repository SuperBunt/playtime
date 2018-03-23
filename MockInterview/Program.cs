using AngleSharp;
using MockInterview;
using System;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Schema;

public class Program
{

    delegate bool DelegateRange(int price);
    static void Main()
    {
        string url = "http://www.daft.ie/dublin/apartments-for-rent/dublin-8/clancy-quay-by-kennedy-wilson-dublin-8-dublin-1706149/";
        Console.WriteLine(url);
        //url = Console.ReadLine();
        //getHouseDetail(url).Wait();
        PdfGenerator myPdf = new PdfGenerator();
        House details = getHouseDetail(url).Result;
        myPdf.GeneratePdf(details);
        //int x;
        //x = Convert.ToInt32(Console.ReadLine());
        //Product prod1 = new Product(10, 1);
        //Product prod2 = new Product(60, 1);
        //Product prod3 = new Product(10, 2);
        //Product prod4 = new Product(60, 2);
        //var validate = new Validate(); // Publisher
        //var validationService = new ValidationService(); // subscriber

        //validate.PriceChecking += validationService.PriceChecking;
        //List<Product> list = new List<Product> { prod1, prod2, prod3, prod4 };
        //list.ForEach(p => validate.CheckPrice(p));

        //Console.WriteLine("Press Enter to exit:");
        //Console.ReadLine();
    }

    public class Product
    {
        public int Price { get; set; }
        public int Type { get; set; }
        public Product(int price, int type)
        {
            this.Price = price;
            this.Type = type;
        }
    }

    

    static async Task<House> getHouseDetail(string url)
    {
        // Setup the configuration to support document loading
        var config = Configuration.Default.WithDefaultLoader();
        // Asynchronously get the document in a new context using the configuration

        var document = await BrowsingContext.New(config).OpenAsync(url);
        House house = new House();
        house.Address = document.QuerySelector("#address_box > div.smi-object-header > h1").TextContent.Trim();
        var photos = document.QuerySelectorAll("#smi-content > div.smi-gallery > ul > li > span > img");
        string img;
        foreach(var src in photos)
        {
            img = src.OuterHtml.ToString().Split('"', '"')[1];
            house.Photos.Add(img);
        }
        house.Price = document.QuerySelector("#smi-price-string").TextContent;
        house.Description = document.QuerySelector("#description").TextContent;
        house.MainPhoto = document.QuerySelector("#smi-gallery-img-main > span > img").OuterHtml.ToString().Split('"', '"')[1];
        var items = document.QuerySelectorAll(".header_text");
        house.BriefFeatures = String.Join(" | ", items.Select(x =>x.TextContent).ToArray());
        
        Console.WriteLine(house.ToString());
        return house;

    }

    public class Validate
    {
        public event EventHandler<PriceCheckEventArgs> PriceChecking;

        public void CheckPrice(Product product)
        {
            OnCheckPrice(product);
        }

        protected virtual void OnCheckPrice(Product product)
        {
            PriceChecking?.Invoke(this, new PriceCheckEventArgs() { Product = product });
        }

    }


    public class PriceCheckEventArgs : EventArgs  // derived from EventArgs
    {
        public Product Product { get; set; }
    }

}