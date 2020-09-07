using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace lesson1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(FlyService));
            host.Open();
            Console.WriteLine("WCF启动成功");
            Console.ReadKey();

        }
    }
}
