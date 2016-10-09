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
        Entry entry;

        RatingBar rating;
        EditText comment;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EntryView);

            int entryID = Intent.GetIntExtra("EntryID", -1);//get entryid from intent
            entry = MainController.GetEntry(entryID);//get entry from DB

            if (entry == null)//check that entry returned
                throw new Exception($"Entry not found for EntryID = {entryID}");

            //populate titleFieldList
            ListView titleFieldList = FindViewById<ListView>(Resource.Id.titleFieldList);
            EditFieldAdapter adpaterFL = new EditFieldAdapter(this, entry.TitleData, false );
            titleFieldList.Adapter = adpaterFL;

            //Set Comment Box Value
            comment = (EditText)FindViewById(Resource.Id.txtComment_EV);
            comment.SetHorizontallyScrolling(false);
            comment.SetLines(3);
            comment.Text = entry.Comment;
            comment.EditorAction += CommentFinishedEditing;

            //Set Rating bar star value
            float d = entry.StarRating;
            rating = (RatingBar)FindViewById(Resource.Id.ratingRBar);
            rating.Rating = d;
            rating.RatingBarChange += RatingClicked;

            //populate otherFieldList
            ListView otherFieldList = FindViewById<ListView>(Resource.Id.otherFieldList);
            EditFieldAdapter adpaterOFL = new EditFieldAdapter(this, entry.OtherData, false);
            otherFieldList.Adapter = adpaterOFL;

            Button btnEdit = FindViewById<Button>(Resource.Id.btnEdit_EV);
            Button btnArchive = FindViewById<Button>(Resource.Id.btnArchive_EV);

            //Set up click events for the buttons
            btnEdit.Click += EditClicked;
            btnArchive.Click += ArchiveClicked;

            if (entry.IsArchived)
                btnArchive.Text = "Unarchive";
            else
                btnArchive.Text = "Archive";


        }

        private void CommentFinishedEditing(object sender, TextView.EditorActionEventArgs e)
        {
            if(e.ActionId == ImeAction.Done)
            {
                entry.Comment = comment.Text;
                MainController.UpdateEntry(entry);
                Toast.MakeText(this, "Comment Updated", ToastLength.Short).Show();
            }
        }

        private void RatingClicked(object sender, EventArgs e)
        {
            entry.StarRating = rating.Rating;
            MainController.UpdateEntry(entry);
            Toast.MakeText(this, "Rating Updated", ToastLength.Short).Show();
        }

        private void EditClicked(object sender, EventArgs e)
        {
            Intent i = new Intent(this, typeof(EntryEditActivity));//Start a detail activity, push the entry ID into it
            i.PutExtra("EntryID", entry.EntryID);
            StartActivity(i);
        }

        private void ArchiveClicked(object sender, EventArgs e)
        {
            entry.IsArchived = !entry.IsArchived;//swap archived state
            MainController.UpdateEntry(entry);
            string tstmsg = "Entry " + (entry.IsArchived ? "Archived" : "Unarchived");
            base.OnBackPressed();
            Toast.MakeText(this, tstmsg, ToastLength.Short).Show();
        }
    }
}