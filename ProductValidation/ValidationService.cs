using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductValidation;
using static ProductValidation.Program;

namespace Validation

{
    public class ValidationService
    {
        public class Service1{
            private int Min => 0;
            private int Max => 50;

            public void PriceChecking(object source, PriceCheckEventArgs args)
            {
                bool inRange = InRange(args.Product.Price, Min, Max);
                Console.WriteLine(inRange);
            }
        }

        public class Service2
        {
            private int Min => 51;
            private int Max => 100;

            public void PriceChecking(object source, PriceCheckEventArgs args)
            {
                bool inRange = InRange(args.Product.Price, Min, Max);
                Console.WriteLine(inRange);
            }
        }
        
        public static bool InRange(int price, int min, int max) => price >= min && price < max;
    }
}
