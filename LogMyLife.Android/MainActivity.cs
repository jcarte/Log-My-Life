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
	[Activity (ScreenOrientation = a.Content.PM.ScreenOrientation.Portrait, Label = "Log My Life", MainLauncher = true, Icon = "@drawable/LauncherIcon4", Theme = "@style/CustomActionBarTheme")]
	public class MainActivity : Activity
	{
		DrawerLayout mDrawerLayout;
		
		ArrayAdapter mLeftAdapter;
		ListView mLeftDrawer;
		ActionBarDrawerToggle mDrawerToggle;
        List<Category> cats = MainController.GetCategories();

        private List<string> cItems;
        private List<string> aItems;
        private ListView lvMainScreen;
        private ListView lvMainScreenLower;
        
        Category currCat;
        
        protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

            currCat = cats[0];

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            mDrawerLayout = FindViewById<DrawerLayout> (Resource.Id.myDrawer);
			mLeftDrawer = FindViewById<ListView> (Resource.Id.leftListView);
			mLeftDrawer.Tag = 0;
       

            mDrawerToggle = new MyActionBarDrawerToggle (this, mDrawerLayout, Resource.Drawable.ic_navigation_drawer, Resource.String.open_drawer, Resource.String.close_drawer);
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

            //About button
            Button about = FindViewById<Button>(Resource.Id.btnAbout);
            about.Click += About_Click;

            //Edit button
            Button edit = FindViewById<Button>(Resource.Id.btnEdit);
            edit.Click += Edit_Click;

            //Add New Entry
            Button addNew = FindViewById<Button>(Resource.Id.btnNew_MA);
            addNew.Click += AddNewEntryClicked;
        }

        private void Edit_Click(object sender, EventArgs e)
        {
            //got to ListEdit.axml
            Intent i = new Intent(this, typeof(ListEdit));//Start a detail activity, push the entry ID into it
            StartActivity(i);
        }

        private void About_Click(object sender, EventArgs e)
        {
            //set alert for executing the task
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            alert.SetTitle("About Log my Life v0.1");
            alert.SetMessage("Log my Life is a Jicola Production.  Version 0.1 is a beta version and there will be many great, new features coming soon, including customisable lists to give you control over your categories!");

            alert.SetNegativeButton("Ok", (senderAlert, args) =>
            {
             });

            Dialog dialog = alert.Create();
            dialog.Show();
        }

        private void AddNewEntryClicked(object sender, EventArgs e)
        {
            Entry newE = MainController.CreateEntry(currCat.CategoryID);
            Intent i = new Intent(this, typeof(EntryEditActivity));//Start a detail activity, push the entry ID into it
            i.PutExtra("EntryID", newE.EntryID);
            StartActivity(i);
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
            List<string> mLeftItems = new List<string>();
            cats = MainController.GetCategories();
            //Populates the items in the menu bar
            foreach (var i in cats)
            {
                mLeftItems.Add(i.Name);
            }
            
            mLeftAdapter = new ArrayAdapter(this, a.Resource.Layout.SimpleListItem1, mLeftItems);
            mLeftDrawer.Adapter = mLeftAdapter;

            if (!cats.Any(c=> c.CategoryID == currCat.CategoryID) )
            {
                currCat = cats[0];
            }
            //Set mainscreen content based on first menu item for inital load
            PopulateListScreen(currCat);
        }

        public void PopulateListScreen(Category cat)
        {
            //Set current category to be displayed - this will be "cat"
            currCat = cat;

            //Create list of current and archived items
            currentEnts = MainController.GetCurrentEntries(cat.CategoryID);
            archivedEnts = MainController.GetArchivedEntries(cat.CategoryID);
            //Converts the entries into strings
            cItems = currentEnts.Select(ent => string.Join(", ", ent.TitleData.Where(s=>!string.IsNullOrEmpty(s.Value)).Select(k => k.Value).ToArray())).ToList();
            aItems = archivedEnts.Select(ent => string.Join(", ", ent.TitleData.Where(s => !string.IsNullOrEmpty(s.Value)).Select(k => k.Value).ToArray())).ToList();

            //This is where the main content can be populated - make dynamic depending on what menu item clicked
            //Top half of screen - current items
            TextView mainScreenTop;
            mainScreenTop = FindViewById<TextView>(Resource.Id.clText);
            mainScreenTop.Text = "NEW " + cat.Name.ToUpper();
            ArrayAdapter<string> adpater = new ArrayAdapter<string>(this, a.Resource.Layout.TestListItem, cItems);
            lvMainScreen.Adapter = adpater;

            //Lower half of screen - archived items
            TextView mainScreenLower;
            mainScreenLower = FindViewById<TextView>(Resource.Id.alText);
            mainScreenLower.Text = "COMPLETED " + cat.Name.ToUpper();
            ArrayAdapter<string> adpaterL = new ArrayAdapter<string>(this, a.Resource.Layout.TestListItem, aItems);
            lvMainScreenLower.Adapter = adpaterL;
        } 

	}
}


