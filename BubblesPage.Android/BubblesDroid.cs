using System;
using Android.OS;
using BubblesPage.Droid;
using Plugin.CurrentActivity;
using Xamarin.Forms;

[assembly:Dependency(typeof(BubblesDroid))]
namespace BubblesPage.Droid
{
    public class BubblesDroid : IBubbles
    {       
        public void Bubbles(string image)
        {
            BubbleHeadService.Image = image;
            var intent = new Android.Content.Intent(CrossCurrentActivity.Current.Activity, typeof(BubbleHeadService));
            CrossCurrentActivity.Current.Activity.StartService(intent);
            CrossCurrentActivity.Current.Activity.Finish();
        }
    }
}
