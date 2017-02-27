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
    public class PlantAdapter : BaseAdapter<Plant>
    {
        PlantList plantList;
        Activity context;
        public PlantAdapter(Activity context, PlantList plantList) : base()
        {
            this.context = context;
            this.plantList = plantList;
        }
        public override long GetItemId(int position)
        {
            return position;
        }
        public override Plant this[int position]
        {
            get
            {
                return plantList.Items[position];
            }
        }
        public override int Count
        {
            get
            {
                return plantList.Items.Count;
            }
        }
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View view = convertView; //re-use an existing view, if one is available
            if (view == null)
            { //or create a new view
                view = context.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem1, null);
            }
                Plant plantName = plantList.Items[position];
                view.FindViewById<TextView>(Android.Resource.Id.Text1).Text = plantName.PlantName;
                return view;
            
        }
    }
}