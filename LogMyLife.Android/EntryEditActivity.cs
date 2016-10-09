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

namespace LogMyLife.Android
{
    [Activity(ScreenOrientation = a.Content.PM.ScreenOrientation.Portrait, Label = "", Icon = "@drawable/icon", Theme = "@style/CustomActionBarTheme")]
    public class EntryEditActivity : Activity
    {
        Entry entry;

        EditText comment;
        RatingBar rating;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.EntryEdit);

            int entryID = Intent.GetIntExtra("EntryID", -1);//get entryid from intent
            entry = MainController.GetEntry(entryID);//get entry from DB

            if (entry == null)//check that entry returned
                throw new Exception($"Entry not found for EntryID = {entryID}");

            //populate titleFieldList
            ListView titleFieldList = FindViewById<ListView>(Resource.Id.titleFieldList);
            EditFieldAdapter adpaterFL = new EditFieldAdapter(this, entry.TitleData, true);
            titleFieldList.Adapter = adpaterFL;

            //Set Comment Box Value
            comment = FindViewById<EditText>(Resource.Id.commentTBox_EE);
            comment.SetHorizontallyScrolling(false);
            comment.SetLines(3);
            comment.Text = entry.Comment;
            comment.EditorAction += CommentFinishedEditing; ;

            //Set Rating bar star value
            float d = entry.StarRating;
            rating = FindViewById<RatingBar>(Resource.Id.ratingRBar);
            rating.Rating = d;
            rating.RatingBarChange += RatingClicked; ;

            //populate otherFieldList
            ListView otherFieldList = FindViewById<ListView>(Resource.Id.otherFieldList);
            EditFieldAdapter adpaterOFL = new EditFieldAdapter(this, entry.OtherData, true);
            otherFieldList.Adapter = adpaterOFL;

            Button btnSave = FindViewById<Button>(Resource.Id.btnSave_EE);
            Button btnCancel = FindViewById<Button>(Resource.Id.btnCancel_EE);

            btnSave.Click += SaveClicked;
            btnCancel.Click += CancelClicked;
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void RatingClicked(object sender, RatingBar.RatingBarChangeEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void CommentFinishedEditing(object sender, TextView.EditorActionEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}