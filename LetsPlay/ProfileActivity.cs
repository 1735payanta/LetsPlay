
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
using Android.Content.PM;
using Android.Database;
using Android.Media;
using Parse;
using Java.IO;
using Android.Graphics;
using Android.Provider;

namespace LetsPlay
{
	public static class App {
		public static File _file;
		public static File _dir;
		public static Bitmap bitmap;
	}

	[Activity (Label = "Profile")]			
	public class ProfileActivity : Activity
	{
		ImageView imgProfilePic;
		TextView txtvName;
		TextView txtvUserName;
		TextView txtvAge;
		TextView txtvSkill;
		TextView txtvBio;
		TextView txtvAchievements;
		Button btnTakePhoto;
		Button btnUploadPhoto;
		String ImageName;
		byte[] ImageData;

		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.Profile);

//			imgProfilePic = FindViewById<ImageView> (Resource.Id.imgProfilePic);
			txtvName = FindViewById<TextView> (Resource.Id.txtvName);
			txtvUserName = FindViewById<TextView> (Resource.Id.txtvUserName);
			txtvAge = FindViewById<TextView> (Resource.Id.txtvAge);
			txtvSkill = FindViewById<TextView> (Resource.Id.txtvSkill);
			txtvBio = FindViewById<TextView> (Resource.Id.txtvBio);
			txtvAchievements = FindViewById<TextView> (Resource.Id.txtvAchievements);
//			btnUpdateProfile = FindViewById<Button> (Resource.Id.btUpdateProfile);
//			btnTakePhoto = FindViewById<Button> (Resource.Id.btnTakePhoto);

			// Create your application here
			if (ParseUser.CurrentUser != null) {
				var currentUser = ParseUser.CurrentUser;
				txtvName.Text = "Name:" + currentUser ["FirstName"] + currentUser ["LastName"];

				txtvUserName.Text = "UserName:" + currentUser ["UserName"];

				txtvAge.Text = "Age:" + currentUser ["Age"];

				txtvSkill.Text = "Skill:" + currentUser ["Skill"];

				txtvBio.Text = "Bio:" + currentUser ["Bio"];

			if (IsThereAnAppToTakePictures ()) {
					CreateDirectoryForPictures ();



					imgProfilePic = FindViewById<ImageView> (Resource.Id.imgProfilePic);
					btnTakePhoto = FindViewById<Button> (Resource.Id.btnTakePhoto);

					btnTakePhoto = FindViewById<Button> (Resource.Layout.btnTakePhoto);
					btnTakePhoto.Click += BtnTakePhoto_Click;
				}
			}
		}

			private void CreateDirectoryForPictures ()
			{
				App._dir = new File (
					Android.OS.Environment.GetExternalStoragePublicDirectory (
						Android.OS.Environment.DirectoryPictures), "LetsPlay");     // set your App Name here
				if (!App._dir.Exists ())
				{
					App._dir.Mkdirs( );
				}
			}

			private bool IsThereAnAppToTakePictures ()
			{
				Intent intent = new Intent (MediaStore.ActionImageCapture);
				IList<ResolveInfo> availableActivities =
					PackageManager.QueryIntentActivities (intent,         PackageInfoFlags.MatchDefaultOnly);
				return availableActivities != null && availableActivities.Count > 0;
			}

