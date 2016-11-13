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
using a = Android;
using LogMyLife.Domain.Model;
using LogMyLife.Domain;

namespace LogMyLife.Android
{
    [Activity(ScreenOrientation = a.Content.PM.ScreenOrientation.Portrait, Label = "", Icon = "@drawable/icon", Theme = "@style/CustomActionBarTheme",WindowSoftInputMode = SoftInput.AdjustResize)]
    public class ListEditAdd : Activity
    {
        Category cat;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditListItems);

            //2 - populate EditText id = "@+id/listTitleEdit with list name
            int catID = Intent.GetIntExtra("Category", -1);//get entryid from intent
            cat = MainController.GetCategory(catID);//get entry from DB
            if (cat == null)//check that entry returned
                throw new Exception($"Category not found for catID = {catID}");
            EditText catTitle = FindViewById<EditText>(Resource.Id.listTitleEdit);
            catTitle.Text = cat.Name;

            //3 - populate ListView id = "@+id/titleFieldListEdit with the header column names (use editrowsingle to make editable)
            List<Column> cols = MainController.GetAllColumns(catID);
            List<Column> titleCols = cols.Where(c => c.Type == Column.ColumnType.Title).ToList();
            List<Column> otherCols = cols.Where(c => c.Type == Column.ColumnType.Normal).ToList();
            ListView headerColumns = FindViewById<ListView>(Resource.Id.titleFieldListEdit);
            ColumnEditAdapter adpaterTH = new ColumnEditAdapter(this,titleCols);
            headerColumns.Adapter = adpaterTH;

            //4 - populate ListView id = "@+id/otherFieldListEdit with non-header column names (use editrowsingle to make editable)
            ListView otherColumns = FindViewById<ListView>(Resource.Id.otherFieldListEdit);
            ColumnEditAdapter adpaterOH = new ColumnEditAdapter(this, otherCols);
            otherColumns.Adapter = adpaterOH;

            //5 - hook up making pop-up for text entry if Button id = "@+id/btnAddCol_EL is pressed (TODO)
            Button btnAddColName = FindViewById<Button>(Resource.Id.btnAddCol_EL);
            btnAddColName.Click += BtnAddColName_Click;

            //6 - make info pannel to explain header rows when ImageButton @+id/btnInfo_ELI is pressed
            ImageButton btnInfo = FindViewById<ImageButton>(Resource.Id.btnInfo_ELI);
            btnInfo.Click += BtnInfo_Click;

            //8 - save button (TODO)
            Button btnSave = FindViewById<Button>(Resource.Id.btnSave_EL);
            btnSave.Click += BtnSave_Click;

            //9 - cancel button
            Button btnCancel = FindViewById<Button>(Resource.Id.btnCancel_EL);
            btnCancel.Click += BtnCancel_Click;


        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
            //Check for blank fields
            //Check for saved fields
            //Check if any of the columns have moved position
            //Check if duplicate col names & message
        }

        private void BtnInfo_Click(object sender, EventArgs e)
        {

            //Information button on headers
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("What is a Summary Heading?");
            alert.SetMessage("There are two types of names you can add to your lists.  \n\nSummary headings are what you'll see on the main screen.  The other headings you will only see once you click into the item.  \n\nSummary headings should be the most important informaiton to you. For example band name and albumn title would make great summary headings.");
            alert.SetNeutralButton("OK", (s, a) => { });
            Dialog dialog = alert.Create();
            dialog.Show();
        }

        private void BtnAddColName_Click(object sender, EventArgs e)
        {

            //Needs to generate user input alertdialogue
      
        }

      

    }
}