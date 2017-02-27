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

namespace GardenPlanner2
{
    public class PlantList
    {
        public PlantList()
        {
            Items = new List<Plant>();
        }

        public List<Plant> Items { get; }
    }
}