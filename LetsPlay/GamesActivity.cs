
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
	[Activity (Label = "GamesActivity")]			
	public class GamesActivity : Activity
	{
		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Games);
		}
			private void PopulateListView()
			{
				var db = new SQLiteConnection (filePath);
				//retrieve all the data in the DB table
				var bookList = db.Table<Game>();

				List<string> bookTitles = new List<string> ();

				//loop through the data and retrieve teh bookTitle column data only
				foreach (var book in bookList) {
					bookTitles.Add (book.BookTitle);
				}
				// set the data source / Adapter for the listView control to an array of the retrieved books
				tblBooks.Adapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleListItem1,bookTitles.ToArray ());
				PopulateListView(); //????
			}
			// Create your application here
		}
	}


