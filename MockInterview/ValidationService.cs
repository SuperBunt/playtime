using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Program;

namespace MockInterview
{
    public class ValidationService
    {
        private struct Validator1
        {
            private int Min => 0;
            private int Max => 50;

            public bool Validate(int price)
            {
                return InRange(price, Min, Max);
            }
        }

        private struct Validator2
        {
            private int Min => 51;
            private int Max => 100;
            public bool Validate(int price)
            {
                return InRange(price, Min, Max);
            }
        }

        public void PriceChecking(object source, PriceCheckEventArgs args)
        {
            Console.WriteLine($"Validation service. Product type: {args.Product.Type}\tPrice: {args.Product.Price} ");
            //var validator = args.Product.Type == 1 ? typeof(Validator1) : typeof(Validator2);
            if (args.Product.Type == 1)
            {
                var validator = new Validator1();
                Console.WriteLine(validator.Validate(args.Product.Price) ? "Price is good" : "Price is bad");
            }
            else
            {
                var validator = new Validator2();
                Console.WriteLine(validator.Validate(args.Product.Price) ? "Price is good" : "Price is bad");
            }
        }

        public static bool InRange(int price, int min, int max) => price >= min && price < max;
    }
}
