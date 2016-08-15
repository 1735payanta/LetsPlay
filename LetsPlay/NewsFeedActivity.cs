
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
	[Activity (Label = "News")]			
	public class NewsFeedActivity : Activity
	{
		EditText txtWinner;
		Button btnSubmit;
		EditText txtTeam1;
		EditText txtTeam2;
		EditText txtScore;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);
		
			SetContentView (Resource.Layout.NewsFeed);

			txtScore = FindViewById<EditText> (Resource.Id.txtScore);
			btnSubmit = FindViewById<Button> (Resource.Id.btnSubmit);
			txtTeam1 = FindViewById<EditText> (Resource.Id.txtTeam1);
			txtTeam2 = FindViewById<EditText> (Resource.Id.txtTeam2);
			txtWinner = FindViewById<EditText> (Resource.Id.txtWinner); 

			btnSubmit.Click += BtnSubmit_Click;
			// Create your application here
		}

		void BtnSubmit_Click (object sender, EventArgs e)
		{
			var user = new ParseUser()
			{
				Team1 = txtTeam1.Text,
				Team2 = txtTeam2.Text,
				Winner = txtWinner.Text
			};

			// other fields can be set just like with ParseObject
			user["Team1"] = txtTeam1.Text;
			user["Team2"] = txtTeam2.Text;
			user ["Winner"] = txtWinner.Text;
			//			user["Age"] = txtAge;
			//			user["Gender"] = txtGender.Text; 
		}
	}
}

