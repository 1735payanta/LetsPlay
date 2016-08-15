
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

using Parse;

namespace LetsPlay
{
	[Activity (Label = "Home", MainLauncher = true, Icon = "@mipmap/icon")]			
	public class HomeActivity : Activity
	{
		TextView lblWelcome;
		Button btnFindGame;
		Button btnCreateGame;
		Button btnScheduledGames;
		Button btnProfile;
		Button btnLogOut;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Home);
			lblWelcome = FindViewById<TextView> (Resource.Id.lblWelcome);
			btnFindGame = FindViewById<Button> (Resource.Id.btnFindGame); 
			btnCreateGame = FindViewById<Button> (Resource.Id.btnCreateGame);
			btnScheduledGames = FindViewById<Button> (Resource.Id.btnScheduledGames);
			btnProfile = FindViewById<Button> (Resource.Id.btnProfile);
			btnLogOut = FindViewById<Button> (Resource.Id.btnLogOut); 

			btnFindGame.Click += BtnFindGame_Click;
			btnCreateGame.Click += BtnCreateGame_Click;
			btnScheduledGames.Click += BtnScheduledGames_Click;
			btnProfile.Click += BtnProfile_Click;
			btnLogOut.Click += BtnLogOut_Click;
			// Create your application here
//			var currentUser = ParseUser.CurrentUser;
//			lblWelcome.Text = "Welcome, " + currentUser ["FirstName"];
		}

		void BtnFindGame_Click (object sender, EventArgs e)
		{
			var intent = new Intent (this, typeof(FindGameActivity));
			StartActivity (intent);
		}

		void BtnLogOut_Click (object sender, EventArgs e)
		{
			var intent = new Intent (this, typeof(LogInActivity));
			StartActivity (intent);
		}

		void BtnProfile_Click (object sender, EventArgs e)
		{
			var intent = new Intent (this, typeof(ProfileActivity));
			StartActivity (intent);
		}

		void BtnScheduledGames_Click (object sender, EventArgs e)
		{
			var intent = new Intent (this, typeof(GamesActivity));
			StartActivity (intent); 
		}

		void BtnCreateGame_Click (object sender, EventArgs e)
		{
			var intent = new Intent (this, typeof(CreateGameActivity));
			StartActivity (intent);
		}
	}
}

