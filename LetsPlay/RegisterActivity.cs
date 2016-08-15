
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
	[Activity (Label = "Registration")]			
	public class RegisterActivity : Activity
	{
		EditText txtFirstName;
		EditText txtLastName;
		EditText txtEmailAddress;
		EditText txtPassword;
		EditText txtConfirmPassword;
//		EditText txtAge;
//		EditText txtGender;
		Button btnRegister;
		ProgressBar progressBarRegister;
		TextView lblFName;
		TextView lblLName;
		TextView lblEAddress;
		TextView lblPassword;
		TextView lblCPassword;
//		TextView lblAge;
//		TextView lblGender;


		protected override void OnCreate (Bundle savedInstanceState)
		{
			base.OnCreate (savedInstanceState);

			SetContentView (Resource.Layout.Registration);

			txtFirstName = FindViewById<EditText> (Resource.Id.txtFirstName);
			txtLastName = FindViewById<EditText> (Resource.Id.txtLastName);
			txtEmailAddress = FindViewById<EditText> (Resource.Id.txtEmailAddress);
			txtPassword = FindViewById<EditText> (Resource.Id.txtPassword);
			txtConfirmPassword = FindViewById<EditText> (Resource.Id.txtConfirmPassword);
//			txtAge = FindViewById<EditText> (Resource.Id.txtAge);
//			txtGender = FindViewById<EditText> (Resource.Id.txtGender);
			progressBarRegister = FindViewById<ProgressBar> (Resource.Id.progressBarRegister);
			lblFName = FindViewById<TextView> (Resource.Id.lblFName);
			lblLName = FindViewById<TextView> (Resource.Id.lblLName);
			lblEAddress = FindViewById<TextView> (Resource.Id.lblEAddress);
			lblPassword = FindViewById<TextView> (Resource.Id.lblPassword);
			lblCPassword = FindViewById<TextView> (Resource.Id.lblCPassword);
//			lblAge = FindViewById<TextView> (Resource.Id.lblAge);
//			lblGender = FindViewById<TextView> (Resource.Id.lblGender);

			btnRegister = FindViewById<Button> (Resource.Id.btnRegister);
			btnRegister.Click += Register_Click;

			progressBarRegister.Visibility = ViewStates.Invisible;
			btnRegister.Visibility = ViewStates.Visible;
		}

		async void Register_Click (object sender, EventArgs e)
		{
			if (!txtEmailAddress.Text.Contains (".") || !txtEmailAddress.Text.Contains ("@")) {

				// TODO: show an alert dialog or toast
				Toast.MakeText (this, "Enter a valid Email address", ToastLength.Long).Show ();
				return;
			}

			// TODO: perform input validation for length of various EditText controls
			// TODO: perform password to confirm password validation
			// TODO: show progress bar and hide button control
			// TODO: call parse to register the user
			if (txtPassword.Text != txtConfirmPassword.Text) {
				// TODO: show an alert
				Toast.MakeText (this, "Password and Confirm Password must match", ToastLength.Long).Show ();
				return;
			}

			progressBarRegister.Visibility = ViewStates.Visible;
			btnRegister.Visibility = ViewStates.Invisible;

			var user = new ParseUser()
			{
				Username = txtEmailAddress.Text,
				Password = txtPassword.Text,
				Email = txtEmailAddress.Text
			};

			// other fields can be set just like with ParseObject
			user["FirstName"] = txtFirstName.Text;
			user["LastName"] = txtLastName.Text;
//			user["Age"] = txtAge;
//			user["Gender"] = txtGender.Text; 

			try
			{
				await user.SignUpAsync();
				// TODO: redirect to Login page via an intent

				var intent = new Intent(this, typeof(HomeActivity));
				StartActivity(intent);
			}
			catch (Exception ex) {
				// TODO: registration failed, show an alert dialog                
				// The login failed. Check the error to see why.
				progressBarRegister.Visibility = ViewStates.Invisible;
				btnRegister.Visibility = ViewStates.Visible;

				var error = ex.Message;

				var dialog = new AlertDialog.Builder(this);
				dialog.SetMessage("Registration Failed! Please try again later");
				dialog.SetNeutralButton("OK", delegate { });
				dialog.Show();
			}
		}
	}
}