
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
using Android.Gms.Maps;
using Android.Gms.Maps.Model;

namespace LetsPlay
{
	[Activity (Label = "MainActivity")]			
	public class MainActivity : Activity, IOnMapReadyCallback
	{
		GoogleMap map;
		MapFragment mapFragment;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			// Set our view from the "main" layout resource
			SetContentView(Resource.Layout.Main);

			// Find the map fragment
			mapFragment = FragmentManager.FindFragmentById(Resource.Id.map) as MapFragment;
			mapFragment.GetMapAsync(this);
		}

		static readonly LatLng Location_MA = new LatLng(43.04, -87.90);
		static readonly LatLng Location_Marquette = new LatLng(33.65, -117.83);
		static readonly LatLng Location_Chicago = new LatLng(41.88, -87.63);
		static readonly LatLng Location_Dallas = new LatLng(32.90, -97.03);

		public void OnMapReady(GoogleMap googleMap)
		{
			map = googleMap;
			map.MapType = GoogleMap.MapTypeSatellite;
			map.MyLocationEnabled = true;
			map.AddMarker(new MarkerOptions().SetPosition(Location_Marquette));
			map.AddMarker(new MarkerOptions()
				.SetPosition(Location_MA)
				.SetTitle("My Apartment")
				.SetSnippet("Where the magic happens")
				.SetIcon(BitmapDescriptorFactory
					.FromResource(Resource.Drawable.Icon)));
			Marker chicagoMarker = map.AddMarker(new MarkerOptions()
				.SetPosition(Location_Chicago)
				.SetTitle("Chicago")
				.Draggable(true)
				.SetIcon(BitmapDescriptorFactory
					.DefaultMarker(BitmapDescriptorFactory.HueYellow)));
			Marker dallasMarker = map.AddMarker(new MarkerOptions()
				.SetPosition(Location_Dallas)
				.SetTitle("Dallas")
				.SetSnippet("Go Cowboys!")
				.InfoWindowAnchor(1,0)
				.SetIcon(BitmapDescriptorFactory
					.DefaultMarker(BitmapDescriptorFactory.HueBlue)));
			map.MarkerClick += delegate(object sender, GoogleMap.MarkerClickEventArgs e) {
	if (e.Marker.Equals(dallasMarker)) {
					dallasMarker.Flat = !dallasMarker.Flat;
				}
			};
			map.MarkerClick += delegate(object sender, GoogleMap.MarkerClickEventArgs e) {
				if (e.Marker.Equals(dallasMarker)) {
					dallasMarker.Flat = !dallasMarker.Flat;
					dallasMarker.ShowInfoWindow();
				} else {
					// Execute default behavior for other markers.
					// Could also just execute the following line for every
					// marker..
					e.Handled = false;
				}
			};

			map.InfoWindowClick += (sender, e) => 
			{
				if (e.Marker.Id == chicagoMarker.Id) {
					e.Marker.SetIcon(BitmapDescriptorFactory
						.DefaultMarker(BitmapDescriptorFactory.HueRose));
				}
			};

			map.MapClick += (sender, e) => 
			{
				if (!chicagoMarker.IsInfoWindowShown) {
					chicagoMarker.SetIcon(BitmapDescriptorFactory
						.DefaultMarker(BitmapDescriptorFactory.HueYellow));
				}
			};

			CameraUpdate update = CameraUpdateFactory.NewLatLngZoom(Location_Xamarin, map.MaxZoomLevel);
			map.MoveCamera(update);

			map.MapLongClick += (sender, e) => 
				map.AnimateCamera(CameraUpdateFactory.ZoomOut(), 1000, null);

		}

	}
		}



