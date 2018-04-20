using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ProductValidation.Program;

namespace ProductValidation
{
    class ProductService
    {
        public class AnnouncerService
        {
            public void PriceChecking(object source, PriceCheckEventArgs args)
            {
                Console.WriteLine($"Product type: {args.Product.Type}\tPrice: {args.Product.Price} ");
            }
        }
    }
}
