using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFTest.Models;

namespace EFTest
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new SentinelContext()) {
                GEOGRAPHICAL_INFORMATION oGIS = new GEOGRAPHICAL_INFORMATION();
                oGIS.UPDATE_DATE_TIME = DateTime.Now;
                oGIS.LATITUDE = 12.123M;
                oGIS.LONGITUDE = -12.123M;
                oGIS.SPEED = 25;
                oGIS.ORIENTATION = 1;

                db.GEOGRAPHICAL_INFORMATION.Add(oGIS);
                db.SaveChanges();

                var query = from gis in db.GEOGRAPHICAL_INFORMATION
                            select gis;

                foreach (var gisItem in query)
                {
                    Console.WriteLine(gisItem.LATITUDE + " " + gisItem.LONGITUDE);
                }
                Console.ReadLine();
            }
        }
    }
}