void BtnTakePhoto_Click (object sender, EventArgs e)
{
		Intent intent = new Intent (MediaStore.ActionImageCapture);
		App._file = new File (App._dir,           
				String.Format("myAppPhoto_{ 0}.jpg", Guid.NewGuid()));
		intent.PutExtra (MediaStore.ExtraOutput,        
				Android.Net. Uri.FromFile (App._file));
		StartActivityForResult (intent,	0); 	
}
		protected override void OnActivityResult (int requestCode, 
				Result resultCode, Intent data)
		{
			base.OnActivityResult (requestCode, resultCode, data);


				try
				{
					Android.Net. Uri contentUri = null;
					Android.Net. Uri _fileUri = null;



					string _filePath;



					if (resultCode == Result.Ok)  
					{    



						var camera = true;  



						if (requestCode == 1)  
						{   
							camera = true;  



							// make camera image available in the gallery  
							Intent mediaScanIntent = new Intent         ( Intent.ActionMediaScannerScanFile);
							contentUri = Android.Net. Uri.FromFile (App._file);
							mediaScanIntent.SetData (contentUri);
							SendBroadcast (mediaScanIntent);
						}   



						_filePath = camera ? App._file.AbsolutePath :          GetPathToGalleryImage(data.Data);  
						_fileUri = camera ? contentUri : data.Data;  



						if (_filePath != null)  
						{   
							var scaledBitmap =       DecodeSampledBitmapFromResource(_filePath, 575, 575);  
							if (scaledBitmap != null)  
							{   
								var matrix1 = new Matrix();  
								matrix1.PostRotate(GetCorrectOrientation(_filePath));  
								var rotatedBitmap = Bitmap.CreateBitmap(scaledBitmap,          0, 0, scaledBitmap.Width,  
									scaledBitmap.Height,  
									matrix1, true);  



								App.bitmap = rotatedBitmap;
								imgPhoto.SetImageBitmap( App.bitmap);  
								imgPhoto.LayoutParameters.Height = imgPhoto.Width;  



							}   
						}   



					}   



					if (App.bitmap != null)    {
						Toast.MakeText (this, "Image uploading…",  
							ToastLength.Short).Show ();
						using (var stream = new System.IO.MemoryStream())
					{     
						App.bitmap.Compress(Bitmap.CompressFormat.Jpeg, 
							25, stream);
						imageData = stream.ToArray();
					}                
						
					ParseFile imageFile = new ParseFile("myPhoto.jpg", imageData);



						// Save the file to Parse first
						await imageFile.SaveAsync(new        
							Progress<ParseUploadProgressEventArgs>(e => {
							// Check e.Progress for progress of upload     } ));

							// now save a reference to the image in your database table
							var myPhotos = new ParseObject("MyPhotos");     // name of your Parse Table
							myPhotos[ "UserId"] = ParseUser.CurrentUser.ObjectId;     // logged-in user
							myPhotos[ "Photo"] = imageFile; // the Parse file
							await myPhotos.SaveAsync(); 



									Toast.MakeText (this, "The Image was uploaded successfully!",      ToastLength.Short).Show ();



							App.bitmap = null;
					}
		}
									catch(Exception ex) {



										var error = ex.Message;
										Toast.MakeText (this, "Error: " + error, ToastLength.Long).Show ();
									}



									// Dispose of the Java side bitmap.
									GC.Collect();
									}




//			// Make it available in the gallery
//
//
//
//			Intent mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
//			Android.Net. Uri contentUri = Android.Net.Uri.FromFile (App._file);
//			mediaScanIntent.SetData (contentUri);
//			SendBroadcast (mediaScanIntent);
//
//
//
//			// Display in ImageView. We will resize the bitmap to fit the display.
//			// Loading the full sized image will consume to much memory
//			// and cause the application to crash.
//
//
//
//			int height = Resources.DisplayMetrics.HeightPixels;
//			int width = imgPhoto.Height;
//			App.bitmap = App._file.Path.LoadAndResizeBitmap (width, height);
//			if (App.bitmap != null)   {
//				imgPhoto.SetImageBitmap ( App.bitmap);
//				App.bitmap = null;
//			}
//
//
//
//			// Dispose of the Java side bitmap.
//			GC.Collect();
//		}
//
		public static class BitmapHelpers
		{
			public static Bitmap LoadAndResizeBitmap (this string fileName,              int width, int height)
			{
				// First we get the the dimensions of the file on disk
				BitmapFactory.Options options = new BitmapFactory.Options              { InJustDecodeBounds = true };
				BitmapFactory.DecodeFile (fileName, options);



				// calculate the ratio that we need to resize the image by
				// in order to fit the requested dimensions.
				int outHeight = options.OutHeight;
				int outWidth = options.OutWidth;
				int inSampleSize = 1;



				if (outHeight > height || outWidth > width)
				{
					inSampleSize = (outWidth > outHeight) ? (outHeight / height)
						: (outWidth / width);
				}



				// load the image and have BitmapFactory resize it for us.
				options.InSampleSize = inSampleSize;
				options.InJustDecodeBounds = false;
				Bitmap resizedBitmap = BitmapFactory.DecodeFile (fileName, options);



				return resizedBitmap;
			}
		}
			private string GetPathToGalleryImage(Android.Net.Uri uri)  
			{   
				if (uri != null)  
				{   
					string path = null;  
					// The projection contains the columns we want to return in our query.  
					var projection = new[] { MediaStore.Images.Media.InterfaceConsts.Data };  
					using (ICursor cursor = ManagedQuery(uri, projection, null, null, null))  
					{   
						if (cursor != null)  
						{   
							int columnIndex =        cursor.GetColumnIndexOrThrow(       MediaStore.Images.Media.InterfaceConsts.Data);  
							cursor.MoveToFirst();  
							path = cursor.GetString(columnIndex);  
						}   
					}   
					return path;  
				}    


				return null;  
			}    

			private float GetCorrectOrientation(String filename)  
			{   



				ExifInterface exif = new ExifInterface(filename);  
				int orientation = exif.GetAttributeInt(ExifInterface.TagOrientation, 1);  



				switch (orientation)  
				{   
				case 6:  
					return 90;  
				case 3:  
					return 180;  
				case 8:
					return 270;  
				default:  
					return 0;  
				}   
			}    



			public Bitmap DecodeSampledBitmapFromResource(String path,  
				int reqWidth, int reqHeight)  
			{   
				// First decode with inJustDecodeBounds=true to check dimensions  
				var options = new BitmapFactory.Options  
				{   
					InJustDecodeBounds = true,  
					InPreferredConfig = Bitmap.Config.Argb8888  
				} ;  
				BitmapFactory.DecodeFile(path, options);  



				// Calculate inSampleSize  
				options.InSampleSize = CalculateInSampleSize(options, reqWidth,  reqHeight);  



				// Decode bitmap with inSampleSize set  
				options.InJustDecodeBounds = false;  
				return BitmapFactory.DecodeFile(path, options);  
			}   
			public int CalculateInSampleSize(BitmapFactory.Options options,  int reqWidth,      int reqHeight)  
			{   
				// Raw height and width of image  
				int height = options.OutHeight;  
				int width = options.OutWidth;  
				int inSampleSize = 1;  



				if (height > reqHeight || width > reqWidth)  
				{   



					int halfHeight = height / 2;  
					int halfWidth = width / 2;  



					// Calculate the largest inSampleSize value that is a power of 2  
					// and keeps both  
					// height and width larger than the requested height and width.  
					while ((halfHeight / inSampleSize) > reqHeight  &&        (halfWidth / inSampleSize) > reqWidth)  
					{   
						inSampleSize *= 2;  
					}   
				}   



				return inSampleSize;  
			}  


	}
}

