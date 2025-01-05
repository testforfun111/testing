using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
namespace UnitTests.Data
{
    public interface dbContextFactory
    {
        DataContext get_db_context();
    }
}
