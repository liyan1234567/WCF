using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFtest1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
            client.DeleteFile("C:\\Users\\FUYU\\Desktop\\3dsbanner");
        }
    }
}
