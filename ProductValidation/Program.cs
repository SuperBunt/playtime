using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validation;
using static ProductValidation.ProductService;
using static Validation.ValidationService;

namespace ProductValidation
{
    public class Program
    {
        static Service1 service1;
        static Service2 service2;
        static AnnouncerService service3;
        static void Main(string[] args)
        {
            Console.WriteLine("Started...");
            Product prod1 = new Product(10, 1);
            Product prod2 = new Product(60, 1);
            Product prod3 = new Product(10, 2);
            Product prod4 = new Product(60, 2);
            Product prod5 = new Product
            {
                Type = 1
            };

            var checker = new PriceChecker(); // Publisher

            service1 = new Service1();
            service2 = new Service2();
            service3 = new AnnouncerService();
            checker.PriceChecking += service3.PriceChecking;    

            List<Product> list = new List<Product> { prod1, prod2, prod3, prod4, prod5 };
            list.ForEach(p => checker.CheckPrice(p));

            Console.WriteLine("Press Enter to exit:");
            Console.ReadLine();
        }
        public class Product
        {
            public Product() { }
            public int Price { get; set; }
            public int Type { get; set; }
            public Product(int price, int type)
            {
                Price = price;
                Type = type;
            }
        }

        // This my publisher
        public class PriceChecker
        {
            public event EventHandler<PriceCheckEventArgs> PriceChecking; // Define an event based on the delegate

            public void CheckPrice(Product product)
            {
                // Product type determines which service
                if (product.Type == 1)
                {
                    PriceChecking += service1.PriceChecking;
                    PriceChecking?.Invoke(this, new PriceCheckEventArgs() { Product = product }); // Raise the event
                    PriceChecking -= service1.PriceChecking;
                }
                else
                {
                    PriceChecking += service2.PriceChecking;
                    PriceChecking?.Invoke(this, new PriceCheckEventArgs() { Product = product });
                    PriceChecking -= service2.PriceChecking;
                }
            }
        }
        public class PriceCheckEventArgs : EventArgs  // derived from EventArgs
        {
            public Product Product { get; set; }
        }

    }
}
