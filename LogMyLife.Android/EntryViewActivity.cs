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
    [Activity(ScreenOrientation = a.Content.PM.ScreenOrientation.Portrait, Label = "", Icon = "@drawable/icon", Theme = "@style/CustomActionBarTheme")]
    public class EntryViewActivity : Activity
    {
        //Data objects
        int entryID;
        Entry entry;

        //Form components
        ListView titleFieldList;
        RatingBar rating;
        EditText comment;
        ListView otherFieldList;
        Button btnEdit;
        Button btnArchive;
        Button btnBack;
        ImageButton btnDelete;

        bool isLoading = false;//so that on resume doesn't trigger updates

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EntryView);

            entryID = Intent.GetIntExtra("EntryID", -1);//get entryid from intent

            //Setup lists
            titleFieldList = FindViewById<ListView>(Resource.Id.titleFieldList);
            otherFieldList = FindViewById<ListView>(Resource.Id.otherFieldList);

            //Setup Comment Box 
            comment = FindViewById<EditText>(Resource.Id.txtComment_EV);
            comment.SetHorizontallyScrolling(false);
            comment.SetLines(3);
            comment.EditorAction += Comment_EditorAction; ;
            comment.FocusChange += Comment_FocusChange; ;

            //Setup Rating
            rating = FindViewById<RatingBar>(Resource.Id.ratingRBar);
            rating.RatingBarChange += RatingClicked;

            //Set up buttons
            btnEdit = FindViewById<Button>(Resource.Id.btnEdit_EV);
            btnArchive = FindViewById<Button>(Resource.Id.btnArchive_EV);
            btnDelete = FindViewById<ImageButton>(Resource.Id.btnDelete_EV);
            btnBack = FindViewById<Button>(Resource.Id.btnB_EV);
            btnEdit.Click += EditClicked;
            btnArchive.Click += ArchiveClicked;
            btnDelete.Click += DeleteClicked;
            btnBack.Click += BtnBack_Click;
            
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        protected override void OnPause()
        {
            base.OnPause();

            if (entry.Comment != comment.Text)
            {
                entry.Comment = comment.Text;
                MainController.UpdateEntry(entry);
            }
        }
        protected override void OnResume()
        {
            base.OnResume();
            
            isLoading = true;

            entry = MainController.GetEntry(entryID);//get entry from DB

            if (entry == null)//check that entry returned
                throw new Exception($"Entry not found for EntryID = {entryID}");

            //populate titleFieldList
            EditFieldAdapter adpaterFL = new EditFieldAdapter(this, entry.TitleData, false);
            titleFieldList.Adapter = adpaterFL;

            //populate otherFieldList
            EditFieldAdapter adpaterOFL = new EditFieldAdapter(this, entry.OtherData, false);
            otherFieldList.Adapter = adpaterOFL;

            //populate comment text
            comment.Text = entry.Comment;

            //populate Rating bar star value
            float d = entry.StarRating;
            rating.Rating = d;
            
            //Archive button text
            if (entry.IsArchived)
                btnArchive.Text = "Unarchive";
            else
                btnArchive.Text = "Archive";

            isLoading = false;

        }


        private void Comment_EditorAction(object sender, TextView.EditorActionEventArgs e)
        {
            if (e.ActionId == ImeAction.Done)
            {
                InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
                inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

                CommentFinishedEditing();
            }
        }

        private void Comment_FocusChange(object sender, View.FocusChangeEventArgs e)
        {
            if (!e.HasFocus)
                CommentFinishedEditing();
        }

        private void CommentFinishedEditing()
        {
            entry.Comment = comment.Text;
            MainController.UpdateEntry(entry);
            Toast.MakeText(this, "Comment Updated", ToastLength.Short).Show();
        }


        private void RatingClicked(object sender, EventArgs e)
        {
            if(!isLoading)
            {
                entry.StarRating = rating.Rating;
                MainController.UpdateEntry(entry);
                Toast.MakeText(this, "Rating Updated", ToastLength.Short).Show();
            }
        }

        private void EditClicked(object sender, EventArgs e)
        {
            if(entry.Comment != comment.Text)
            {
                entry.Comment = comment.Text;
                MainController.UpdateEntry(entry);
            }
            
            Intent i = new Intent(this, typeof(EntryEditActivity));//Start a detail activity, push the entry ID into it
            i.PutExtra("EntryID", entry.EntryID);
            StartActivity(i);
        }

        private void ArchiveClicked(object sender, EventArgs e)
        {
            entry.Comment = comment.Text;
            
            entry.IsArchived = !entry.IsArchived;//swap archived state
            MainController.UpdateEntry(entry);
            string tstmsg = "Entry " + (entry.IsArchived ? "Archived" : "Unarchived");
            base.OnBackPressed();
            Toast.MakeText(this, tstmsg, ToastLength.Short).Show();
        }

        private void DeleteClicked(object sender, EventArgs e)
        {
            //set alert for executing the task
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("Delete");
            alert.SetMessage("Are you sure you want to delete this list?");
            alert.SetNegativeButton("Delete", (senderAlert, args) =>
            {
                //Delete selected lists selected
                MainController.DeleteEntry(entry);
                base.OnBackPressed();
                Toast.MakeText(this, "Deleted", ToastLength.Short).Show();

            });
            alert.SetPositiveButton("Cancel", (s, a) => { });
            Dialog dialog = alert.Create();
            dialog.Show();

            
        }
    }
}