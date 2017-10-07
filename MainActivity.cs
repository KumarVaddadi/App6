using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Preferences;
using System.Json;
using static Android.Resource;

namespace App5
{
    [Activity(Label = "Consist Management", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
      
        private List<Consist> itemlist;
        DataClient dc;
        private ListView listCars;
        protected override async void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

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
            listCars = FindViewById<ListView>(Resource.Id.lstCars);
            itemlist = new List<Consist>();

            dc = new DataClient();
            var lstConNames = await dc.GetFleetData("Name");
            var lstSoldTime = await dc.GetFleetData("SoldTime");
            var lstGPRSIpAddress = await dc.GetFleetData("GPRSIpAddress");
            var lstNrOfCars = await dc.GetFleetData("NrOfCars");
            var lstIpAddress = await dc.GetFleetData("IpAddress");
            var lstStateId = await dc.GetFleetData("StateId");
            var lstVehiclePos = await dc.GetFleetData("VehiclePos");
            var lst = itemlist;
            for (int i = 0; i < lstConNames.Count; i++)
            {
                //DateTime.Parse(lstSoldTime[i])
                itemlist.Add(new Consist
                {
                    Name = lstConNames[i],
                    SoldTime = ConvertDt_string(lstSoldTime[i]),
                    NrOfCars = Convert2Int(lstNrOfCars[i]),
                    StateId = Convert2Int(lstStateId[i]),
                    GPRSIpAddress = lstGPRSIpAddress[i],
                    IpAddress = lstIpAddress[i],
                    VehiclePos = Convert2Int(lstVehiclePos[i])
                });

            }

            //contents = new List<App3.CarManageDto>(jsonCatalogContent);
            ArrayAdapter<string> adapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, lstConNames);
            listCars.Adapter = adapter;

            listCars.ItemClick += listCars_ItemClick;

            listCars.LongClick += ListCars_LongClick1;
        }

        private void ListCars_LongClick1(object sender, View.LongClickEventArgs e)
        {
            Android.App.AlertDialog.Builder dialog = new AlertDialog.Builder(this);
            AlertDialog alert = dialog.Create();
            alert.SetTitle("Alert Box");
            alert.SetMessage("Long Pressed");
            alert.SetButton("OK", (c, ev) =>
            {
                // Ok button click task  
            });
            alert.Show();
        }


        public int Convert2Int(string str)
        {
            if (str == "")
                return 0;
            return Convert.ToInt32(str);
        }

        public DateTime? ConvertDt_string(string dt)
        {
            if (dt == "")
                return null;

            return DateTime.Parse(dt);

        }
        private void listCars_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            //itemlist[e.Position].Name
            List<Consist> singleList = new List<Consist>();
            singleList.Add(new Consist
            {
                Name = itemlist[e.Position].Name,
                SoldTime = itemlist[e.Position].SoldTime,
                NrOfCars = itemlist[e.Position].NrOfCars,
                StateId = itemlist[e.Position].StateId,
                GPRSIpAddress = itemlist[e.Position].GPRSIpAddress,
                IpAddress = itemlist[e.Position].IpAddress,
                VehiclePos = itemlist[e.Position].VehiclePos
            });
            //singleList.Add(itemlist.)

            MyListViewAdapter adapter = new MyListViewAdapter(this, singleList);
            listCars.Adapter = adapter;
        }


        public override void OnBackPressed()
        {
            var intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);

            //base.OnBackPressed(); -> DO NOT CALL THIS LINE OR WILL NAVIGATE BACK
        }


    }
}

