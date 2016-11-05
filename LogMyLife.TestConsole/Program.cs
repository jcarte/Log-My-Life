using LogMyLife.Domain.Test.ModelTests;
using LogMyLife.TestConsole.ModelTests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogMyLife.TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //CategoryTest.RunTest();
            //EntryTest.RunTest();
            ColumnTest.RunTest();
            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
