﻿using LogMyLife.Domain.Test.ModelTests;
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
            CategoryTest.RunTest();
            Console.WriteLine("Done");
            Console.Read();
        }
    }
}
