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

namespace RaysHotDogs
{
    [Activity(Label = "Visit Ray's store")]
    public class RayMapActivity : Activity
    {
        private Button externalMapButton;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.RayMapView);
            FindViews();
            HandleEvents();
            // Create your application here
        }

        private void FindViews()
        {
            externalMapButton = FindViewById<Button>(Resource.Id.externalMapButton);
        }

        private void HandleEvents()
        {
            externalMapButton.Click += ExternalMapButton_Click;
        }

        private void ExternalMapButton_Click(object sender, EventArgs e)
        {
            Android.Net.Uri rayLocationUri = Android.Net.Uri.Parse("geo:50.846704,4.352446");
            Intent mapIntent = new Intent(Intent.ActionView, rayLocationUri);
            StartActivity(mapIntent);
        }
    }
}