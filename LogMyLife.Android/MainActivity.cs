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
using a = Android;
using LogMyLife.Domain.Model;

using System.Linq;

namespace LogMyLife.Android
{
	[Activity (Label = "Log My Life", MainLauncher = true, Icon = "@drawable/icon", Theme = "@style/CustomActionBarTheme")]
	public class MainActivity : Activity
	{
		DrawerLayout mDrawerLayout;
		List<string> mLeftItems = new List<string>();
		ArrayAdapter mLeftAdapter;
		ListView mLeftDrawer;
		ActionBarDrawerToggle mDrawerToggle;

        private List<string> cItems;
        private List<string> aItems;
        private ListView lvMainScreen;
        private ListView lvMainScreenLower;

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

			mLeftAdapter = new ArrayAdapter (this, a.Resource.Layout.SimpleListItem1, mLeftItems);
			mLeftDrawer.Adapter = mLeftAdapter;


			mDrawerLayout.SetDrawerListener (mDrawerToggle);
			ActionBar.SetDisplayHomeAsUpEnabled (true);
			ActionBar.SetHomeButtonEnabled (true);	
			ActionBar.SetDisplayShowTitleEnabled (true);







            List<Category> cats = MainController.GetCategories();
            Category currentCat = cats[0];

            List<Entry> currentEnts = MainController.GetCurrentEntries(currentCat.CategoryID);
            List<Entry> archivedEnts = MainController.GetArchivedEntries(currentCat.CategoryID);

            cItems = currentEnts.Select(e => $"{e.DisplayValue1}, {e.DisplayValue2}, {e.DisplayValue3}").ToList();
            aItems = archivedEnts.Select(e => $"{e.DisplayValue1}, {e.DisplayValue2}, {e.DisplayValue3}").ToList();

            //This is where the main content can be populated - make dynamic depending on what menu item clicked
            TextView mainScreenTop;
            mainScreenTop = FindViewById<TextView>(Resource.Id.clText);
            mainScreenTop.Text = "CURRENT " + currentCat.Name.ToUpper();
            TextView mainScreenLower;
            mainScreenLower = FindViewById<TextView>(Resource.Id.alText);
            mainScreenLower.Text = "ARCHIVED " + currentCat.Name.ToUpper();


            //test list
            //List<Entry> currentEnts = MainController.GetCurrentEntries(cat.CategoryID);
            //cItems = new List<string>();
            //cItems.Add("BookA");
            //cItems.Add("Bookb");
            //cItems.Add("Bookc");
            //cItems.Add("Bookd"); cItems.Add("Bookd"); cItems.Add("Bookd"); cItems.Add("Bookd"); cItems.Add("Bookd"); cItems.Add("Bookd"); cItems.Add("Bookd"); cItems.Add("Bookd");

            lvMainScreen = FindViewById<ListView>(Resource.Id.lvMainScreen);
            ArrayAdapter<string> adpater = new ArrayAdapter<string>(this, a.Resource.Layout.SimpleListItem1, cItems);
            lvMainScreen.Adapter = adpater;

            //List<Entry> archivedEnts = MainController.GetArchivedEntries(cat.CategoryID);
            //aItems = new List<string>();
            //aItems.Add("Book1");
            //aItems.Add("Book2");
            //aItems.Add("Book3");
            //aItems.Add("Book4");

            lvMainScreenLower = FindViewById<ListView>(Resource.Id.lvMainScreenLower);
            ArrayAdapter<string> adpaterL = new ArrayAdapter<string>(this, a.Resource.Layout.SimpleListItem1, aItems);
            lvMainScreenLower.Adapter = adpaterL;

        }

		protected override void OnPostCreate (Bundle savedInstanceState)
		{
			base.OnPostCreate (savedInstanceState);
			mDrawerToggle.SyncState ();
		}

		public override void OnConfigurationChanged (a.Content.Res.Configuration newConfig)
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


