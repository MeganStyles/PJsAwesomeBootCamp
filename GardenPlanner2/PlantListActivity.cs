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
using Android.Util;
using Newtonsoft.Json;
using System.IO;

namespace GardenPlanner2
{
    [Activity(Label = "PlantListActivity")]
    public class PlantListActivity : ListActivity
    {
        private string data;
        private PlantList plantList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //set the layout to a list view
            SetContentView(Resource.Layout.PlantListView);


            //find the list view and make an object
            ListView listView = new ListView(this);

            data = Intent.GetStringExtra("plantFilePath") ?? "Data not available";

            using (var streamReader = new StreamReader(data))
            {
                //reads the file and then sets it to a string
                string content = streamReader.ReadToEnd();
                //get the list out of the file
                plantList = JsonConvert.DeserializeObject<PlantList>(content);
            }

            //and then put each item in the list class instance into a listview thing
            PlantAdapter plantAdapter = new PlantAdapter(this, plantList);

            ListView.Adapter = plantAdapter;

    
         
        }
    }
}