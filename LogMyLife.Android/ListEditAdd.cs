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

namespace LogMyLife.Android
{
    [Activity(ScreenOrientation = a.Content.PM.ScreenOrientation.Portrait, Label = "", Icon = "@drawable/icon", Theme = "@style/CustomActionBarTheme",WindowSoftInputMode = SoftInput.AdjustResize)]
    public class ListEditAdd : Activity
    {
        // 1 - set which list is being edited or if new list (don't need - will always exist)

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            
                        
            //2 - populate EditText id = "@+id/listTitleEdit with list name if available
            
            //3 - populate ListView id = "@+id/titleFieldListEdit with the header column names (use editrowsingle to make editable)
            
            //4 - populate ListView id = "@+id/otherFieldListEdit with non-header column names (use editrowsingle to make editable)

            //5 - hook up making pop-up for text entry if Button id = "@+id/btnAddCol_EL is pressed
            Button btnAddColName = FindViewById<Button>(Resource.Id.btnAddCol_EL);
            btnAddColName.Click += BtnAddColName_Click;

            //6 - make info pannel to explain header rows when ImageButton @+id/btnInfo_ELI is pressed
            Button btnInfo = FindViewById<Button>(Resource.Id.btnInfo_ELI);
            btnInfo.Click += BtnInfo_Click;

            //7 - delete column when trash is clicked

        
        }

        private void BtnInfo_Click(object sender, EventArgs e)
        {

            //Information button on headers
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("What is a Summary Heading?");
            alert.SetMessage("There are two types of names you can add to your lists.  Summary headings will show in your lists all the time.  The other headings you will only see once you click to the full screen mode.  Summary headings should be the most important informaiton.");
            alert.SetNeutralButton("OK",delegate { });//TO DO TEST****            
            Dialog dialog = alert.Create();
            dialog.Show();
        }

        private void BtnAddColName_Click(object sender, EventArgs e)
        {

            //Needs to generate user input alertdialogue
      
        }

      

    }
}