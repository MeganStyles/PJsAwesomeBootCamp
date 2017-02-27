using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Newtonsoft.Json;
using System.IO;

namespace GardenPlanner2
{
    [Activity(Label = "GardenPlanner2", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        private Plant plantObject = new Plant();


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "AddPlant" layout resource
            SetContentView(Resource.Layout.AddPlant);

            // create an object for the "edit" text
            EditText newPlantName = FindViewById<EditText>(Resource.Id.new_plant_name);
            //when the text is changed in the edit text create a string "name"
            newPlantName.TextChanged += NewPlantName_TextChanged;

            // create an object for the "save button"
            Button save = FindViewById<Button>(Resource.Id.save_button);
            //when the save button is clicked do something
            save.Click += Save_Click;


        }

        private void Save_Click(object sender, System.EventArgs e)
        {

            //find the place to save json files and create a new folder
            string plantFiles = BaseContext.GetDir("PlantsFile", 0).AbsolutePath;
            //Name the new json file
            string fileName = Path.Combine(plantFiles, "PlantsFile.json");

            PlantList plantItems;

            if (File.Exists(fileName))
            {
                //read the stuff out of the file and put it into the plant items list

                using (var streamReader = new StreamReader(fileName))
                {
                    //reads the file and then sets it to a string
                    string content = streamReader.ReadToEnd();
                    plantItems = JsonConvert.DeserializeObject<PlantList>(content);
                }

            }
            else
            {
                plantItems = new PlantList();
            }


            //put plantObject in List
            plantItems.Items.Add(plantObject);
            //write list to json
            //creates a streamWriter and uses above filepath to write file
            using (var streamWriter = new StreamWriter(fileName, false))
            {
                //writes the new plant object instance to a json file
                streamWriter.WriteLine(JsonConvert.SerializeObject(plantItems));
            }

            //on save click opens view plant screen.
            Intent intent = new Intent(this, typeof(ViewPlantActivity));
            //passes the json file just created to the new activity
            intent.PutExtra("plantFilePath", fileName);
            StartActivity(intent);
        }


        private void NewPlantName_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            string plantName = e.Text.ToString();
            plantObject.PlantName = plantName;
        }
    }
}

