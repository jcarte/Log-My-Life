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
        //Blank holder for the listview
        ListView listList;
        //All of the category objects from the database
        List<Category> cats;

        protected override void OnResume()
        {
            base.OnResume();
            //Blank list to put the string of the name in the objects in cats
            List<string> mItems = new List<string>();
            //The call to get all the info on the categories ready to put into the list
            cats = MainController.GetCategories();
            //Puts the name from each iteration in GetCategories into mItems
            foreach (var i in cats)
            {
                mItems.Add(i.Name);
            }
            //Create an adapter to translate what's in mItems into listList
            ArrayAdapter mAdapter = new ArrayAdapter<string>(this, a.Resource.Layout.SimpleListItemSingleChoice, mItems);

            //Put the adapter into the listview           
            listList.Adapter = mAdapter;

        }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "ListEdit" layout resource
            SetContentView(Resource.Layout.ListEdit);

            //Define listList ListView to be the list in ListEdit.axml (editListList)
            listList = FindViewById<ListView>(Resource.Id.editListsList);
            listList.ChoiceMode = a.Widget.ChoiceMode.Single;
            
            //Link up buttons
            Button btnEdit = FindViewById<Button>(Resource.Id.btnEdit_LE);
            Button btnDelete = FindViewById<Button>(Resource.Id.btnDelete_LE);
            Button btnNew = FindViewById<Button>(Resource.Id.btnNew_LE);

            btnEdit.Click += BtnEdit_Click;
            btnDelete.Click += BtnDelete_Click;
            btnNew.Click += BtnNew_Click;
        }


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //make category object of list to be deleted
            Category todelete = cats.ElementAt(listList.CheckedItemPosition);
            
            
            //set alert for executing the task
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Delete");
            alert.SetMessage("Are you sure you want to delete this list?");
            alert.SetPositiveButton("Delete", (senderAlert, args) => {
                //Delete selected lists selected
                MainController.DeleteCategory(todelete);
                OnResume();
                Toast.MakeText(this, "Deleted!", ToastLength.Short).Show();
            });
            alert.SetNegativeButton("Cancel", (s, a) => {});
            Dialog dialog = alert.Create();
            dialog.Show();
            
        }
        
        private void BtnEdit_Click(object sender, EventArgs e)
        {
            //TODO: Go to Edit page for first selected item (need to do error handling if more than one list selected, 
            //or stop multiple selection above - annnoying for delete)
        }

        private void BtnNew_Click(object sender, EventArgs e)
        {

            //TODO - add pop up to get name of list to be added and make it then start edit list page
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("New List");
            alert.SetMessage("Enter the name of your new list:");
            // Set an EditText view to get user input  
            EditText input = new EditText(this);
            alert.SetView(input);
            
            //User confirmation
            alert.SetPositiveButton("Ok", (senderAlert, args) => {
                MainController.CreateCategory(input.Text, Category.CategoryType.UserCreated);            
                OnResume();
                Toast.MakeText(this, "New List Created!", ToastLength.Short).Show();
            });
            alert.SetNegativeButton("Cancel", (s, a) => { });
            Dialog dialog = alert.Create();
            dialog.Show();


            //set alert for executing the task


        }
    }
}