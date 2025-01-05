using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.Run
{
    public class RandomOrder
    {
        private readonly bool _isRandomOrder;

        public RandomOrder()
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("testsettings.json")
                .Build();

            _isRandomOrder = bool.Parse(config["isRandomOrder"] ?? "false");
        }

        public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases)
        {
            if (_isRandomOrder)
            {
                var random = new Random();
                return testCases.OrderBy(x => random.Next());
            }
            return testCases;
        }
    }
}
