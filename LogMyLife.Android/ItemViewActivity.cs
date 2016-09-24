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

namespace LogMyLife.Android
{
    [Activity(Label = "", Icon = "@drawable/icon", Theme = "@style/CustomActionBarTheme")]
    public class ItemViewActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Main2);

            Entry ent = MainController.GetCurrentEntries(1)[0];

            //populate titleFieldList
            ListView titleFieldList = FindViewById<ListView>(Resource.Id.titleFieldList);
            EditFieldAdapter adpaterFL = new EditFieldAdapter(this, ent.TitleData );
            titleFieldList.Adapter = adpaterFL;

            //Set Rating bar star value
            float d = 3;
            RatingBar rb = (RatingBar)FindViewById(Resource.Id.ratingRBar);
            rb.Rating = d;

            //populate otherFieldList
            ListView otherFieldList = FindViewById<ListView>(Resource.Id.otherFieldList);
            EditFieldAdapter adpaterOFL = new EditFieldAdapter(this, ent.OtherData);
            otherFieldList.Adapter = adpaterOFL;
        }
    }
}