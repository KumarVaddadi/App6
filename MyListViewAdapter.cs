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
using System.Globalization;

namespace App5
{
    public class MyListViewAdapter :BaseAdapter<Consist>
    {
        private List<Consist> listItems;
        private Context mContext;

        public MyListViewAdapter(Context context, List<Consist> list)
        {
            listItems = list;
            mContext = context;
        }

        public override int Count
        {
            get { return listItems.Count; }
        }

        public override Consist this[int position] => listItems[position];

        public override long GetItemId(int position)
        {
            return position;
        }

        //public override string this[int position] => listItems[position];
        /*
            *<ConsistManageDto>
                   <BusVersion i:nil="true"/>
                   <GPRSIpAddress>10.28.85.183</GPRSIpAddress>
                   <IpAddress>0.0.0.0</IpAddress>
                   <Name>187001</Name>
                   <NrOfCars>1</NrOfCars>
                   <SoldTime>2017-06-05T00:30:00</SoldTime>
                   <StateId>0</StateId>
                   <VehiclePos>0</VehiclePos>
               </ConsistManageDto>
            */
        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View row = convertView;

            if (row == null)
            {
                row = LayoutInflater.From(mContext).Inflate(Resource.Layout.list_row, null, false);
            }

            DateTime? dt = listItems[position].SoldTime;
            
            TextView txtConsitname = row.FindViewById<TextView>(Resource.Id.txtConsistname);
            txtConsitname.Text = listItems[position].Name;
            TextView txtName = row.FindViewById<TextView>(Resource.Id.txtName);
            //txtName.Text = DateTime.ParseExact(listItems[position].SoldTime, "dd/mm/yyyy", CultureInfo.InvariantCulture);
            txtName.Text = Convertdt_string(dt);
            TextView txtPosition = row.FindViewById<TextView>(Resource.Id.txtPosition);
            txtPosition.Text = listItems[position].GPRSIpAddress;
            TextView txtType = row.FindViewById<TextView>(Resource.Id.txtType);
            txtType.Text = listItems[position].IpAddress;
            return row;
        }

        public string Convertdt_string(DateTime? dt)
        {
            var str = dt;
            return dt.ToString();
        }
    }
}