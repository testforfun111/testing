using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;

namespace UnitTests.ObjectMothers
{
    public class ProductObjectMother
    {
        public ProductBuilder CreateProduct()
        {
            return new ProductBuilder();
        }
    }
}
