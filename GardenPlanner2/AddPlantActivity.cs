using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Util;
using Newtonsoft.Json;
using System.IO;
using System;

namespace GardenPlanner2
{
    [Activity(Label = "AddPlantActivity", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        //initialises a plant object
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
            //when the save button is clicked go to view plant activity
            save.Click += Save_Click;

            // create an object for the "return button"
            Button returnToList = FindViewById<Button>(Resource.Id.to_list);
            // when the button is clicked go back to the list view
            returnToList.Click += Return_List_Click;



        }

        //grabs the edit text entered by user and turns into a plant object.name
        private void NewPlantName_TextChanged(object sender, Android.Text.TextChangedEventArgs e)
        {
            string plantName = e.Text.ToString();
            plantObject.PlantName = plantName;
        }

        private void Return_List_Click(object sender, EventArgs e)
        {

            //to do: just sends the file without saving a new one. 
            //doesn't need to send data file again - no new plant item
            //need a second inent constructor for just a normal intent.
            StartActivity(SwitchActivity.intent(this, typeof(PlantListActivity), File_Path()));
        }

        private void Save_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(plantObject.PlantName))
            {
                //to do: calls the save function (need to make)
                Save_File(File_Path());
                //opens uses an intent to open the ViewPlantActivity and send a json string file
                StartActivity(SwitchActivity.intent(this, typeof(ViewPlantActivity), File_Path()));

            } else
            {
                Toast.MakeText(this, "Do you really want to save an invisible plant?", ToastLength.Short).Show();
                //To do: make multiple toast messages a different one per click.
            }
            
                        
        }

       

        //Grabs the json file path string sent with the intent and deserializes it into a string
        private void Save_File(string fileName)
        {

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
            
            
                //to do: write toast message - "you can't have a plant with no name"
                using (var streamWriter = new StreamWriter(fileName, false))
                {
                    //writes the new plant object instance to a json file
                    streamWriter.WriteLine(JsonConvert.SerializeObject(plantItems));
                }
            
                       
           
        }

        private string File_Path()
        {
            //To do: put this in a seperate function
            //find the place to save json files and create a new folder
            string plantFiles = BaseContext.GetDir("PlantsFile", 0).AbsolutePath;
            //Name the new json file
            string fileName = Path.Combine(plantFiles, "PlantsFile.json");
            return fileName;
        }

    }
}

