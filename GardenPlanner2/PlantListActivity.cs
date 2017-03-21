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
        //string to store plant list data
        private string data;
        //string to store incoming plant data if updated
        private string dataNew;
        //declares the plant list object
        private PlantList plantList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //set the layout to a list view
            SetContentView(Resource.Layout.PlantListView);


            //find the list view and make an object
            ListView listView = new ListView(this);

            dataNew = Intent.GetStringExtra("plantFilePath") ?? "No data file";

            if (data != dataNew)
            {
                data = dataNew;
            }

            //call the method for streamreader to deserialize json
            plantList = Get_File(data);
            //and then put each item in the list class instance into a listview thing
            PlantAdapter plantAdapter = new PlantAdapter(this, plantList);

            ListView.Adapter = plantAdapter;
            ListView.ItemClick += ListView_ItemClick;
        
        }

        private void ListView_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //finds the item clicked and sets the plant name to a string
            string name = plantList.Items[e.Position].PlantName;
            //uses the plant name to find the item in the json file
            
            

            //opens uses an intent to open the ViewPlantActivity and send a json string file
            StartActivity(SwitchActivity.intent(this, typeof(ViewPlantActivity), data, name));
        }

        private PlantList Get_File(string file)
        {
            using (var streamReader = new StreamReader(data))
            {
                //reads the file and then sets it to a string
                string content = streamReader.ReadToEnd();
                //get the list out of the file
                plantList = JsonConvert.DeserializeObject<PlantList>(content);
            }
            return plantList;
        }
    }
}