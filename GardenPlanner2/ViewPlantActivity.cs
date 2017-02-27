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
    [Activity(Label = "ViewPlantActivity")]
    public class ViewPlantActivity : Activity
    {
        private string data;
        SwitchActivity switchActivity = new SwitchActivity();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            //set the layout view to use
            SetContentView(Resource.Layout.ViewPlantInstance);

            //get the data sent from the last activity
            data = Intent.GetStringExtra("plantFilePath") ?? "Data not available";

            TextView plantName = FindViewById<TextView>(Resource.Id.view_plant_name);
            plantName.Text = File_Plant_Name().ToString();

            Button linkToList = FindViewById<Button>(Resource.Id.view_to_list);
            linkToList.Click += Open_List;

            Button editPlant = FindViewById<Button>(Resource.Id.view_edit);
            editPlant.Click += Go_Here(this, typeof(MainActivity), data);
                
                
                
               
                
               




        }

        private void Go_Here(object sender, EventArgs e)
        {
            StartActivity(switchActivity.intent(context, type, file));
            
            
        }

        
       

        private void Open_List(object sender, EventArgs e)
        {
            Intent openList = new Intent(this, typeof(PlantListActivity));
            openList.PutExtra("plantFilePath", data);
            StartActivity(openList);
        }

        private string File_Plant_Name()
        {
            
            //creates a new streamreader and tells it what file to read

            using (var streamReader = new StreamReader(data))
            {
                //reads the file and then sets it to a string
                string content = streamReader.ReadToEnd();
                PlantList plantList = JsonConvert.DeserializeObject<PlantList>(content);
                //need to get latest plant object out of list.
                Plant plant = plantList.Items[plantList.Items.Count - 1];
                //need to get plant name from object
                string name = plant.PlantName;
                //return name;
                return name;
            }

        }

    }
}