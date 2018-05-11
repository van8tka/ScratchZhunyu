using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Media;

namespace ScratchZhunyu
{
	[Activity(Label = "Погладь Жуню", Theme = "@style/AppTheme.NoActionBar")]
	public class MainActivity : AppCompatActivity, View.IOnTouchListener
	{
     
        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			SetContentView(Resource.Layout.activity_main);
            var imgScratch = FindViewById<View>(Resource.Id.forTapCat);
            imgScratch.SetOnTouchListener(this);          
    }

        protected MediaPlayer player;
       


        bool isPlay = false;

        public bool OnTouch(View v, MotionEvent e)
        {
            if(e.Action == MotionEventActions.Move)
            {
                if(!isPlay)
                {
                    var afd = Assets.OpenFd("myr.mp3");
                    if (player == null)
                    {
                        player = new MediaPlayer();
                       
                    }
                    player.Reset();
                    player.SetDataSource(afd.FileDescriptor, afd.StartOffset, afd.Length);
                    player.Prepare();
                    player.Looping = true;
                    player.Start();

                }
                   
                isPlay = true;
            }
            if(e.Action == MotionEventActions.Up)
            {
                if(player!=null)
                    player.Stop();
                isPlay = false;
            }
            return true;
        }


        public override void OnBackPressed()
        {
            if (player != null)
                player.Stop();
            base.OnBackPressed();
        }
    }
}

