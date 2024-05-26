using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class Program
    {
        static void Main(string[] args)
        {
            using(ServiceHost svc = new ServiceHost(typeof(ApotekaService)))
            {
                svc.Open();

                Console.WriteLine("Otvoren servis!");
                Console.ReadKey();
                svc.Close();
            }

        }
    }
}
