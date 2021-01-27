using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHubs_Example
{
    class Program
    {
        static void Main(string[] args)
        {
            //LoadData();
            //SendData().Wait();
            SendEvent.LoadData();
            SendEvent.sendEvent().Wait();
        }
    }
}
