using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lesson1Test
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.FlyServiceClient client = new ServiceReference1.FlyServiceClient();
            Console.Write(client.Fly());
            Console.ReadKey();
        }
    }
}
