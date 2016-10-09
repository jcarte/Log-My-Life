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
        List<Category> cats = MainController.GetCategories();

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

            //Populates the items in the menu bar
            foreach (var i in cats)
            {
                mLeftItems.Add(i.Name);
            }      
                  
            mDrawerToggle = new MyActionBarDrawerToggle (this, mDrawerLayout, Resource.Drawable.ic_navigation_drawer, Resource.String.open_drawer, Resource.String.close_drawer);
			mLeftAdapter = new ArrayAdapter (this, a.Resource.Layout.SimpleListItem1, mLeftItems);
			mLeftDrawer.Adapter = mLeftAdapter;
            mLeftDrawer.ItemClick += MLeftDrawer_ItemClick;
			mDrawerLayout.SetDrawerListener (mDrawerToggle);
			ActionBar.SetDisplayHomeAsUpEnabled (true);
			ActionBar.SetHomeButtonEnabled (true);	
			ActionBar.SetDisplayShowTitleEnabled (true);

            //populate entry lists
            lvMainScreen = FindViewById<ListView>(Resource.Id.lvMainScreen);
            lvMainScreenLower = FindViewById<ListView>(Resource.Id.lvMainScreenLower);

            //Set up click events on entry lists to start new activity
            lvMainScreen.ItemClick += CurrentEntryClicked;
            lvMainScreenLower.ItemClick += ArchiveEntryClicked;

            
            

        }

        private void CurrentEntryClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            OpenEntryDetail(currentEnts[e.Position]);
        }
        private void ArchiveEntryClicked(object sender, AdapterView.ItemClickEventArgs e)
        {
            OpenEntryDetail(archivedEnts[e.Position]);
        }

        

        private void OpenEntryDetail(Entry e)
        {
            Intent i = new Intent(this, typeof(EntryViewActivity));//Start a detail activity, push the entry ID into it
            i.PutExtra("EntryID", e.EntryID);
            StartActivity(i);
        }


        private void MLeftDrawer_ItemClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            PopulateListScreen(cats[e.Position]);
            mDrawerLayout.CloseDrawers();
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

        List<Entry> currentEnts;
        List<Entry> archivedEnts;

        protected override void OnResume()
        {
            base.OnResume();

            //Set mainscreen content based on first menu item for inital load
            PopulateListScreen(cats[0]);
        }

        public void PopulateListScreen(Category cat)
        {
            //Set current category to be displayed - this will be "cat"
            //Category currentCat = cats[0];

            //Create list of current and archived items
            currentEnts = MainController.GetCurrentEntries(cat.CategoryID);
            archivedEnts = MainController.GetArchivedEntries(cat.CategoryID);
            //Converts the entries into strings
            cItems = currentEnts.Select(ent => string.Join(", ", ent.TitleData.Select(k => k.Value).ToArray())).ToList();
            aItems = archivedEnts.Select(ent => string.Join(", ", ent.TitleData.Select(k => k.Value).ToArray())).ToList();

            //This is where the main content can be populated - make dynamic depending on what menu item clicked
            //Top half of screen - current items
            TextView mainScreenTop;
            mainScreenTop = FindViewById<TextView>(Resource.Id.clText);
            mainScreenTop.Text = "CURRENT " + cat.Name.ToUpper();
            ArrayAdapter<string> adpater = new ArrayAdapter<string>(this, a.Resource.Layout.SimpleListItem1, cItems);
            lvMainScreen.Adapter = adpater;

            //Lower half of screen - archived items
            TextView mainScreenLower;
            mainScreenLower = FindViewById<TextView>(Resource.Id.alText);
            mainScreenLower.Text = "ARCHIVED " + cat.Name.ToUpper();
            ArrayAdapter<string> adpaterL = new ArrayAdapter<string>(this, a.Resource.Layout.SimpleListItem1, aItems);
            lvMainScreenLower.Adapter = adpaterL;
        } 

	}
}


