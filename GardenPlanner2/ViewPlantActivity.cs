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
        //declares a string for the data file coming with the user this page
        private string _data;
        private string _plantName;
        //declares the custom intent object
        SwitchActivity switchActivity = new SwitchActivity();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here

            //set the layout view to use
            SetContentView(Resource.Layout.ViewPlantInstance);

            //get the data sent from the last activity
            _data = Intent.GetStringExtra("plantFilePath") ?? "Data not available";
            _plantName = Intent.GetStringExtra("PlantName") ?? "Data not available";
            _plantName = Current_Plant_Name(_plantName);
            


            //grabs the text view for the plant name
            TextView plantName = FindViewById<TextView>(Resource.Id.view_plant_name);
            //sets the plant name as a string in the text view
            plantName.Text = _plantName;

            //grabs the button that sends the user to the list view
            Button linkToList = FindViewById<Button>(Resource.Id.view_to_list);
            linkToList.Click += Open_List;

            //grabs the button that takes the user back to the edit plant view
            Button editPlant = FindViewById<Button>(Resource.Id.view_edit);
            editPlant.Click += Open_Edit;

        }

        //opens the edit platn view
        private void Open_Edit(object sender, EventArgs e)
        {

            //calls start activity on the custom intent object and sends the data along with the user to the edit plant view
            StartActivity(SwitchActivity.intent(this, typeof(MainActivity), _data));

        }

        //opens the list view
        private void Open_List(object sender, EventArgs e)
        {
            ////calls start activity on the custom intent object and sends the data along with the user to the plant list view
            StartActivity(SwitchActivity.intent(this, typeof(PlantListActivity), _data));
        }

        private string Last_Plant_Name()
        {

            //creates a new streamreader and tells it what file to read

            using (StreamReader streamReader = new StreamReader(_data))
            {
                //reads the file and then sets it to a string
                string content = streamReader.ReadToEnd();
                PlantList plantList = JsonConvert.DeserializeObject<PlantList>(content);

                //need to get latest plant object out of list.
                string name = plantList.Items[plantList.Items.Count - 1].PlantName;

                //return name;
                return name;
            }

        }

        private string Current_Plant_Name(string name)
        {
            using (StreamReader streamReader = new StreamReader(_data))
            {
                string content = streamReader.ReadToEnd();
                PlantList plantList = JsonConvert.DeserializeObject<PlantList>(content);
                string plantName;

                if(Intent.GetStringExtra("PlantName") == "Data not available")                {
                    //need to get latest plant object out of list.
                    var plant = plantList.Items[plantList.Items.Count - 1];
                    plantName = plant.PlantName.ToString() ;
                }else                {
                    var plant = plantList.Items.Find(Items => Items.PlantName == name);
                    plantName = plant.PlantName.ToString();
                }
                return plantName;
            }

        }
    }
}