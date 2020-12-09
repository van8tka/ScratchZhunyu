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
            var btnNextStep = FindViewById<Button>(Resource.Id.btnNextStep);
            var frame = FindViewById<FrameLayout>(Resource.Id.frFieldView);
            _drawField.LayoutParameters = new LinearLayout.LayoutParams(frame.LayoutParameters);         
            frame.AddView(_drawField);
            _sheepView = new SheepView(this);
            frame.AddView(_sheepView);
            btnNextStep.Click += (s, e) => MoveSheep( );
        }


        private void MoveSheep( )
        {
            var screen = ScreenSizePx.GetSize(this);
            _sheepView.SetX(_sheepView.GetX() + 10);
            _sheepView.SetY(_sheepView.GetY() + 10);
            _sheepView.Invalidate();
        }
    }

    public class SheepView : View
    {
        private int positionX, positionY;
        public SheepView(Context context):base(context)
        {
            var screen = ScreenSizePx.GetSize(context as Activity);
            positionX = screen.Item2/2;
            positionY = screen.Item1/2;
        }

        protected override void OnDraw(Canvas canvas)
        {
            Bitmap bitmap = BitmapFactory.DecodeResource(Resources, Resource.Drawable.sheep);          
            canvas.DrawBitmap(bitmap, positionX, positionY, null);
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