using MVVMApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            var r = new HotpepperClient().SearchAsync(35.669220, 139.761457).Result;
            foreach (var shop in r.results.shop)
            {
                Console.WriteLine($"{shop.name} {shop.name_kana}");
            }
        }
    }
}
