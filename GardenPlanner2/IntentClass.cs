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

    public class SwitchActivity : Intent
    {
        Context context;
        Type type;
        string data;
        public Intent intent(Context context, Type type, string data)
        {
            this.context = context;
            this.type = type;
            this.data = data;

            Intent intent = new Intent(context, type);
            intent.PutExtra("plantFilePath", data);
            return intent;
                 
        }
     }
 }

       
       

    
