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
    [Activity(ScreenOrientation = a.Content.PM.ScreenOrientation.Portrait, Label = "", Icon = "@drawable/icon", Theme = "@style/CustomActionBarTheme", WindowSoftInputMode = SoftInput.AdjustResize)]
    public class ListEdit : Activity
    {
        //Blank list to put the items in
        List<string> mItems = new List<string>();
        //Blank holder for the listview
        ListView listList;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "ListEdit" layout resource
            SetContentView(Resource.Layout.ListEdit);

            //The call to get all the info on the categories ready to put into the list
            List<Category> cats = MainController.GetCategories();

            //Puts the name from each iteration in GetCategories into mItems
            foreach (var i in cats)
            {
                mItems.Add(i.Name);
            }


            //Define listList ListView to be the list in ListEdit.axml (editListList)
            listList = FindViewById<ListView>(Resource.Id.editListsList);
            listList.ChoiceMode = a.Widget.ChoiceMode.Multiple;

            //Create an adapter to translate what's in mItems into listList
            ArrayAdapter mAdapter = new ArrayAdapter<string>(this, a.Resource.Layout.SimpleListItemMultipleChoice, mItems);

            //Put the adapter into the listview           
            listList.Adapter = mAdapter;

            //Link up buttons
            Button btnEdit = FindViewById<Button>(Resource.Id.btnEdit_LE);
            Button btnDelete = FindViewById<Button>(Resource.Id.btnDelete_LE);

            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
        }


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //TODO: Delete selected lists selected
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            //TODO: Go to Edit page for first selected item (need to do error handling if more than one list selected, 
            //or stop multiple selection above - annnoying for delete)
        }
    }
}