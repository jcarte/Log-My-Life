using System;
using Android.Support.V4.App;
using Android.App;
using Android.Support.V4.Widget;
using a = Android;

namespace LogMyLife.Android
{
	public class MyActionBarDrawerToggle : ActionBarDrawerToggle
	{
		Activity mActivity;

		public MyActionBarDrawerToggle (Activity activity, DrawerLayout drawerLayout, int imageResource, int openDrawerDesc, int closeDrawerDesc)
			: base (activity, drawerLayout, imageResource, openDrawerDesc, closeDrawerDesc)
		{
			mActivity = activity;
            
		}

		public override void OnDrawerOpened (a.Views.View drawerView)
		{
			int drawerType = (int)drawerView.Tag;

			if (drawerType == 0) 
			{
				//Left Drawer
				base.OnDrawerOpened (drawerView);
				mActivity.ActionBar.Title = "Select Your List";
			}
		}

		public override void OnDrawerClosed (a.Views.View drawerView)
		{
			int drawerType = (int)drawerView.Tag;

			if (drawerType == 0) 
			{
				//Left Drawer
				base.OnDrawerClosed (drawerView);
				mActivity.ActionBar.Title = "Log My Life";
			}
		}

		public override void OnDrawerSlide (a.Views.View drawerView, float slideOffset)
		{
			int drawerType = (int)drawerView.Tag;

			if (drawerType == 0) 
			{
				//Left Drawer
				base.OnDrawerSlide (drawerView, slideOffset);
			}

		}
	}
}

