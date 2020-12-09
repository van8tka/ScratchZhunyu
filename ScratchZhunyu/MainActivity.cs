using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using Android.Media;
using Android.Telephony;
using Android.Content;

namespace ScratchZhunyu
{
	[Activity(Label = "Погладь Жуню", Theme = "@style/AppTheme.NoActionBar")]
	public class MainActivity : AppCompatActivity, View.IOnTouchListener
	{
        protected MediaPlayer player;
        bool isPlay = false;
        protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.activity_main);
            var imgScratch = FindViewById<View>(Resource.Id.forTapCat);
            imgScratch.SetOnTouchListener(this);
            if (isTabletOrTv())
            {
                var llB = FindViewById<LinearLayout>(Resource.Id.ltBtns);
                llB.Visibility = ViewStates.Visible;
            }
            var btnStartMur = FindViewById<Button>(Resource.Id.btnPlay);
            var btnStopMur = FindViewById<Button>(Resource.Id.btnStop);
            btnStartMur.Click += BtnStartMur_Click;
            btnStopMur.Click += BtnStopMur_Click;
        }

        private void BtnStopMur_Click(object sender, EventArgs e)
        {
            StopPlayer();
        }

        private void BtnStartMur_Click(object sender, EventArgs e)
        {
            StartPlayer();
        }

        public bool isTabletOrTv()
        {
            TelephonyManager manager = (TelephonyManager)this.ApplicationContext.GetSystemService(Context.TelephonyService);
            return manager.PhoneType == Android.Telephony.PhoneType.None;
        }


      
        public bool OnTouch(View v, MotionEvent e)
        {
          
            if (e.Action == MotionEventActions.Move)
            {
                StartPlayer();
            }
            if(e.Action == MotionEventActions.Up)
            {
                StopPlayer();
            }
            return true;
        }

        private void StartPlayer()
        {
            if (!isPlay)
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

        private void StopPlayer()
        {
            if (player != null)
                player.Stop();
            isPlay = false;
        }

        public override void OnBackPressed()
        {
            StopPlayer();
            base.OnBackPressed();
        }
    }
}

