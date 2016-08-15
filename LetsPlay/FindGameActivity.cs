
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

namespace LetsPlay
{
	[Activity (Label = "Find Game")]			
	public class FindGameActivity : Activity
	{
		TextView lblFindGame;
		EditText txtLocation;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
			SetContentView (Resource.Layout.FindGame);

			lblFindGame = FindViewById<TextView> (Resource.Id.lblFindGame);
			txtLocation = FindViewById<EditText> (Resource.Id.txtLocation);

			// Create your application here
		}
	}
}

