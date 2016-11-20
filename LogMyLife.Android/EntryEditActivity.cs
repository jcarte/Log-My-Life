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
using LogMyLife.Domain;
using LogMyLife.Domain.Model;
using a = Android;
using Android.Views.InputMethods;

namespace LogMyLife.Android
{
    [Activity(ScreenOrientation = a.Content.PM.ScreenOrientation.Portrait, Label = "", Icon = "@drawable/icon", Theme = "@style/CustomActionBarTheme",WindowSoftInputMode = SoftInput.AdjustResize)]
    public class EntryEditActivity : Activity
    {
        Entry entry;

        ListView titleFieldList;
        ListView otherFieldList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EntryEdit);

            int entryID = Intent.GetIntExtra("EntryID", -1);//get entryid from intent
            entry = MainController.GetEntry(entryID);//get entry from DB

            if (entry == null)//check that entry returned
                throw new Exception($"Entry not found for EntryID = {entryID}");

            //populate titleFieldList
            titleFieldList = FindViewById<ListView>(Resource.Id.titleFieldList);
            EditFieldAdapter adpaterFL = new EditFieldAdapter(this, entry.TitleData, true);
            titleFieldList.Adapter = adpaterFL;


            //populate otherFieldList
            otherFieldList = FindViewById<ListView>(Resource.Id.otherFieldList);//TODO EditText fields aren't showing right,
            EditFieldAdapter adpaterOFL = new EditFieldAdapter(this, entry.OtherData, true);
            otherFieldList.Adapter = adpaterOFL;
            //adpaterOFL.ItemUpdated += AdpaterItemUpdated;

            Button btnSave = FindViewById<Button>(Resource.Id.btnSave_EE);
            Button btnCancel = FindViewById<Button>(Resource.Id.btnCancel_EE);

            btnSave.Click += SaveClicked;
            btnCancel.Click += CancelClicked;

        }


        private void CancelClicked(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            //TODO if start editing field then click save, doesn't update changes, below doesn't work because adapter doesn't update list
            ////Check title fields are up to date

            //Have they entered anything in the title? cancel if not
            if(((EditFieldAdapter)titleFieldList.Adapter).Items.Sum(i=>i.Value.Length) == 0)
            {
                Toast.MakeText(this, "Enter a value in the summary heading", ToastLength.Long).Show();
                return;
            }

            foreach (var item in ((EditFieldAdapter)titleFieldList.Adapter).Items)
            {
                string val;
                if (entry.TitleData.TryGetValue(item.Key, out val) && val != item.Value)
                    entry.EditColumnData(item.Key, item.Value);
            }

            ////Check other fields are up to date
            foreach (var item in ((EditFieldAdapter)otherFieldList.Adapter).Items)
            {
                string val;
                if (entry.OtherData.TryGetValue(item.Key, out val) && val != item.Value)
                    entry.EditColumnData(item.Key, item.Value);
            }

            MainController.UpdateEntry(entry);
            Toast.MakeText(this, "Saved", ToastLength.Short).Show();
            base.OnBackPressed();
        }
        
    }
}