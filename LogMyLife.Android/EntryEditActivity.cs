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

        //TODO taked out comment and rating so that all fields fit on screen, if so many fields that they fall
        //below the keyboard then it doesn't work, don't know why

        //EditText comment;
        //RatingBar rating;
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
            adpaterFL.ItemUpdated += AdpaterItemUpdated;

            ////Set Comment Box Value
            //comment = FindViewById<EditText>(Resource.Id.commentTBox_EE);
            //comment.SetHorizontallyScrolling(false);
            //comment.SetLines(3);
            //comment.Text = entry.Comment;
            //comment.EditorAction += CommentFinishedEditing; ;

            ////Set Rating bar star value
            //float d = entry.StarRating;
            //rating = FindViewById<RatingBar>(Resource.Id.ratingRBar);
            //rating.Rating = d;
            //rating.RatingBarChange += RatingClicked;

            //populate otherFieldList
            ListView otherFieldList = FindViewById<ListView>(Resource.Id.otherFieldList);//TODO EditText fields aren't showing right,
            EditFieldAdapter adpaterOFL = new EditFieldAdapter(this, entry.OtherData, true);
            otherFieldList.Adapter = adpaterOFL;
            adpaterOFL.ItemUpdated += AdpaterItemUpdated;

            Button btnSave = FindViewById<Button>(Resource.Id.btnSave_EE);
            Button btnCancel = FindViewById<Button>(Resource.Id.btnCancel_EE);

            btnSave.Click += SaveClicked;
            btnCancel.Click += CancelClicked;

        }

        private void AdpaterItemUpdated(KeyValuePair<string, string> kvp)
        {
            
            //InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
            //inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

            entry.EditColumnData(kvp.Key, kvp.Value);

            Toast.MakeText(this, kvp.Key + ": " + kvp.Value, ToastLength.Short).Show();
        }

        //private void RatingClicked(object sender, RatingBar.RatingBarChangeEventArgs e)
        //{
        //    entry.StarRating = rating.Rating;
        //    Toast.MakeText(this, "Comment Updated", ToastLength.Short).Show();
        //}

        //private void CommentFinishedEditing(object sender, TextView.EditorActionEventArgs e)
        //{
        //    if (e.ActionId == ImeAction.Done)
        //    {
        //        InputMethodManager inputManager = (InputMethodManager)this.GetSystemService(Context.InputMethodService);
        //        inputManager.HideSoftInputFromWindow(this.CurrentFocus.WindowToken, HideSoftInputFlags.NotAlways);

        //        entry.Comment = comment.Text;
        //        Toast.MakeText(this, "Comment Updated", ToastLength.Short).Show();
        //    }
        //}

        private void CancelClicked(object sender, EventArgs e)
        {
            base.OnBackPressed();
        }

        private void SaveClicked(object sender, EventArgs e)
        {
            base.OnBackPressed();
            MainController.UpdateEntry(entry);
            Toast.MakeText(this, "Saved", ToastLength.Short).Show();
        }
        
    }
}