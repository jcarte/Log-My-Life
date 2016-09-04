using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Support.V4.Widget;
using Android.Support.V4.App;
using LogMyLife.Domain;

namespace DrawerLayoutTutorial
{
	[Activity (Label = "Log My Life", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/CustomActionBarTheme")]
	public class MainActivity : Activity
	{
		DrawerLayout mDrawerLayout;
		List<string> mLeftItems = new List<string>();
		ArrayAdapter mLeftAdapter;
		ListView mLeftDrawer;
		ActionBarDrawerToggle mDrawerToggle;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			// Set our view from the "main" layout resource
			SetContentView (Resource.Layout.Main);
			mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.myDrawer);
			mLeftDrawer = FindViewById<ListView> (Resource.Id.leftListView);

			mLeftDrawer.Tag = 0;

            foreach (var i in MainController.GetCategories())
            {
                mLeftItems.Add(i.Name);
            }            
                

            mDrawerToggle = new MyActionBarDrawerToggle (this, mDrawerLayout, Resource.Drawable.ic_navigation_drawer, Resource.String.open_drawer, Resource.String.close_drawer);

			mLeftAdapter = new ArrayAdapter (this, Android.Resource.Layout.SimpleListItem1, mLeftItems);
			mLeftDrawer.Adapter = mLeftAdapter;


			mDrawerLayout.SetDrawerListener (mDrawerToggle);
			ActionBar.SetDisplayHomeAsUpEnabled (true);
			ActionBar.SetHomeButtonEnabled (true);	
			ActionBar.SetDisplayShowTitleEnabled (true);

            //This is where the main content can be populated - make dynamic depending on what menu item clicked
            TextView mainScreen;
            mainScreen = FindViewById<TextView>(Resource.Id.tvText);
            mainScreen.Text = "Hello, world, hiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii";

        }

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			mDrawerToggle.SyncState ();
		}

		public override void OnConfigurationChanged (Android.Content.Res.Configuration newConfig)
		{
			base.OnConfigurationChanged (newConfig);
			mDrawerToggle.OnConfigurationChanged (newConfig);
		}

		public override bool OnCreateOptionsMenu (IMenu menu)
		{
			MenuInflater.Inflate (Resource.Menu.action_bar, menu);
			return base.OnCreateOptionsMenu (menu);
		}
				
		public override bool OnOptionsItemSelected (IMenuItem item)
		{
            if (mDrawerToggle.OnOptionsItemSelected(item))
            {
                  return true;
            }

            switch (item.ItemId)
            {
                default:
                    return base.OnOptionsItemSelected(item);
            }

           

		}

        

	}
}


