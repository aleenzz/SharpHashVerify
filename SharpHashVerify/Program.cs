using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace SharpHashVerify
{
    class Program
    {
        static void Main(string[] args)
        {
            var req = HttpHelper.doGet("https://192.168.129.8/ews/", "administrator", "B3255351D8DFE7CDEDF3F552A49146D6");
            var req123 = HttpHelper.doGet("https://192.168.129.8/ews/", "administrator", "23255351D8DFE7CDEDF3F552A49146D6");
            Console.WriteLine(req123);
            Console.WriteLine(req);
        }
    }
}
