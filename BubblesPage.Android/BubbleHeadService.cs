using System;
using Android.App;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.Content;
using Android.Views;
using Android.Widget;
using Java.Util;
using Plugin.CurrentActivity;
using Refractored.Controls;
using static Android.Views.View;

namespace BubblesPage.Droid
{
    [Service]
    public class BubbleHeadService : Service, IOnTouchListener
    {
        public static string Image { get; set; }
        private IWindowManager mWindowManager;
        private View mChatHeadView;
        WindowManagerLayoutParams paramss;
        public override IBinder OnBind(Intent intent)
        {
            //String valor = getIntent().getStringExtra("usuario");
            //image = intent.GetStringExtra("image");
            return null;
        }

        public override void OnDestroy()
        {
            base.OnDestroy();
            if (mChatHeadView != null) mWindowManager.RemoveView(mChatHeadView);
        }
        public override void OnCreate()
        {
            base.OnCreate();
            mChatHeadView = LayoutInflater.From(CrossCurrentActivity.Current.Activity).Inflate(Resource.Layout.bubble_layout, null);


            // create a windowManager object - relatively easy
            mWindowManager = GetSystemService(WindowService).JavaCast<IWindowManager>();


            WindowManagerTypes LAYOUT_FLAG;
            if (Build.VERSION.SdkInt >= BuildVersionCodes.O)
            {
                LAYOUT_FLAG = WindowManagerTypes.ApplicationOverlay;//mWindowManager.LayoutParams.TYPE_APPLICATION_OVERLAY;
            }
            else
            {
                LAYOUT_FLAG = WindowManagerTypes.Phone;
            }

            paramss = new WindowManagerLayoutParams(
                WindowManagerLayoutParams.WrapContent,
                WindowManagerLayoutParams.WrapContent,
                LAYOUT_FLAG,
                WindowManagerFlags.NotFocusable,
                Android.Graphics.Format.Translucent);
            paramss.Gravity = GravityFlags.Top | GravityFlags.Left;
            paramss.X = 0;
            paramss.Y = 100;


            



            mWindowManager.AddView(mChatHeadView, paramss);


            var chatHeadImage = mChatHeadView.FindViewById<CircleImageView>(Resource.Id.chat_head_profile_iv);
            var fileDesc = Assets.OpenFd("messenger_tone.mp3");
            
            var bitmap = GetDrawable(Image);
            chatHeadImage.SetImageDrawable(bitmap);
            chatHeadImage.SetOnTouchListener(this);
            chatHeadImage.Click += ChatHeadImage_Click;
            MediaPlayer mp = new MediaPlayer();
            mp.Reset();
            mp.SetDataSource(fileDesc);
            mp.Prepare();
            mp.Start();
        }

        private void ChatHeadImage_Click(object sender, EventArgs e)
        {
            StopSelf();
        }

        private MotionEventActions lastAction;
        private int initialX;
        private int initialY;
        private float initialTouchX;
        private float initialTouchY;
        private static int MAX_CLICK_DURATION = 200;
        private long startClickTime;

        

        public bool OnTouch(View v, MotionEvent e)
        {
            if (e.Action == MotionEventActions.Down)
            {
                startClickTime = Calendar.Instance.TimeInMillis;
                initialX = paramss.X;
                initialY = paramss.Y;
                initialTouchX = e.RawX;
                initialTouchY = e.RawY;
                return true;
            }
            else if (e.Action == MotionEventActions.Up)
            {
                //long clickDuration = Calendar.Instance.TimeInMillis - startClickTime;
                //if (clickDuration < MAX_CLICK_DURATION)
                //{
                //    Intent intent = new Intent(this, typeof(BubbleActivity));
                //    intent.AddFlags(ActivityFlags.NewTask);//(Intent.FLAG_ACTIVITY_NEW_TASK);
                //    StartActivity(intent);
                //    StopSelf();
                //    lastAction = e.Action;
                //    return true;
                //}
            }
            else if(e.Action == MotionEventActions.Move)
            {
                paramss.X = initialX + (int)(e.RawX - initialTouchX);
                paramss.Y = initialY + (int)(e.RawY - initialTouchY);
                mWindowManager.UpdateViewLayout(mChatHeadView, paramss);
                lastAction = e.Action;
                return true;
            }
            return false;
        }



        private BitmapDrawable GetDrawable(string imageEntryImage)
        {
            int resID = Resources.GetIdentifier(imageEntryImage, "drawable",PackageName);
            var drawable = ContextCompat.GetDrawable(this, resID);
            var bitmap = ((BitmapDrawable)drawable).Bitmap;
            var resultHeight = drawable.IntrinsicHeight / 2;
            var resultWidth = drawable.IntrinsicWidth / 2;

            return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, resultWidth, resultHeight, true));
        }
    }
}
