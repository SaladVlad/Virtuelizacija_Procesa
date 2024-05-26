﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ServiceHost svc = new ServiceHost(typeof(LibraryService));
            svc.Open();
            Console.WriteLine("Service started...");
            Console.ReadKey();
        }
    }
}
