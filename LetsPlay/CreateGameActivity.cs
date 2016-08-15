
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
using System.IO;
using SQLite;

namespace LetsPlay
{
	[Activity (Label = "Create Game")]			
	public class CreateGameActivity : Activity
	{
		EditText txtGameName;
		EditText txtNumberOfPlayers; 
		EditText txtDate; 
		EditText txtLocation; 
		EditText txtTime;
		Button btnSubmit;

		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.CreateGame);

			txtGameName = FindViewById<EditText> (Resource.Id.txtGameName);
			txtNumberOfPlayers = FindViewById<EditText> (Resource.Id.txtNumberOfPlayers);
			txtDate = FindViewById<EditText> (Resource.Id.txtDate);
			txtTime = FindViewById<EditText> (Resource.Id.txtLocation);
			btnSubmit = FindViewById<Button> (Resource.Id.btnSubmit);
			txtLocation = FindViewById<EditText> (Resource.Id.txtLocation);

			btnSubmit.Click += BtnSubmit_Click;
		

			// Create our connection, if the database file and/or table doesn't exist create it
				//TODO: Add DB Creation 
				// create the database file if it doesn't exist 
				try 
			{ 
				// Create our connection, if the database and/or table doesn't exist create it 
				var db = new SQLiteConnection(filePath); 
				db.CreateTable<GamesActivity>(); 
			} 
			catch (IOException ex) 
			{ 
				var reason = string.Format("Failed to create Table - reason {0}", ex.Message); 
					Toast.MakeText(this, reason, ToastLength.Long).Show(); 
					}
					//TODO: Add code to populate the Table View if it contains data
			// Create your application here
		}

		void BtnSubmit_Click (object sender, EventArgs e)
		{
				string alertTitle, alertMessage;
				//input Validation: onliy insert a book if the title is not mpty
				if (!string.IsNullOrEmpty (txtTitle.Text)) {
					//Insert a book into the database 
					var newBook = new Game { BookTitle = txtTitle.Text, ISBN = txtISBN.Text };

					var db = new SQLiteConnection (filePath);
					db.Insert (newBook);
					//show an alert to confirm that the book has been added
					alertTitle = "Success";
					alertMessage = string.Format ("Book ID: {0} with Title: {1} has been successfully added!",
						newBook.BookId, newBook.BookTitle);
					//TODO: Add code to populate the List View with the new values
					PopulateListView(); 

				} else { // show failed alert message
					alertTitle = "Failed";
					alertMessage = "Enter a valid Book Title";
				}
				//create an alert and show it based on teh alet title and message created earlier
				AlertDialog.Builder alert = new AlertDialog.Builder (this);
				alert.SetTitle (alertTitle);
				alert.SetMessage (alertMessage);
				alert.SetPositiveButton ("OK", (senderAlert, args) => {
					Toast.MakeText (this, "Continue!", ToastLength.Short).Show ();
				});
				alert.SetNegativeButton ("Cancel", (senderAlert, args) => {
					Toast.MakeText (this, "Canceled!", ToastLength.Short).Show ();
				});
				Dialog dialog = alert.Create ();
				dialog.Show ();
			}
		}
		// define the file path on the device where the DB will be stored
		string filePath = 
			Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "BookList.db3"); 
}
		
	
