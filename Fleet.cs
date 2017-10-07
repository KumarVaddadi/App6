using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace App5
{
    public class Fleet
    {
    }

    public class Consist
    {
        public string Name { get; set; }
        public int VehiclePos { get; set; }
        public int StateId { get; set; }
        public string IpAddress { get; set; }
        public string GPRSIpAddress { get; set; }
        public string BusVersion { get; set; }
        public int NrOfCars { get; set; }
        public DateTime? SoldTime { get; set; }
    }

    public class Car
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ConsistName { get; set; }
        public int Position { get; set; }
        public string Type { get; set; }
    }

    public class RootObject
    {
        public string Name { get; set; }
        public List<Consist> Consists { get; set; }
        public List<Car> Cars { get; set; }
    }
}