using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using WebServices.Services;

namespace WebServices.Test.Clients
{
    public class LocationServiceTestClient
    {
        ServiceHost oServiceHost = null;

        public void StartService()
        {
            Uri oBaseAddress = new Uri("http://webservices.daveajrussell.com/Services/LocationService.svc");
            oServiceHost = new ServiceHost(typeof(LocationService), oBaseAddress);
            oServiceHost.Open();
        }

        public void StopService()
        {
            if (oServiceHost.State != CommunicationState.Closed)
                oServiceHost.Close();
        }

        static void Main(string[] args)
        {
            LocationServiceTestClient oProgram = new LocationServiceTestClient();
            System.Console.WriteLine("Starting Location Service");
            oProgram.StartService();
            System.Console.WriteLine("Press any key to Stop the Location Service");
            System.Console.ReadKey();
            oProgram.StopService();
        }
    }
}
