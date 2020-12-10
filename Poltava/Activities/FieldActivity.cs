using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poltava.Activities
{
    [Activity(Label = "FieldActivity", MainLauncher = true, ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape)]
    public class FieldActivity : Activity
    {
        private DrawView _drawField;
        private SheepView _sheepView;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            _drawField = new DrawView(this);
            SetContentView(Resource.Layout.FieldView);
            var btnLeft = FindViewById<Button>(Resource.Id.btnStepLeft);
            var btnRight = FindViewById<Button>(Resource.Id.btnStepRight);
            var btnUp = FindViewById<Button>(Resource.Id.btnStepUp);
            var btnDown = FindViewById<Button>(Resource.Id.btnStepDown);
            var frame = FindViewById<FrameLayout>(Resource.Id.frFieldView);
            _drawField.LayoutParameters = new LinearLayout.LayoutParams(frame.LayoutParameters);         
            frame.AddView(_drawField);
            _sheepView = new SheepView(this);
            frame.AddView(_sheepView);
            btnLeft.Click += (s, e) => MoveLeft( );
            btnRight.Click += (s, e) => MoveRight();
            btnUp.Click += (s, e) => MoveUp();
            btnDown.Click += (s, e) => MoveDown();           
        }

        protected override void OnStart()
        {
            base.OnStart();
            _border = new Border()
            {
                Top = 5,
                Left = 5,
                Bottom = ScreenSizePx.GetSize(this).Item1 - 5,
                Right = ScreenSizePx.GetSize(this).Item2 - 5
            };
        }


        private Border _border;

        private struct Border
        {
            public int Top;  
            public int Bottom;
            public int Left;
            public int Right;
        }
        
        private int stepSize = 50;
        private void MoveDown()
        {
            _sheepView.PositionY += stepSize;
            _sheepView.Invalidate();
        }

        private void MoveUp()
        {
            _sheepView.PositionY -= stepSize;
            _sheepView.Invalidate();
        }

        private void MoveRight()
        {
            _sheepView.PositionX += stepSize;
            _sheepView.Invalidate();
        }

        private void MoveLeft( )
        {
            _sheepView.PositionX -= stepSize;
            _sheepView.Invalidate();
        }
    }

    public class SheepView : View
    {
        private int _positionX, _positionY;
        private int HeightScreen, WidthScreen;
        double imageHeight;
        double imageWidth;
        private Context _context;
        public int PositionX { get => _positionX; set {
                if (value >= 0 && value <= WidthScreen - imageWidth )
                    _positionX = value;
                else
                    BorderMsg();
            }
        }

        public int PositionY
        {
            get => _positionY; set {
                if (value >= 0 && value <= HeightScreen - imageHeight)
                    _positionY = value;
                else
                    BorderMsg();
            }
        }

        private void BorderMsg()
        {
            Toast.MakeText(_context, "Вы достигли границы игрового поля!", ToastLength.Short).Show();
        }

        public SheepView(Context context):base(context)
        {
            _context = context;
            var screen = ScreenSizePx.GetSize(context as Activity);
            HeightScreen = screen.Item1;
            WidthScreen = screen.Item2;
            PositionY = HeightScreen/2;
            PositionX = WidthScreen/2;
        }

     

        protected override void OnDraw(Canvas canvas)
        {
            Bitmap bitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.sheep);
            imageHeight = bitmap.Height;
            imageWidth = bitmap.Width;
            canvas.DrawBitmap(bitmap, PositionX, PositionY, null);
            base.OnDraw(canvas);
        }

    }


    public class DrawView : View
    {
        private int _width;
        private int _height;
        public DrawView(Context context) : base(context)
        {
            var screen = ScreenSizePx.GetSize(context as Activity);
            _width = screen.Item2;
            _height = screen.Item1;
        }
        protected override void OnDraw(Canvas canvas)
        {
            Bitmap bitmapRaw = BitmapFactory.DecodeResource(Resources, Resource.Drawable.see_map1024);
            Bitmap bitmap = Bitmap.CreateScaledBitmap(bitmapRaw, _width, _height, false);
            canvas.DrawBitmap(bitmap, 0, 0, null);
            base.OnDraw(canvas);
        }
    }

    public static class ScreenSizePx
    {
        public static Tuple<int, int> GetSize(Activity activity)
        {
            DisplayMetrics displayMetrics = new DisplayMetrics();
            activity.WindowManager.DefaultDisplay.GetMetrics(displayMetrics);
            return new Tuple<int, int>(displayMetrics.HeightPixels, displayMetrics.WidthPixels);
        }
    }
}