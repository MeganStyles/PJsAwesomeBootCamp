using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;

namespace GardenPlanner2    {

    public class SwitchActivity : Intent    {

        //Intent that sends only data file
        public static Intent intent(Context context, Type openActivity, string data)    {            
            Intent intent = new Intent(context, openActivity);
            intent.PutExtra("plantFilePath", data);
            return intent;                 
        }

        //Intent that sends data file as well as current position in plantlist of data
        public static Intent intent(Context context, Type openActivity, string data, PlantList plant)    {
            PlantList plantList = new PlantList();
            Intent intent = new Intent(context, openActivity);
            intent.PutExtra("plantFilePath", data);
            intent.PutExtra("postion", plant.Items);
            return intent;
        }       
     }
 }

       
       

    
