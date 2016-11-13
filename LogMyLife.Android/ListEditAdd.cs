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

        ListView otherColumns;
        ListView headerColumns;
        EditText catTitle;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EditListItems);

            //populate EditText id = "@+id/listTitleEdit with list name
            int catID = Intent.GetIntExtra("Category", -1);//get entryid from intent
            cat = MainController.GetCategory(catID);//get entry from DB
            if (cat == null)//check that entry returned
                throw new Exception($"Category not found for catID = {catID}");
            catTitle = FindViewById<EditText>(Resource.Id.listTitleEdit);
            catTitle.Text = cat.Name;

            //Get list views
            headerColumns = FindViewById<ListView>(Resource.Id.titleFieldListEdit);
            otherColumns = FindViewById<ListView>(Resource.Id.otherFieldListEdit);
            PopulateLists();

            //hook up making pop-up for text entry if Button id = "@+id/btnAddCol_EL is pressed (TODO)
            Button btnAddColName = FindViewById<Button>(Resource.Id.btnAddCol_EL);
            btnAddColName.Click += BtnAddColName_Click;

            //make info pannel to explain header rows when ImageButton @+id/btnInfo_ELI is pressed
            ImageButton btnInfo = FindViewById<ImageButton>(Resource.Id.btnInfo_ELI);
            btnInfo.Click += BtnInfo_Click;

            //save button
            Button btnSave = FindViewById<Button>(Resource.Id.btnSave_EL);
            btnSave.Click += BtnSave_Click;

            //cancel button
            Button btnCancel = FindViewById<Button>(Resource.Id.btnCancel_EL);
            btnCancel.Click += BtnCancel_Click;


        }

        private void PopulateLists()
        {
            //get columns for category from database
            List<Column> cols = MainController.GetAllColumns(cat.CategoryID);
            List<Column> titleCols = cols.Where(c => c.Type == Column.ColumnType.Title).ToList();
            List<Column> otherCols = cols.Where(c => c.Type == Column.ColumnType.Normal).ToList();

            //populate ListView id = "@+id/titleFieldListEdit with the header column names (use editrowsingle to make editable)
            ColumnEditAdapter adpaterTH = new ColumnEditAdapter(this, titleCols);
            headerColumns.Adapter = adpaterTH;
            adpaterTH.ColumnDeleted += ColumnDeleted;

            //populate ListView id = "@+id/otherFieldListEdit with non-header column names (use editrowsingle to make editable)
            ColumnEditAdapter adpaterOH = new ColumnEditAdapter(this, otherCols);
            otherColumns.Adapter = adpaterOH;
            adpaterOH.ColumnDeleted += ColumnDeleted;
        }

        private void ColumnDeleted(object sender, Column e)
        {
            if (((ColumnEditAdapter)headerColumns.Adapter).Items.Count == 1 && e.Type == Column.ColumnType.Title)
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Delete Failed");
                alert.SetMessage($"Cannot delete '{e.Name}' because it is the only Summary Heading.  /nYou must always have at least one Summary Heading.");
                alert.SetNeutralButton("OK", (s, a) => { });
                alert.Create().Show();
            }
            else
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Delete");
                alert.SetMessage($"Are you sure you want to delete '{e.Name}' and all data stored in it?");
                alert.SetPositiveButton("Yes", (s, a) =>
                {
                    MainController.DeleteColumn(e);
                    PopulateLists();//refresh lists
                });
                alert.SetNegativeButton("No", (s, a) => { });
                alert.Create().Show();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            List<Column> dbCols = MainController.GetAllColumns(cat.CategoryID);
            List<Column> newCols = new List<Column>();
            newCols.AddRange(((ColumnEditAdapter)headerColumns.Adapter).Items);
            newCols.AddRange(((ColumnEditAdapter)otherColumns.Adapter).Items);

            //Error if any duplicates
            foreach (Column col in newCols)
            {
                if(newCols.Count(c => c.Name == col.Name)>1)
                {
                    AlertDialog.Builder alert = new AlertDialog.Builder(this);
                    alert.SetTitle("Duplicate Names");
                    alert.SetMessage($"'{col.Name}' appears multiple times, please remove one and try again.");
                    alert.SetNeutralButton("OK", (s, a) => { });
                    alert.Create().Show();
                    break;
                }
            }

            //Delete blank fields
            //newCols.Where(c => c.Name == "").ToList().ForEach(col => 
            //{ 
            //    MainController.DeleteColumn(col.ColumnID);//remove form db
            //    dbCols.Remove(dbCols.FirstOrDefault(dc => dc.ColumnID == col.ColumnID));//remove from list representing db
            //});

            int updateCount = 0;

            foreach (Column newCol in newCols)
            {
                Column dbCol = dbCols.FirstOrDefault(db => db.ColumnID == newCol.ColumnID);
                if (dbCol == null)//new col doesn't exist in db
                    throw new Exception("Column not yet added to db!");

                if (dbCol.Name != newCol.Name || dbCol.Order != newCol.Order)//update order and column name in db
                {
                    MainController.UpdateColumn(newCol);
                    updateCount++;
                }
            }


            //Update list name if changed
            if (catTitle.Text != cat.Name)
            {
                cat.Name = catTitle.Text;
                MainController.UpdateCategory(cat);
            }


            if (updateCount > 1)
                Toast.MakeText(this, $"{updateCount} Headings Updated", ToastLength.Short).Show();
            else if (updateCount == 1)
                Toast.MakeText(this, $"Heading Updated", ToastLength.Short).Show();

            base.OnBackPressed();
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

        private void BtnAddColName_Click(object sender, EventArgs e)//Add column to list
        {
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("New Heading");
            alert.SetMessage("Enter the name of your new heading:");
            // Set an EditText view to get user input  
            EditText input = new EditText(this);
            alert.SetView(input);
            //User confirmation
            alert.SetPositiveButton("Add as Summary Heading", (senderAlert, args) => {
                AddColumn(input.Text, Column.ColumnType.Title);
            });
            alert.SetNeutralButton("Add as Other Heading", (senderAlert, args) => {
                AddColumn(input.Text, Column.ColumnType.Normal);
            });
            alert.SetNegativeButton("Cancel", (s, a) => { });
            alert.Create().Show();
        }

        private void AddColumn(string name, Column.ColumnType type)
        {
            if (name == "")
            {
                Toast.MakeText(this, "Heading Must Have a Name", ToastLength.Short).Show();
                return;
            }

            List<Column> allCols = ((ColumnEditAdapter)headerColumns.Adapter).Items.Union(((ColumnEditAdapter)otherColumns.Adapter).Items).ToList();
            if (allCols.Any(c=>c.Name == name))
            {
                Toast.MakeText(this, $"A Heading For {name} Already Exists", ToastLength.Short).Show();
                return;
            }

            MainController.CreateColumn(name, cat.CategoryID, type);
            PopulateLists();
            Toast.MakeText(this, "New Column Created!", ToastLength.Short).Show();
        }


    }
}