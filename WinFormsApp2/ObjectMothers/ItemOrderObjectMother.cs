using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.Builders;

namespace UnitTests.ObjectMothers
{
    public class ItemOrderObjectMother
    {
        public ItemOrderBuilder CreateItemOrder()
        {
            return new ItemOrderBuilder();
        }
    }
}
